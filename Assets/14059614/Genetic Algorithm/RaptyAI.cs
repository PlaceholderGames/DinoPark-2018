using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;
public class RaptyAI : MonoBehaviour
{
    public StateMachine<RaptyAI> stateMachine { get; set; }
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
    [HideInInspector]
    public float Health, Weight, Hunger, Age, Speed;
    int MaxHealth, MaxWeight, MaxHunger, MaxAge;
    int MinHealth, MinWeight, MinHunger, MinAge;
    bool Healthy = false, Fit = false, Hungry = false;

    //game time
    float timer = 0.0f;

    //targets
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject ankylosaurus;
    [HideInInspector]
    public GameObject target;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        wander = gameObject.GetComponent<Wander>();
        vision = gameObject.GetComponent<FieldOfView>();
        astar = gameObject.GetComponent<AStarSearch>();
        path = gameObject.GetComponent<ASPathFollower>();
        seek = gameObject.GetComponent<Seek>();
        flee = gameObject.GetComponent<Flee>();
        arrive = gameObject.GetComponent<Arrive>();
        terrain = GameObject.Find("Terrain");


        Statistics();
        stateMachine = new StateMachine<RaptyAI>(this);
        //stateMachine.ChangeState(Idle.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0)
        {
            //Debug.Log("Time: " + Mathf.RoundToInt(timer) + "s");

            Hunger += 1.0f * Time.deltaTime;
        }
        stateMachine.Update(); //update states every frame
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

        RNG(Health, MinHealth, MaxHealth);
        RNG(Weight, MinWeight, MaxWeight);
        RNG(Hunger, MinHunger, MaxHunger);
        RNG(Age, MinAge, MaxAge);
    }

    void RNG(float stat, int min, int max)
    {
        stat = Random.Range(min, max);
    }
    void Die()
    {
        Destroy(gameObject);
    }
    void BodyChecker()
    {

        if (Hunger >= 90)
        {
            Health -= 0.5f * Time.deltaTime;
            if (Hunger >= MaxHunger)
            {
                Hunger = MaxHunger;
            }
            else if (Hunger <= MinHunger)
            {
                Hunger = MinHunger;
            }
        }
        if (Weight > MaxWeight || Weight < MinWeight)
        {
            Health -= 0.5f * Time.deltaTime;
            if (Weight <= 0)
            {
                Weight = 0;
            }
        }
        if (Hunger > MaxHunger) Health -= 0.5f * Time.deltaTime;
        if (Age > MaxAge) Die();
        if (Health <= 0) Die();
    }
    void Growth()
    {
        if (Health >= MaxHealth * 0.7)  Healthy = true;
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
    /*
    void SwapState()
    {
        if (Hungry == true)
        {
            stateMachine.ChangeState(Eat.Instance);
            if (Vector3.Distance(transform.position, ankylosaurus.transform.position) <= 20.0f)
            {
                stateMachine.ChangeState(Hunt.Instance);
            }
        }
        if (Vector3.Distance(transform.position, ankylosaurus.transform.position) <= 20.0f)      //attack even when not hungry
        {                                                                                        //
            stateMachine.ChangeState(Hunt.Instance);                                             //
        }
        if (Age >= 6 && Healthy == true && Fit == true && Hungry == false)
        {
            stateMachine.ChangeState(Breed.Instance); //assumed that they already mated
        }
        else
        stateMachine.ChangeState(Idle.Instance);
    }
    */
}

