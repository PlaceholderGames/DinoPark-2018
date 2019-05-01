using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyAnky : Agent
{
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

    public Animator anim;
    public float maxHealth = 100.0f;
    public float health;
    public int dangerCount = 0;
    public bool foodGone = false;
    public int prevState;
    //going to be used in dead state to give rapty extra health for eating a dead anky
    public bool dead = false;

    public int raptyChasing = -1;

    public GameObject alpha = null;
    // Use this for initialization
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        // Assert default animation booleans and floats
        //numbers are used to keep track of previous state
        anim.SetBool("isIdle", true); //0
        anim.SetBool("isEating", false); //1
        anim.SetBool("isDrinking", false); //2
        anim.SetBool("isAlerted", false); //3
        anim.SetBool("isGrazing", false); //4
        anim.SetBool("isAttacking", false); //5
        anim.SetBool("isFleeing", false); //6
        anim.SetBool("isDead", false); //7
        anim.SetFloat("speedMod", 1.0f);
        health = maxHealth;
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller
        base.Start();

    }

    protected override void Update()
    {
        
        //lowering ankys health on update
        health = health - 0.001f;
        //Debug.Log(health);

        // Idle - should only be used at startup
        
        // Eating - requires a box collision with food source

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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "AnkyFood")
        {
            health = health + 5.0f;
            foodGone = true;
        }   
    }
}
