using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FSM;
public class MyAnky : Agent
{
    public StateMachine<MyAnky> stateMachine { get; set; }
    [HideInInspector]
    public GameObject terrain;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Wander wander;
    [HideInInspector]
    public FieldOfView vision;
    [HideInInspector]
    public AStarSearch astar;
    [HideInInspector]
    public ASPathFollower path;
    [HideInInspector]
    public Seek seek;
    [HideInInspector]
    public Flee flee;
    [HideInInspector]
    public Arrive arrive;

    //Anky's statistic
    public float Health, Weight, Hunger, Thirst, Age, Speed;
    bool Healthy = false, Fit = false, Hungry = false, Thirsty = false;

    float time = 0;

    [HideInInspector]
    public MyRapty[] raptor;
    [HideInInspector]
    public GameObject closestRaptor;
    public GameObject food;
    public GameObject water;
    [HideInInspector]
    public GameObject rapty;

    public enum ankyState
    {
        IDLE,       // The default state on creation.
        EATING,     // This is for eating depending on y value of the object to denote grass level
        DRINKING,   // This is for Drinking, depending on y value of the object to denote water level
        ALERTED,      // This is for hightened awareness, such as looking around
        GRAZING,    // Moving with the intent to find food (will happen after a random period)
        ATTACKING,  // Causing damage to a specific target
        FLEEING,     // Running away from a specific target
        DEAD
    };


    // Use this for initialization
    protected override void Start()
    {
        Statistics();
        raptor = GameObject.FindObjectsOfType<MyRapty>();
        animator = GetComponent<Animator>();

        animator.SetBool("isIdle", true);
        animator.SetBool("isEating", false);
        animator.SetBool("isDrinking", false);
        animator.SetBool("isAlerted", false);
        animator.SetBool("isGrazing", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isFleeing", false);
        animator.SetBool("isDead", false);
        animator.SetFloat("speedMod", 1.0f);
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller
        base.Start();
    }

    protected override void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            Hunger += 1 * Time.deltaTime;
            Thirst += 2 * Time.deltaTime;
        }
        BodyChecker();
        Growth();
        Swapstate();
        if (animator.GetBool("isGrazing"))
        {
            gameObject.GetComponent<Wander>().enabled = true;
            Weight -= 0.4f;
        }
        else if (!animator.GetBool("isGrazing"))
        {
            Debug.Log("OK");
            gameObject.GetComponent<Wander>().enabled = false;
        }
        // Idle - should only be used at startup

        // Eating - requires a box collision with a dead dino

        // Drinking - requires y value to be below 32 (?)

        // Alerted - up to the student what you do here

        // Hunting - up to the student what you do here

        // Fleeing - up to the student what you do here

        // Dead - If the animal is being eaten, reduce its 'health' until it is consumed
        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }
    void Statistics()
    {
        Health = 120;
        Weight = 60;
        Hunger = 0;
        Thirst = 0;
        Age = 1;

        Speed = 5;
    }

    void BodyChecker()
    {
        if (Health >= 80) Healthy = true;
        else if (Health < 40) Healthy = false;

        if (Weight <= 90 && Weight >= 50) Fit = true;
        else if (Weight < 50 || Weight > 90) Fit = false;

        if (Hunger <= 30) Hungry = false;
        else if (Hunger > 50) Hungry = true;

        if (Thirst <= 20) Thirsty = false;
        else if (Thirst > 40) Thirsty = true;
    }
    void Growth()
    {
        if (Healthy == true && Fit == true && Hungry == false && Thirsty == false)
        {
            Age++;
            Health += (Health * 0.1f);
            Weight += (Weight * 0.1f);
            Hunger += (Hunger * 0.2f);
            Thirst += (Thirst * 0.2f);
        }
    }


    void Swapstate()
    {
        if (Thirsty == true && !animator.GetBool("isEating"))
        {
            /*
            gameObject.GetComponent<Seek>().enabled = true;
            animator.SetBool("isDrinking", true);
            if (Vector3.Distance(transform.position, water.transform.position) <= 0.5f)
            {
                Destroy(water);
                Hunger -= 30;
                gameObject.GetComponent<Seek>().enabled = false;
                animator.SetBool("isDrinking", false);
            }
            */
        }
        if (Hungry == true && !animator.GetBool("isDrinking"))
        {
            gameObject.GetComponent<Seek>().enabled = true;
            animator.SetBool("isEating", true);

            if (Vector3.Distance(transform.position, food.transform.position) <= 0.5f)
            {
                Destroy(food);
                Hunger -= 50;
                gameObject.GetComponent<Seek>().enabled = false;
                animator.SetBool("isEating", false);
            }
        }
        if (Vector3.Distance(transform.position, rapty.transform.position) <= 20.0f) //
        {
            //detect if raptors are nearby but should be using Field of View instead
            animator.SetBool("isAlert", true);
        }

    }


}
