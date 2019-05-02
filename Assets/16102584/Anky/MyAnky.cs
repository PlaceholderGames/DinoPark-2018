using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MyAnky : Agent
{
    static int HUNGRY_LEVEL = 25;
    static int PARCHED_LEVEL = 25;
    static int WALK_SPEED = 10;
    static int RUN_SPEED = 20;
    static int HEIGHT_SAMPLES = 10;
    static float WATER_HEIGHT = 35.28f;

    public enum ankyState
    {
        //With billboard textures:
        ALERTED,    // This is for hightened awareness, such as looking around
        ATTACKING,  // Causing damage to a specific target
        FLEEING,    // Running away from a specific target
        HUNGRY,     // Alerts herd that object is hungry
        KEEN,       // Ready for mating
        THIRSTY,    // Alerts herd that object is thirsty

        //Without:
        GRAZING,    // Moving with the intent to find food/water without alerting herd
        SEARCHING,  // Moving with herd to find food/water
        EATING,     // This is for eating depending on y value of the object to denote grass level
        DRINKING,   // This is for Drinking, depending on y value of the object to denote water level
        DEAD
    };

    private enum ankyLabel
    {
        CHILD,
        ADULT,
        INJURED,
        ELDER,
        WARRIOR,
        LEADER
    };


    private Terrain terrain;
    private TerrainData terrainData;

    private AudioSource roar;
    private Animator anim;

    private RawImage billboard;
    private Vector4 clear, colour;
    private Texture2D[] billboards = new Texture2D [6];

    private ankyState currentState;
    private ankyLabel label;

    private FieldOfView FOV;
    private GameObject[] spheres = new GameObject[HEIGHT_SAMPLES];

    [SerializeField]
    //private AgentBehaviour target;

    private Wander wander;
    private Pursue pursue;

    #region Stats
    //Active values between 0 and determined maximum
    private float hunger = 50;
    private float thirst = 50;
    private float strength = 50;
    private float stamina = 50;
    private float health = 50;

    private int full = 100;
    private int quenched = 100;

    //Power in battle & Herd Ranking. Recovers very slowly over time.
    private int maxStrength = 100;
    //Recovers quickly after delay, reduces slowly whilst moving. When 0 reduces movement speed.
    private int maxStamina = 100;
    //Dies when 0. Recovers when hunger + thirst > 50%. Reduces when below 10%. 
    private int maxHealth = 100;

    //Getters
    public bool isHungry() { if (hunger < HUNGRY_LEVEL) { return true; } return false; }
    public bool isThirsty() { if (thirst < PARCHED_LEVEL) { return true; } return false; }
    #endregion

    // Use this for initialization
    protected override void Start()
    {
        wander = gameObject.GetComponent<Wander>();
        pursue = gameObject.GetComponent<Pursue>();

        for(int i = 0; i < HEIGHT_SAMPLES; i++)
        {
            spheres[i] = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            spheres[i].transform.position = new Vector3(0, 100, 0);
            spheres[i].AddComponent<Agent>();
        }

        //target = gameObject.GetComponent<AgentBehaviour>();

        pursue.target = spheres[0];

        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        terrainData = terrain.terrainData;

        FOV = gameObject.GetComponent<FieldOfView>();

        roar = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        currentState = ankyState.THIRSTY;

        clear = new Vector4(0, 0, 0, 0);
        colour = new Vector4(1, 1, 1, 0.6f);
        
        billboard = transform.Find("Billboard").Find("State").GetComponent<RawImage>();
        billboards[0] = Resources.Load<Texture2D>("Alerted");
        billboards[1] = Resources.Load<Texture2D>("Attacking");
        billboards[2] = Resources.Load<Texture2D>("Fleeing");
        billboards[3] = Resources.Load<Texture2D>("Hungry");
        billboards[4] = Resources.Load<Texture2D>("Keen");
        billboards[5] = Resources.Load<Texture2D>("Thirsty");
        SetBillboardState();

        // Assert default animation booleans and floats
        //anim.SetBool("isIdle", true);
        anim.SetBool("isGrazing", true);
        anim.SetBool("isEating", false);
        anim.SetBool("isDrinking", false);
        anim.SetBool("isAlerted", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isFleeing", false);
        anim.SetBool("isDead", false);
        anim.SetFloat("speedMod", 1.0f);
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller
        base.Start();
    }

    protected override void Update()
    {
        switch(currentState)
        {
            case ankyState.ALERTED:
                {
                    roar.Play(0);
                    break;
                }
            case ankyState.ATTACKING:
                {
                    roar.Play(0);
                    break;
                }
            case ankyState.FLEEING:
                {
                    
                    break;
                }
            case ankyState.HUNGRY:
                {
                    
                    break;
                }
            case ankyState.KEEN:
                {
                    roar.Play(0);
                    break;
                }
            case ankyState.THIRSTY:
                {
                    SearchForWater();
                    break;
                }
            case ankyState.GRAZING:
                {
                    break;
                }
            case ankyState.SEARCHING:
                {
                    
                    break;
                }
            case ankyState.EATING:
                {
                    
                    break;
                }
            case ankyState.DRINKING:
                {

                    break;
                }
            case ankyState.DEAD:
                {
                    roar.Play(0);
                    break;
                }
            default:
                print("Unknown Anky State!");
                break;
        }
        base.Update();



        if (health <= 0)
            currentState = ankyState.DEAD;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public void SetBillboardState()
    {
        if ((int)currentState < 6)
        {
            billboard.color = colour;
            billboard.texture = billboards[(int)currentState];
        }
        else
            billboard.color = clear;
    }

    private void SearchForFood()
    {

    }

    private void SearchForWater()
    {
        if (wander.enabled)
        {
            wander.enabled = false;
            pursue.enabled = true;
        }
        if(pursue.target.transform.position.y >= WATER_HEIGHT)
        {
            int angle = 0;

            float lowestHeight = 1000;
            int lowestSphere = 0;

            for (int i = 0; i < HEIGHT_SAMPLES; i++, angle += 36)
            {
                Vector3 position = GetCoordOnCircle(100, angle);
                position.x += transform.position.x;
                position.z += transform.position.z;
                position.y = terrain.SampleHeight(position);

                if (position.y < lowestHeight)
                {
                    lowestHeight = position.y;
                    lowestSphere = i;
                }

                spheres[i].transform.position = position;
            }

            pursue.target.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            pursue.target = spheres[lowestSphere];
            pursue.target.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
        }
        else
        {
            pursue.enabled = false;
        }
    }

    public void SetLabel (int idx)
    {
        //Updates label using index number
        label = (ankyLabel)idx;
        Debug.Log(label);
    }

    private Vector3 GetCoordOnCircle (int radius, float angle)
    {
        Vector3 coord = new Vector3(radius * Mathf.Sin(angle), 0, radius * Mathf.Cos(angle));
        return coord;
    }
}
