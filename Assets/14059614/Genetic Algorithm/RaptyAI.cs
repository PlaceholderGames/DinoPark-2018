using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;
public class RaptyAI : DinoAI
{
    public StateMachine<RaptyAI> stateMachine { get; set; }

    //Raptor's statistic

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
        Statistics(1);
        stateMachine = new StateMachine<RaptyAI>(this);
        stateMachine.ChangeState(Idle.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        BodyChecker(1);
        stateMachine.Update(); //update states every frame
    }

    void Growth()
    {
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
        if (Age >= 6 && Healthy == true && Fit == true && Hungry == false)
        {
            stateMachine.ChangeState(Breed.Instance); //assumed that they already mated
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

