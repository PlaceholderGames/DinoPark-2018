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

    public Transform Raptylocation;
    public BoxCollider colBox;

    // Use this for initialization
    protected override void Start()
    {

        colBox = GetComponent<BoxCollider>();

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
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller
        
        base.Start();
    }

    protected override void Update()
    {
        // Idle - should only be used at startup
        //location data of Rapty for use with Anky hunting
        anim.SetFloat("RaptyX", Raptylocation.position.x);
        anim.SetFloat("RaptyY", Raptylocation.position.y);
        anim.SetFloat("RaptyZ", Raptylocation.position.z);

        //Idle is set as default but the raptor must hunt to win
        //the state is transtioned to "isHunting" for rapty optimal use.
        if (anim.GetBool("isIdle") == true)
        {

            anim.SetBool("isIdle", false);
            anim.SetBool("isHunting", true);

        }
        // Eating - requires a box collision with a dead dino
        while (anim.GetBool("isHunting"))
        {
            if (true)
            {

            }
        }
        // Drinking - requires y value to be below 32 (?)

        // Alerted - up to the student what you do here

        // Hunting - up to the student what you do here

        // Fleeing - up to the student what you do here

        // Dead - If the animal is being eaten, reduce its 'health' until it is consumed
        if (anim.GetBool("isDead") == true)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isHunting", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);

            Destroy(gameObject);
        }

        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    private void OnCollisionEnter(Collision colBox)
    {
        if (colBox.gameObject.tag == "deadAnky")
        {
            anim.SetBool("isEating", true);
            anim.SetBool("isHunting", false);
            Destroy(colBox.gameObject);
        }

        else if (colBox.gameObject.tag == "Anky")
        {
            anim.SetBool("isAttacking", true);
            anim.SetBool("isHunting", false);
            Debug.Log("Rapty is attacking an Anky");
        }
    }
}
