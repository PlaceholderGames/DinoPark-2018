using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

    //public & private variables needed for the additional improvments
    public Transform Ankylocation;
    public BoxCollider boxCol;


    // Use this for initialization
    protected override void Start()
    {
        //additional start up data for improvments
        Ankylocation = GetComponent<Transform>();
        boxCol = GetComponent<BoxCollider>();

        anim = GetComponent<Animator>();
        // Assert default animation booleans and floats
        anim.SetBool("isIdle", true);
        anim.SetBool("isEating", false);
        anim.SetBool("isDrinking", false);
        anim.SetBool("isAlerted", false);
        anim.SetBool("isGrazing", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isFleeing", false);
        anim.SetBool("isDead", false);
        anim.SetFloat("speedMod", 1.0f);
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller
        base.Start();

    }

    protected override void Update()
    {
        // Idle - should only be used at startup
        //location data needed for Rapty to find Anky
        anim.SetFloat("AnkyX", Ankylocation.position.x);
        anim.SetFloat("AnkyY", Ankylocation.position.y);
        anim.SetFloat("AnkyZ", Ankylocation.position.z);

        // Grazing - grazing state for the Anky to locate food
        if (anim.GetBool("isIdle") == true)
        {
            anim.SetBool("isGrazing", true);
            anim.SetBool("isIdle", false);
        }

        // Eating - requires a box collision with a dead dino
        //if the collision box of the Anky collides with the GrazingPos - grass, "isGrazing" state is true
        if (boxCol.gameObject.tag == "GrazingPos")
        {
            anim.SetBool("isEating", true);
            anim.SetBool("isGrazing", false);
        }
        // Drinking - requires y value to be below 32 (?)

        // Alerted - up to the student what you do here
        if (anim.GetBool("isAlerted") == true)
        {
            anim.SetBool("isAlerted", false);
            anim.SetBool("isFleeing", true);
        }
        // Hunting - up to the student what you do here
        if (anim.GetBool("isHunting") == true)
        {
            anim.SetBool("isHunting", false);
            anim.SetBool("isAlerted", true);
        }
        // Fleeing - up to the student what you do here
        if (anim.GetBool("isFleeing") == true)
        {
            set(Ankylocation);
        }
        // Dead - If the animal is being eaten, reduce its 'health' until it is consumed
        if (anim.GetBool("inDead") == true)
        {
            Destroy(boxCol.gameObject);
        }

        base.Update();
    }

    private void set(Transform ankylocation)
    {
        throw new NotImplementedException();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    private void OnCollisionEnter(Collision boxCol)
    {
        if (boxCol.gameObject.tag == "GrazingPos")
        {
            anim.SetBool("isEating", true);
            Ankylocation = boxCol.gameObject.Y;             //where Y is the Y value of the grass
            Debug.Log("Anky is Eating Grass");
            Destroy(boxCol.gameObject);
            anim.SetBool("isGrazing", false);
            if (anim.GetBool("GrazingPos") == false)
            {
                anim.SetBool("isHunting", true);
            }
        }
    }
}