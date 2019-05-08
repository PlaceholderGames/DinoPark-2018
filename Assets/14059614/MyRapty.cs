using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FSM;
public class MyRapty : Agent
{
    public StateMachine<MyRapty> stateMachine { get; set; }
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

    //Raptor's statistic
    public float Health, Weight, Hunger, Age, Speed;
    int MaxHealth, MaxWeight, MaxHunger, MaxAge;
    int MinHealth, MinWeight, MinHunger, MinAge;
    bool Healthy = false, Fit = false, Hungry = false;

    //game time
    float timer = 0.0f;

    public GameObject anky;
    

    public enum raptyState
    {
        IDLE,       // The default state on creation.
        EATING,     // This is for eating depending on location of a target object (killed prey)
        DRINKING,   // This is for Drinking, depending on y value of the object to denote water level
        ALERTED,      // This is for hightened awareness, such as looking around
        HUNTING,    // Moving with the intent to hunt
        ATTACKING,  // Causing damage to a specific target
        FLEEING,     // Running away from a specific target
        DEAD
    };

    // Use this for initialization
    protected override void Start()
    {
        Statistics();
        animator = GetComponent<Animator>();
        // Assert default animation booleans and floats
        animator.SetBool("isIdle", true);
        animator.SetBool("isEating", false);
        animator.SetBool("isDrinking", false);
        animator.SetBool("isAlerted", false);
        animator.SetBool("isHunting", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isFleeing", false);
        animator.SetBool("isDead", false);
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller


        base.Start();
        //stateMachine = new StateMachine<MyRapty>(this);
        //stateMachine.ChangeState(Idle.Instance);
        
    }

    protected override void Update()
    {
        BodyChecker();
        Growth();

        //SwapState();
        // Idle - should only be used at startup

        // Eating - requires a box collision with a dead dino

        // Drinking - requires y value to be below 32 (?)

        // Alerted - up to the student what you do here

        // Hunting - up to the student what you do here

        // Fleeing - up to the student what you do here

        // Dead - If the animal is being eaten, reduce its 'health' until it is consumed
        //stateMachine.Update();
        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    void Statistics()
    {

        MinHealth = 60;
        MinWeight = 90;
        MinHunger = 45;
        MinAge = 1;

        MaxHealth = 120;
        MaxWeight = 130;
        MaxHunger = 85;
        MaxAge = 10;

        Speed = 20;

        Health = Random.Range(MinHealth, MaxHealth);
        Weight = Random.Range(MinWeight, MaxWeight);
        Hunger = Random.Range(MinHunger, MaxHunger);
        Age = Random.Range(MinAge, MaxAge);
    }

    void BodyChecker()
    {
        if (Age > MaxAge) Health = 0;
    }
    void Growth()
    {
        if (Health >= MaxHealth * 0.7) Healthy = true;
        else if (Health < MaxHealth * 0.7) Healthy = false;

        if (Weight <= MaxWeight * 0.8 && Weight >= MaxWeight * 0.6) Fit = true;
        else if (Weight < MaxWeight * 0.6 || Weight > 0.8) Fit = false;

        if (Hunger <= MinHunger) Hungry = false;
        else if (Hunger > MinHunger) Hungry = true;

        if (Healthy == true && Fit == true && Hungry == false)
        {
            Age++;
            Health -= (Health * 0.1f);
            Weight -= (Weight * 0.1f);
            Hunger += (Hunger * 0.2f);
        }
    }
    void SwapState()
    {
        //Enter Hunt state
        if (Vector3.Distance(transform.position, anky.transform.position) <= 20.0f )
        {
            //stateMachine.ChangeState(Hunt.Instance);
        }
        /*
        MyAnky[] anky = GameObject.FindObjectsOfType<MyAnky>();
        MyAnky ankyTarget = anky[0];
        
        if (anky.Length >= 1)
        {
            for (int i = 1; i < anky.Length; i++)
            {
                if (Vector3.Distance(anky[i].transform.position, transform.position) < Vector3.Distance(ankyTarget.transform.position, transform.position))
                {
                    ankyTarget = anky[i];
                }
            }
        }*/
        
        if (Health <= 0)
        {
            //stateMachine.ChangeState(Dead.Instance);
        }
    }
}
