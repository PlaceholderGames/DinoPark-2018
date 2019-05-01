
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;
public class RaptyAI : MonoBehaviour
{
    public StateMachine<RaptyAI> stateMachine { get; set; }

    //Raptor's statistic
    float Health, Weight, Hunger, Age, Speed;
    int MaxHealth, MaxWeight, MaxHunger, MaxAge;
    int MinHealth, MinWeight, MinHunger, MinAge;
    int Gender;
    bool Healthy = false, Fit = false, Hungry = false;

    //game time
    float timer = 0.0f;
    // Use this for initialization
    void Start()
    {
        Statistics();
        stateMachine = new StateMachine<RaptyAI>(this);
        stateMachine.ChangeState(Idle.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1.0f * Time.deltaTime;
        if (timer > 0)
        {
            Debug.Log("Time: " + Mathf.RoundToInt(timer) + "s");
        }
        stateMachine.Update(); //update states every frame
    }

    void FixedUpdate()
    {
        Vision();
    }

    void Vision()
    {
        int sightDist = 10;
        Ray FrontRay = new Ray(transform.position, transform.forward * sightDist);
        

    }

    void Statistics()
    {
        RNG(Gender, 0, 2);
        //0 = female - faster and be hungry slower
        //1 = male - stronger in health and heavier
        switch (Gender)
        {
            case 0: //female
                MinHealth = 40;
                MinWeight = 75;
                MinHunger = 30;
                MinAge = 1;

                MaxHealth = 100;
                MaxWeight = 115;
                MaxHunger = 70;
                MaxAge = 10;

                RNG(Health, MinHealth, MaxHealth);
                RNG(Weight, MinWeight, MaxWeight);
                RNG(Hunger, MinHunger, MaxHunger);
                RNG(Age, MinAge, MaxAge);
                Speed = 3.0f;
                break;
            case 1: //male
                MinHealth = 60;
                MinWeight = 90;
                MinHunger = 45;
                MinAge = 1;

                MaxHealth = 120;
                MaxWeight = 130;
                MaxHunger = 85;
                MaxAge = 10;

                RNG(Health, MinHealth, MaxHealth);
                RNG(Weight, MinWeight, MaxWeight);
                RNG(Hunger, MinHunger, MaxHunger);
                RNG(Age, MinAge, MaxAge);
                Speed = 2.5f;
                break;
        }
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
        if (Hunger >= 90) Health -= 0.5f * Time.deltaTime;
        if (Weight > MaxWeight || Weight < MinWeight) Health -= 0.5f * Time.deltaTime;
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
        if (Hungry == true) stateMachine.ChangeState(Food.Instance);
    }
    void PrintStats()
    {
        Debug.Log(Mathf.RoundToInt(Health));
        Debug.Log(Mathf.RoundToInt(Weight));
        Debug.Log(Mathf.RoundToInt(Hunger));
        Debug.Log(Mathf.RoundToInt(Age));
        Debug.Log(Mathf.RoundToInt(Speed));
    }
}

