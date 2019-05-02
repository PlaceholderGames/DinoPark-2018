using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyAnky : Agent
{
    public enum AnkyState
    {
        IDLE,       // The default state on creation.
        EATING,     // This is for eating depending on y value of the object to denote grass level
        DRINKING,   // This is for Drinking, depending on y value of the object to denote water level
        ALERTED,    // This is for hightened awareness, such as looking around
        GRAZING,    // Moving with the intent to find food (will happen after a random period)
        ATTACKING,  // Causing damage to a specific target
        FLEEING,    // Running away from a specific target
        DEAD        // Dead
    };

    public Animator anim;
    private string CurrentCommand;
    
    // Use this for initialization
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        // Assert default animation booleans and floats
        //x BehaviourSwitch("isIdle");
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller
        //BehaviourSwitch("isGrazing");
        base.Start();
    }

    protected override void Update()
    {
        Debug.Log(CurrentBehaviour());

        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public void BehaviourSwitch(string command)
    {
        CurrentCommand = command;
        if (command == "isIdle")
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            //Debug.Log("Idle");
        }
        else if (command ==  "isEating")
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", true);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
           //Debug.Log("Eating");
        }
        else if (command == "isDrinking")
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", true);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            //Debug.Log("Drinking");
        }
        else if (command == "isAlerted")
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", true);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            //Debug.Log("Alerted");
        }
        else if (command == "isGrazing")
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            //Debug.Log("Grazing");
        }
        else if (command == "isAttacking")
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", true);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            //Debug.Log("Attacking");
        }
        else if (command == "isFleeing")
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", true);
            anim.SetBool("isDead", false);
            //Debug.Log("Fleeing");
        }
        else if (command == "isDead")
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", true);
            //Debug.Log("Dead");
        }
    }

    public string CurrentBehaviour()
    {
        return CurrentCommand;
    }
}
