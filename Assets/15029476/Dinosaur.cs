using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dinosaur : Agent
{
    public float waterLevel = 36.0f;

    protected Seek seek;
    private Face face;
    private FieldOfView fov;
    protected GenerateMapData mapData;

    //the two lists required to perform an A* search
    protected List<Vector3> openQueue = new List<Vector3>();
    protected List<Vector3> closedQueue = new List<Vector3>();
    protected List<Vector3> optimalRoute = new List<Vector3>();
    protected Vector3 startPos = new Vector3();
    protected Vector3 currentPos = new Vector3();

    protected GameObject targetObj;

    public enum dinoState
    {
        IDLE,       // The default state on creation.
        SEEKINGFOOD,    // Moving with the intent to find food (either looking for food or to eat meat)
        EATING,     // This is for eating depending on y value of the object to denote grass level
        SEEKINGDRINK,    // Moving with the intent to find water
        DRINKING,   // This is for Drinking, depending on y value of the object to denote water level
        SLEEPING,
        ALERTED,      // This is for hightened awareness, such as looking around
        ATTACKING,  // Causing damage to a specific target
        FLEEING,     // Running away from a specific target
        DEAD
    };
    protected dinoState currentState;

    [Header("Restore/Drain Speed percentage per second")]
    [SerializeField]
    protected float healthDrainSpeed = 0.2f;
    [SerializeField]
    protected float hungerDrainSpeed = 1.0f;
    [SerializeField]
    protected float thirstDrainSpeed = 2.0f;
    [SerializeField]
    protected float energyDrainSpeed = 1.0f;

    [Header("Multiplier for the drain/restore speeds")]
    [SerializeField]
    protected float speedMultiplier = 1.0f;

    protected Animator anim;

    //setup dino stats, initialised at 100%
    protected float health = 100.0f;
    protected float hunger = 100.0f;
    protected float thirst = 100.0f;
    protected float energy = 100.0f;

    //timers to control the drain/restore speeds of the animal stats
    private float healthTimer = 0.0f;
    private float hungerTimer = 0.0f;
    private float thirstTimer = 0.0f;
    private float energyTimer = 0.0f;

    private bool dinoIsDying = false;

    private string textStatus = "";

    protected bool stateChangeOccured = false;

    //says whether the dino is performing an activity and prevents immediate changing when priorities change
    private bool activityInProgress = false;

    void OnDrawGizmos()
    {
        for (int i = 0; i < closedQueue.Count; i++)
        {
            Gizmos.color = new Color(0, 0, 1, 0.8f);
            Gizmos.DrawCube(closedQueue[i], new Vector3(1, 50, 1));
        }

        for (int i = 0; i < optimalRoute.Count; i++)
        {
            Gizmos.color = new Color(1, 0, 0, 0.8f);
            Gizmos.DrawCube(optimalRoute[i], new Vector3(1, 50, 1));
        }

    }

    // Use this for initialization
    protected override void Start ()
    {
        mapData = GameObject.Find("EventManager").GetComponent<GenerateMapData>();
        targetObj = new GameObject("Dino Target");
        fov = transform.GetComponent<FieldOfView>();

        //setup the seek script and set its target to the target object
        seek = gameObject.AddComponent<Seek>();
        seek.target = targetObj;

        //face = gameObject.AddComponent<Face>();
        //face.target = targetObj;

        currentState = dinoState.IDLE;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update ()
    {
        float dt = Time.deltaTime;

        //slowly restore health over time and the dino is not dying
        if (currentState != dinoState.DEAD && !dinoIsDying && health < 100.0f)
        {
            healthTimer += dt;
            if (healthTimer >= 1.0f / (healthDrainSpeed * speedMultiplier))
            {
                health++;
                healthTimer = 0.0f;
            }
        }

        if (currentState != dinoState.EATING && currentState != dinoState.DEAD && hunger > 0.0f)
        {
            hungerTimer += dt;
            if (hungerTimer >= 1.0f / (hungerDrainSpeed * speedMultiplier))
            {
                hunger--;
                hungerTimer = 0.0f;
            }
        }

        if (currentState != dinoState.DRINKING && currentState != dinoState.DEAD && thirst > 0.0f)
        {
            thirstTimer += dt;
            if (thirstTimer >= 1.0f / (thirstDrainSpeed * speedMultiplier))
            {
                thirst--;
                thirstTimer = 0.0f;
            }
        }

        if (currentState != dinoState.SLEEPING && currentState != dinoState.DEAD && energy > 0.0f)
        {
            energyTimer += dt;
            if (energyTimer >= 1.0f / (energyDrainSpeed * speedMultiplier))
            {
                energy--;
                energyTimer = 0.0f;
            }
        }

        //if the dino has reached 0% hunger or thirst rapidly lower their health
        if (currentState != dinoState.DEAD && (hunger == 0.0f || thirst == 0.0f))
        {
            if (!dinoIsDying)
            {
                dinoIsDying = true;
                healthDrainSpeed = 8.0f;
            }

            //rapidly lower the dino's health
            if (health > 0.0f)
            {
                healthTimer += dt;
                if (healthTimer >= 1.0f / (healthDrainSpeed * speedMultiplier))
                {
                    health--;
                    healthTimer = 0.0f;
                }
            }
            else
            {
                //kill the dino
                currentState = dinoState.DEAD;
            }
        }

        //determine which is the lowest value dino stat and therefore the state to go into
        float lowestVal = 100.0f;
        if (thirst <= lowestVal)
        {
            lowestVal = thirst;

            if (!activityInProgress && currentState != dinoState.SEEKINGDRINK && currentState != dinoState.DRINKING)
            {
                currentState = dinoState.SEEKINGDRINK;
                stateChangeOccured = true;
            }
        }

        if (hunger < lowestVal)
        {
            lowestVal = hunger;

            if (!activityInProgress && currentState != dinoState.SEEKINGFOOD && currentState != dinoState.EATING)
            {
                currentState = dinoState.SEEKINGFOOD;
                stateChangeOccured = true;
            }
        }

        if (energy < lowestVal)
        {
            lowestVal = energy;

            if (!activityInProgress && currentState != dinoState.SLEEPING)
            {
                currentState = dinoState.SLEEPING;
                stateChangeOccured = true;
            }
        }

        //set the current activity that the dino will perform
        chooseActivity();

        //keep the text status updated each frame
        generateTextStatus();

        //move the dino towards its target
        seek.target = targetObj;
        seek.GetSteering();

        base.Update();

        //keep the dino looking towards its target
        Vector3 direction = targetObj.transform.position - transform.position;
        float targetOrientation = Mathf.Atan2(direction.x, direction.z);
        targetOrientation *= Mathf.Rad2Deg;
        transform.Rotate(Vector3.up, targetOrientation);

        //reset the queues if a state change occurs
        if (stateChangeOccured)
        {
            stateChangeOccured = false;

            openQueue.Clear();
            closedQueue.Clear();
            optimalRoute.Clear();
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    private void chooseActivity()
    {
        switch (currentState)
        {
            case dinoState.IDLE:
                break;
            case dinoState.SEEKINGFOOD:
                seekFood();
                break;
            case dinoState.EATING:
                eat();
                break;
            case dinoState.SEEKINGDRINK:
                seekWater();
                break;
            case dinoState.DRINKING:
                drinkWater();
                break;
            case dinoState.SLEEPING:
                sleep();
                break;
            case dinoState.ALERTED:
                break;
            case dinoState.ATTACKING:
                break;
            case dinoState.FLEEING:
                break;
            case dinoState.DEAD:
                break;
            default:
                break;
        }
    }

    private void generateTextStatus()
    {
        switch(currentState)
        {
            case dinoState.IDLE:
                textStatus = name + " doesn't know what to do.";
                break;
            case dinoState.SEEKINGFOOD:
                textStatus = name + " is looking for food.";
                break;
            case dinoState.EATING:
                textStatus = name + " is eating.";
                break;
            case dinoState.SEEKINGDRINK:
                textStatus = name + " is looking for water.";
                break;
            case dinoState.DRINKING:
                textStatus = name + " is drinking.";
                break;
            case dinoState.SLEEPING:
                textStatus = name + " is sleeping.";
                break;
            case dinoState.ALERTED:
                textStatus = name + " has been alerted.";
                break;
            case dinoState.ATTACKING:
                textStatus = name + " is fighting another dinosaur.";
                break;
            case dinoState.FLEEING:
                textStatus = name + " is fleeing from a dinosaur.";
                break;
            case dinoState.DEAD:
                textStatus = name + " has died.";
                break;
            default:
                break;
        }
    }

    protected Vector3 findNearestMapNode(Vector3 position)
    {
        int sample = mapData.returnSampleStep();
        Vector3 nodePos = mapData.returnMapNodes()[Mathf.RoundToInt(position.x / sample)][Mathf.RoundToInt(position.z / sample)].position;

        return nodePos;
    }

    protected Vector3 getNewNodePos(Vector3 curPos, int dir)
    {
        int sample = mapData.returnSampleStep();

        switch (dir)
        {
            case 0:
                curPos = new Vector3(curPos.x + sample, curPos.y, curPos.z);
                break;
            case 1:
                curPos = new Vector3(curPos.x - sample, curPos.y, curPos.z);
                break;
            case 2:
                curPos = new Vector3(curPos.x, curPos.y, curPos.z + sample);
                break;
            case 3:
                curPos = new Vector3(curPos.x, curPos.y, curPos.z - sample);
                break;
            default:
                break;
        }

        return findNearestMapNode(curPos);
    }

    private void seekWater()
    {
        //to seek water, the search will attempt to find the lowest level map node
        //weight of each node is its y value

        if (optimalRoute.Count == 0)
        {
            //starting node is added to the queue (the node nearest to the dino)
            startPos = findNearestMapNode(transform.position);
            currentPos = startPos;

            openQueue.Add(findNearestMapNode(currentPos));
            float gCost = Mathf.Abs(currentPos.y - startPos.y);
            float hCost = Mathf.Abs(currentPos.y - waterLevel);
            mapData.setGCost(currentPos, gCost);
            mapData.setHCost(currentPos, hCost);
            mapData.setParentPos(currentPos, Vector3.zero);

            do
            {
                if (openQueue.Count == 0)
                    break;

                currentPos = openQueue[0];

                //searches for the 4 nearest nodes
                for (int i = 0; i < 4; i++)
                {
                    //stores the new position that we're looking at
                    Vector3 tmpPos = getNewNodePos(currentPos, i);

                    if (tmpPos.x > 0 && tmpPos.x < (mapData.returnTerrainSize().x) && tmpPos.z > 0 && tmpPos.z < (mapData.returnTerrainSize().z))
                    {

                        //check whether the node is already in the closed list
                        if (!closedQueue.Contains(tmpPos))
                        {
                            float newGCost = Mathf.Abs(tmpPos.y - startPos.y);
                            float newHCost = Mathf.Abs(tmpPos.y - waterLevel);
                            float movementCost = newGCost + Mathf.Abs(tmpPos.y - currentPos.y) + newHCost;

                            if (movementCost < newGCost || !openQueue.Contains(tmpPos))
                            {
                                if (!openQueue.Contains(tmpPos))
                                {
                                    openQueue.Add(tmpPos);
                                    mapData.setGCost(tmpPos, movementCost);
                                    mapData.setHCost(tmpPos, newHCost);
                                    mapData.setParentPos(tmpPos, currentPos);
                                }
                                else
                                {
                                    mapData.setParentPos(tmpPos, currentPos);
                                    mapData.setGCost(tmpPos, movementCost);
                                }
                            }
                        }
                    }
                }

                //openQueue.Sort((a, b) => mapData.getGHCost(a).CompareTo(mapData.getGHCost(b)));
                //openQueue = openQueue.OrderBy(e => e.y).ToList();


                closedQueue.Add(openQueue[0]);
                openQueue.RemoveAt(0);

            } while (openQueue.Count > 0 && currentPos.y > waterLevel);
            optimalRoute.Add(currentPos);

            //mapData.setParentPos(openQueue[0], currentPos);
            closedQueue.Add(openQueue[0]);

            //Vector3 newCurPos = currentPos;

            while (currentPos != startPos)
            {
                optimalRoute.Add(currentPos);

                int i = 0;
                for (i = 0; i < closedQueue.Count; i++)
                    if (closedQueue[i] == currentPos)
                        break;

                currentPos = mapData.getParentPos(closedQueue[i]);
            }

            openQueue.Clear();
            closedQueue.Clear();
        }

        //make the dinosaur follow the optimal route
        if (optimalRoute.Count > 0)
        {
            targetObj.transform.position = optimalRoute[optimalRoute.Count - 1];

            float tolerance = 3.0f;
            if (Mathf.Abs(Mathf.Round(transform.position.x) - targetObj.transform.position.x) < tolerance)
            {
                if (Mathf.Abs(Mathf.Round(transform.position.z) - targetObj.transform.position.z) < tolerance)
                {
                    optimalRoute.RemoveAt(optimalRoute.Count - 1);
                }
            }
        }

        if (optimalRoute.Count == 0)
        {
            currentState = dinoState.DRINKING;
            stateChangeOccured = true;
        }
        

        ////if the dino is already looking for water but is at the correct y for water, then begin drinking
        //float levelTolerance = 1.0f;
        //if (transform.position.y <= waterLevel || Mathf.Abs(transform.position.y - waterLevel) < levelTolerance)
        //{
        //    currentState = dinoState.DRINKING;
        //    stateChangeOccured = true;
        //}
    }

    private void drinkWater()
    {
        activityInProgress = true;

        float thirstVal = 2.0f;

        thirstTimer += Time.deltaTime;
        if (thirstTimer >= 1.0f / (thirstDrainSpeed * speedMultiplier))
        {
            thirst += thirstVal;
            thirstTimer = 0.0f;

            if (thirst > 100.0f)
                thirst = 100.0f;
        }

        //mark the bool so that the dino can partake in other tasks
        if (thirst >= 100.0f)
            activityInProgress = false;
    }

    //gets called within the individual dinosaur classes
    protected virtual void seekFood() {}
    protected virtual void eat() { }

    private void sleep()
    {
        activityInProgress = true;

        float energyVal = 2.0f;

        energyTimer += Time.deltaTime;
        if (energyTimer >= 1.0f / (energyDrainSpeed * speedMultiplier))
        {
            energy += energyVal;
            energyTimer = 0.0f;

            if (energy > 100.0f)
                energy = 100.0f;
        }

        //mark the bool so that the dino can partake in other tasks
        if (energy >= 100.0f)
            activityInProgress = false;
    }

    //START: setters/getters
    public string returnName()
    {
        return name;
    }

    public float returnHealth()
    {
        return health;
    }

    public float returnHunger()
    {
        return hunger;
    }

    public float returnThirst()
    {
        return thirst;
    }

    public float returnEnergy()
    {
        return energy;
    }

    public string returnTextStatus()
    {
        return textStatus;
    }
    //END: setters/getters
}