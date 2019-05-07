using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyRapty : Agent
{

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

    private Animator anim;

    public GameObject waterDetector = null;

    public float maxHealth = 100f;
    public float health;

    public float maxHunger = 100f;
    public float maxThirst = 100f;
    public float hunger;
    public float thirst;

    public float timeToLive = 120f;

    public bool killed = false;

    // Use this for initialization
    protected override void Start()
    {

        anim = GetComponent<Animator>();    

        // Assert default animation booleans and floats
        anim.SetBool("isIdle", true);       //
        anim.SetBool("isEating", false);    //
        anim.SetBool("isDrinking", false);  //
        anim.SetBool("isAlerted", false);   // This with GetBool and GetFloat allows 
        anim.SetBool("isHunting", false);   // you to see how to change the flag parameters in the animation controller
        anim.SetBool("isAttacking", false); //
        anim.SetBool("isFleeing", false);   //
        anim.SetBool("isDead", false);      //

        health = maxHealth; // Health starts at max health.
        hunger = maxHunger; // Hunger starts at max hunger.
        thirst = maxThirst; // Thirst starts at max thirst.

        base.Start();

    }

    protected override void Update()
    {

        hunger = hunger - Time.deltaTime / 4;    // Hunger decreases every update.
        thirst = thirst - Time.deltaTime / 2;    // Thirst decreases every update.

        timeToLive = timeToLive - Time.deltaTime;

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

        Debug.Log("Colliding");

        if (col.gameObject.tag == "Anky")
        {
            

            if (health == 0.0f)
            {

                anim.SetBool("isDead", true);

            }
            else
            {

                if (col.gameObject.GetComponent<MyAnky>().health <= 0)
                {

                    hunger = hunger + 100;

                    Destroy(col.gameObject);

                }

            }

        }

    }

}
