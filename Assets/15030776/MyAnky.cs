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

    public GameObject waterDetector = null; // Used as a target for the Anky to reach water to drink, won't be used until the Anky reaches the Drinking State.

    public float maxHealth = 100f;
    public float health;

    public float maxHunger = 100f;
    public float maxThirst = 100f;
    public float hunger;
    public float thirst;

    public float timeToLive = 120f; // Used as the max limit that the Anky lives, regardless of its thirst and hunger values.

    // Use this for initialization
    protected override void Start()
    {

        anim = GetComponent<Animator>();

        // Assert default animation booleans and floats
        anim.SetBool("isIdle", true);       //
        anim.SetBool("isEating", false);    //
        anim.SetBool("isDrinking", false);  //
        anim.SetBool("isAlerted", false);   // This with GetBool and GetFloat allows
        anim.SetBool("isGrazing", false);   // you to see how to change the flag parameters in the animation controller
        anim.SetBool("isAttacking", false); //
        anim.SetBool("isFleeing", false);   //
        anim.SetBool("isDead", false);      //
        anim.SetFloat("speedMod", 1.0f);    //

        health = maxHealth; // Health starts at max health.
        hunger = maxHunger; // Hunger starts at max hunger.
        thirst = maxThirst; // Thirst starts at max thirst.

        base.Start();

    }

    protected override void Update()
    {

        hunger = hunger - Time.deltaTime / 4;    // Hunger decreases every update/ over time.
        thirst = thirst - Time.deltaTime / 2;    // Thirst decreases every update/ over time.

        timeToLive = timeToLive - Time.deltaTime;   // The Anky's life timer decreases over time.

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

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Rapty")  //
        {                                   //
                                            // If the Anky's collision box hits the Rapty's collision box(es),
            health = health - 20;           // then the Anky takes this amount of damage on each consecutive hit/ update.
                                            //
        }                                   //

    }

}
