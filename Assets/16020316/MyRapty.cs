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

    public float maxHealth = 100.0f;
    public float health;

    public float hunger = 50.0f;
    public float thirst = 50.0f;

    public bool recover = false;
    public bool dead = false;
    public bool foodGone = false;
    public int prevState;

    public GameObject alpha = null;

    public GameObject waterFinder = null;
    // Use this for initialization
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        // Assert default animation booleans and floats
        anim.SetBool("isIdle", true);
        anim.SetBool("isEating", false);
        anim.SetBool("isDrinking", false);
        anim.SetBool("isAlerted", false);
        anim.SetBool("isHunting", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isFleeing", false);
        anim.SetBool("isDead", false);
        health = hunger + thirst;
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller
        base.Start();
    }

    protected override void Update()
    {
        thirst = thirst - 0.001f;
        hunger = hunger - 0.002f;
        health = hunger + thirst;
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
        if (col.gameObject.tag == "Anky")
        {
            health = health - 2.0f;
            if (health == 0.0f)
            {
                dead = true;
            }
            else
            {
                if (col.gameObject.GetComponent<MyAnky>().dead)
                {
                    health = health + 20.0f;
                    Destroy(col.gameObject);
                }
                else
                {
                    health = health + 5.0f;
                }
                health = health + 5.0f;
                recover = true;
            }
        }
    }
}
