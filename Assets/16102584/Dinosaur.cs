using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dinosaur : Agent
{
    //Constants
    protected static int HEIGHT_SAMPLES = 10;
    protected static float WATER_HEIGHT = 34.75f;
    protected static float VITAL_RESTORE = 15.0f;
    protected static float VITAL_DECLINE = 0.01f;
    protected static float VITAL_THRESHOLD = 0.75f;
    protected static float CONCERN_THRESHOLD = 0.5f;
    protected static float EMERGENCY_THRESHOLD = 0.25f;

    //Vitals
    protected List<Vital> priorities = new List<Vital>();
    protected float health = 100.0f;
    private float energy = 100.0f;
    private bool consuming = false;

    //Objects to obtain
    protected FieldOfView FOV;
    private int FOVRadius;
    private Terrain terrain;
    private TerrainData terrainData;

    //Scripts
    protected FSM brain;
    protected Wandering wander;

    //Peripherals
    private GameObject[] heightCheckSpheres = new GameObject[HEIGHT_SAMPLES];
    private GameObject heightSpheres;
    [SerializeField]
    private List<Vector3> foodSamples = new List<Vector3>();
    private bool identifyingResources = false;
    protected GameObject foodTarget;
    private GameObject waterTarget;
    protected GameObject group;
    private GameObject fleeTarget;

    //Extras
    private RawImage billboard;
    private Vector4 clear = new Vector4(0, 0, 0, 0);
    private Vector4 colour = new Vector4(1, 1, 1, 0.6f);
    private Texture2D[] billboards = new Texture2D[6];
    private AudioSource roar;
    protected Animator anim;

    // Use this for initialization
    protected virtual void Awake()
    {
        //Init
        brain = gameObject.AddComponent<FSM>();
        wander = gameObject.GetComponent<Wandering>();
        waterTarget = gameObject;
        foodTarget = new GameObject();
        foodTarget.AddComponent<Agent>();
        foodTarget.name = "Food";
        fleeTarget = new GameObject();
        fleeTarget.name = "Escape Point";
        fleeTarget.AddComponent<Agent>();
        FOV = gameObject.GetComponent<FieldOfView>();
        FOVRadius = Mathf.RoundToInt(FOV.viewRadius);
        initPriorities();
        //Locate
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        terrainData = terrain.terrainData;
        terrain.gameObject.AddComponent<TerrainReset>();
        anim = GetComponent<Animator>();
        //Spawn
        spawnSpheres();
        //Start
        brain.pushState(wanderAround);
        //Extras
        billboard = transform.Find("Billboard").Find("State").GetComponent<RawImage>();
        billboards[0] = Resources.Load<Texture2D>("Alerted");
        billboards[1] = Resources.Load<Texture2D>("Attacking");
        billboards[2] = Resources.Load<Texture2D>("Fleeing");
        billboards[3] = Resources.Load<Texture2D>("Hungry");
        billboards[4] = Resources.Load<Texture2D>("Keen");
        billboards[5] = Resources.Load<Texture2D>("Thirsty");
        SetBillboardState(6);
    }

    protected override void Update()
    {
        base.Update();
        updateVitals();
    }

    //Update
    private void updateVitals()
    {
        foreach (Vital v in priorities)
        {
            v.value -= VITAL_DECLINE;
            v.priority = v.value / 100;
            if(v.value <= EMERGENCY_THRESHOLD)
            {
                v.value = 0;
                health -= VITAL_DECLINE;
            }
            if (v.value > 100)
                health += VITAL_DECLINE;
        }
        priorities.Sort(delegate (Vital v1, Vital v2) { return v1.value.CompareTo(v2.value); });
        if(priorities[0].priority < VITAL_THRESHOLD && brain.getCurrentState() == wanderAround)
        {
            brain.pushState(priorities[0].startState);
        }

        if (health <= 0)
            brain.pushState(dead);

        if (brain.getCurrentState() == wanderAround)
            wander.timeToTarget = 8f;
        else
            wander.timeToTarget = 0.1f;
    }

    protected virtual void searchForFood()
    {
        wander.enabled = true;
        SetBillboardState(3);
        if (!identifyingResources)
        {
            StartCoroutine(identifyResources());
        }

        float shortestDist = 999999.0f;
        Vector3 targetPos = new Vector3(0, 0, 0);
        foreach (Vector3 vec in foodSamples)
        {
            float dist = Vector3.Distance(transform.position, vec);
            if (dist < shortestDist)
            {
                shortestDist = dist;
                targetPos = vec;
            }
        }
        foodTarget.transform.position = targetPos;
        wander.target = foodTarget;

        if (Vector3.Distance(transform.position, wander.target.transform.position) < 5)
        {
            wander.enabled = false;
            brain.pushState(eatFood);
        }

    }
    protected void eatFood()
    {
        if (!consuming)
        {
            if (priorities.Find(delegate (Vital v) { return v.name == "Hunger"; }).priority > 1)
            {
                brain.popState();
                brain.pushState(wanderAround);
                Debug.Log("Returning to wander");
            }
            else
            {
                priorities.Find(delegate (Vital v) { return v.name == "Hunger"; }).value += VITAL_RESTORE;
                StartCoroutine(consume());
                consuming = true;
                DestroyGrass();
            }
        }
    }

    private void DestroyGrass()
    {
        Vector3 tempVec = foodTarget.transform.position - terrain.transform.position;
        float fx = tempVec.x / terrain.terrainData.size.x;
        float fy = tempVec.z / terrain.terrainData.size.z;
        int x = Mathf.RoundToInt(fx * terrain.terrainData.detailWidth);
        int y = Mathf.RoundToInt(fy * terrain.terrainData.detailHeight);

        int[,] detail0 = terrainData.GetDetailLayer(x, y, 1, 1, 0);
        detail0[0, 0] = 0;
        terrain.terrainData.SetDetailLayer(x, y, 0, detail0);
    }
    private void findGrass()
    {
        int radius = FOVRadius;
        int searchFrequency = 10;
        List<Vector3> FOVPositions = new List<Vector3>();
        for (int x = -radius; x <= radius; x+= searchFrequency)
        {
            for (int y = -radius; y <= radius; y+=searchFrequency)
            {
                Vector3 newVec = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y);
                newVec.y = terrain.SampleHeight(newVec);
                if (Vector3.Distance(transform.position, newVec) <= radius)
                {
                    FOVPositions.Add(newVec);
                }
            }
        }
        List<Vector3> foundGrass = new List<Vector3>();
        foreach (Vector3 posInRadius in FOVPositions)
        {
            Vector3 tempVec = posInRadius - terrain.transform.position;
            float fx = tempVec.x / terrain.terrainData.size.x;
            float fy = tempVec.z / terrain.terrainData.size.z;
            int x = Mathf.RoundToInt(fx * terrain.terrainData.detailWidth);
            int y = Mathf.RoundToInt(fy * terrain.terrainData.detailHeight);
            int[,] detail0 = terrainData.GetDetailLayer(x, y, 1, 1, 0);

            int grassTotal = 0;
            foreach (int i in detail0)
            {
                grassTotal += i;
            }

            if (grassTotal != 0)
            {
                foundGrass.Add(posInRadius);
            }
        }

        foreach (Vector3 vec in foundGrass)
        {
            bool add = true;
            foreach (Vector3 grass in foodSamples)
            {
                if (Vector3.Distance(vec, grass) < searchFrequency)
                {
                    add = false;
                }
            }
            if (add)
            {
                foodSamples.Add(vec);
            }
        }
    }

    //Thirst
    public void searchForWater()
    {
        wander.enabled = true;
        if (wander.target == group || wander.target == foodTarget) 
        {
            wander.target = waterTarget;
        }
        SetBillboardState(5);
        if (waterTarget.transform.position.y >= WATER_HEIGHT - 1)
        {
            float lowestHeight = 1000;
            int lowestSphere = 0;
            float angle = 0;

            for (int i = 0; i < HEIGHT_SAMPLES; i++, angle += ((360 / HEIGHT_SAMPLES) * Mathf.Deg2Rad))
            {
                Vector3 position = GetCoordOnCircle(FOVRadius, angle);
                position.x += transform.position.x;
                position.z += transform.position.z;
                position.y = terrain.SampleHeight(position);

                if (position.y < lowestHeight)
                {
                    lowestHeight = position.y;
                    lowestSphere = i;
                }

                heightCheckSpheres[i].transform.position = position;
            }

            waterTarget.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            waterTarget = heightCheckSpheres[lowestSphere];
            waterTarget.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);

            moveTowards(waterTarget);
        }
        else
        {
            brain.popState();
            brain.pushState(goToWater);
        }
    }
    private void goToWater()
    {
        if (Vector3.Distance(transform.position, wander.target.transform.position) < 5)
        {
            wander.enabled = false;
            brain.popState();
            brain.pushState(drinkWater);
        }
    }
    private void drinkWater()
    {
        if (!consuming)
        {
            if (priorities.Find(delegate (Vital v) { return v.name == "Thirst"; }).priority > 1)
            {
                brain.popState();
                brain.pushState(wanderAround);
                Debug.Log("Returning to wander");
                waterTarget.transform.position = new Vector3(100, 100, 100);
            }
            else
            {
                priorities.Find(delegate (Vital v) { return v.name == "Thirst"; }).value += VITAL_RESTORE;
                StartCoroutine(consume());
                consuming = true;
            }
        }
        //Debug.Log("Priority: " + priorities.Find(delegate (Vital v) { return v.name == "Thirst"; }).priority);
    }

    //Breeding
    private void keen()
    {

    }
    private void searchForMate()
    {

    }
    private void breed()
    {

    }

    //Alerted
    private void alerted()
    {

    }
    private void attackEnemy()
    {

    }
    protected void fleeEnemy()
    {
        wander.enabled = true;
        SetBillboardState(2);
        maxSpeed = 7;
        maxAccel = 21;

        Vector3 pos = transform.position;
        float smallestDist = 99999;
        foreach (Transform dino in FOV.visibleTargets)
        {
            if (dino.tag == "Rapty")
            {
                float dist = Vector3.Distance(transform.position, dino.transform.position);
                if (dist < smallestDist)
                {
                    pos = dino.position;
                    smallestDist = dist;
                }
            }
        }

        Vector3 direction = transform.position - pos;
        fleeTarget.transform.position = transform.position + direction;
        wander.target = fleeTarget;
        
    }

    //Tired
    private void tired()
    {

    }
    private void rest()
    {

    }

    //Dead
    private void dead()
    {
        SetBillboardState(6);
        wander.enabled = false;
        anim.SetBool("isDead", true);
        StartCoroutine(die());
    }

    //Default
    private void wanderAround()
    {
        SetBillboardState(6);
        if (gameObject.GetComponent<Wandering>().enabled == false)
        {
            gameObject.GetComponent<Wandering>().enabled = true;
            wander.target = group;
        }
    }

    //Extras
    public void SetBillboardState(int ID)
    {
        if (ID < 6)
        {
            billboard.color = colour;
            billboard.texture = billboards[ID];
        }
        else
        {
            billboard.color = clear;
        }
    }

    private void spawnSpheres()
    {
        heightSpheres = new GameObject();
        heightSpheres.name = "Height Check Spheres";
        for (int i = 0; i < HEIGHT_SAMPLES; i++)
        {
            heightCheckSpheres[i] = new GameObject();
            heightCheckSpheres[i].transform.position = new Vector3(0, 100, 0);
            heightCheckSpheres[i].name = i.ToString();
            heightCheckSpheres[i].transform.SetParent(heightSpheres.transform);
            heightCheckSpheres[i].AddComponent<Agent>();
        }
    }

    private Vector3 GetCoordOnCircle(int radius, float angle)
    {
        Vector3 coord = new Vector3(radius * Mathf.Sin(angle), 0, radius * Mathf.Cos(angle));
        return coord;
    }
    private void moveTowards(GameObject target)
    {
        wander.target = target;
    }

    IEnumerator consume()
    {
        float timetoEnd = Time.timeSinceLevelLoad + 2.0f;
        anim.SetBool("isConsuming", true);
        while (Time.timeSinceLevelLoad <= timetoEnd)
        {
            yield return null;
        }
        anim.SetBool("isConsuming", false);
        consuming = false;
    }
    IEnumerator identifyResources()
    {
        identifyingResources = true;
        findGrass();
        float timetoEnd = Time.timeSinceLevelLoad + 2.0f;
        while (Time.timeSinceLevelLoad <= timetoEnd)
        {
            yield return null;
        }
        identifyingResources = false;
    }
    protected IEnumerator flee()
    {
        brain.pushState(fleeEnemy);
        float timetoEnd = Time.timeSinceLevelLoad + 10.0f;
        while (Time.timeSinceLevelLoad <= timetoEnd)
        {
            yield return null;
        }
        if (brain.Count() > 1)
            brain.popState();
        else
            brain.pushState(wanderAround);
        maxSpeed = 6;
        maxAccel = 18;
        wander.target = group;
    }

    protected IEnumerator die()
    {
        float timetoEnd = Time.timeSinceLevelLoad + 10.0f;
        while (Time.timeSinceLevelLoad <= timetoEnd)
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (brain.getCurrentState() == searchForFood || brain.getCurrentState() == eatFood)
        {
            for (int i = 0; i < foodSamples.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(foodSamples[i], new Vector3(2, 1, 2));
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(foodTarget.transform.position, new Vector3(3, 3, 3));
        }

        if (brain.getCurrentState() == searchForWater || brain.getCurrentState() == goToWater || brain.getCurrentState() == drinkWater)
        {
            for (int i = 0; i < heightCheckSpheres.Length; i++)
            {
                if(heightCheckSpheres[i].transform.position == waterTarget.transform.position)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere(heightCheckSpheres[i].transform.position, 5);
                }
                else
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(heightCheckSpheres[i].transform.position, 1);
                }
            }
        }
    }

    private void initPriorities()
    {
        priorities.Add(new Vital("Hunger", Random.Range(50.0f, 100.0f), searchForFood));
        priorities.Add(new Vital("Thirst", Random.Range(50.0f, 100.0f), searchForWater));
        //priorities.Add(new Vital("Hunger", 10.0f, searchForFood));
        //priorities.Add(new Vital("Thirst", 200.0f, searchForWater));
        //priorities.Add(new Vital("Energy", 100.0f, searchForFood));
        //priorities.Add(new Vital("Health", 100.0f, searchForFood));
    }

    public FSM.StateDelegate getCurrentState () { return brain.getCurrentState(); }
}
