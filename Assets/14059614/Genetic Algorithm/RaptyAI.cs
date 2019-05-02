using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;
public class RaptyAI : MonoBehaviour
{
    public StateMachine<RaptyAI> stateMachine { get; set; }

    //Raptor's statistic
    [HideInInspector]
    public float Health, Weight, Hunger, Age, Speed;
    int MaxHealth, MaxWeight, MaxHunger, MaxAge;
    int MinHealth, MinWeight, MinHunger, MinAge;
    int Gender;
    bool Healthy = false, Fit = false, Hungry = false;

    //game time
    float timer = 0.0f;
    float dayTime = 0.0f;
    float nightTime = 0.0f;
    bool day, night;

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
        day = true;
        night = false;
        Statistics();
        stateMachine = new StateMachine<RaptyAI>(this);
        stateMachine.ChangeState(Idle.Instance);
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

        Speed = Age + 1;

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
    void SwapState()
    {
        if (Hungry == true)
        {
            stateMachine.ChangeState(Food.Instance);
            if (Vector3.Distance(transform.position, player.transform.position) <= 20.0f ||
                Vector3.Distance(transform.position, ankylosaurus.transform.position) <= 20.0f)
            {
                stateMachine.ChangeState(Hunt.Instance);
            }
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= 20.0f ||          //
            Vector3.Distance(transform.position, ankylosaurus.transform.position) <= 20.0f)      //attack even when not hungry
        {                                                                                        //
            stateMachine.ChangeState(Hunt.Instance);                                             //
        }
        else
        stateMachine.ChangeState(Idle.Instance);
    }
    void PrintStats()
    {
        //Debug.Log(Mathf.RoundToInt(Health));
        //Debug.Log(Mathf.RoundToInt(Weight));
        //Debug.Log(Mathf.RoundToInt(Hunger));
        //Debug.Log(Mathf.RoundToInt(Age));
        //Debug.Log(Mathf.RoundToInt(Speed));
    }
}

