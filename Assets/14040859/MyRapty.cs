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
        ALERTING,   //Alerting nearby Raptors
        ALERTED,      // This is for hightened awareness, such as looking around
        HUNTING,    // Moving with the intent to hunt
        ATTACKING,  // Causing damage to a specific target
        FLEEING,     // Running away from a specific target
        DEAD        //Ded
    };
    private Animator anim;
    private Transform raptyLoc;
    public BoxCollider col;
    public Face raptyTarget1;
    public Pursue raptyTarget2;
    // Use this for initialization
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        raptyLoc = GetComponent<Transform>();
        col = GetComponent<BoxCollider>();
        raptyTarget1 = GetComponent<Face>();
        raptyTarget2 = GetComponent<Pursue>();
        // Assert default animation booleans and floats
        anim.SetBool("isIdle", true);
        anim.SetBool("isEating", false);
        anim.SetBool("isDrinking", false);
        anim.SetBool("isAlerting", false);
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
        anim.SetFloat("raptyX", raptyLoc.position.x);
        anim.SetFloat("raptyY", raptyLoc.position.y);
        anim.SetFloat("raptyZ", raptyLoc.position.z);

        if (anim.GetBool("isHunting") == true)
        {
            GetComponent<Wander>().enabled = true;
          //  GetComponent<Pursue>().enabled = true;
        }
      
        if(raptyTarget1.target == null && raptyTarget2.target == null)
        {
            raptyTarget1.target = GameObject.FindWithTag("DeadRapty");
            raptyTarget2.target = GameObject.FindWithTag("DeadRapty");
        }

        if (anim.GetBool("isDead") == true)
        {
            GetComponent<Wander>().enabled = false;
            GetComponent<Pursue>().enabled = false;
            GetComponent<Face>().enabled = false;
            GetComponent<MyRapty>().enabled = false;
            GetComponent<Agent>().enabled = false;
            raptyLoc.Rotate(0.0f, 0.0f, 90.0f);
        }

        if (anim.GetBool("isDrinking") == true)
        {
           GetComponent<Wander>().enabled = false;
        }
        
        if (anim.GetFloat("raptyDecay") < 0.0f)
        {
            Destroy(gameObject);
        }

        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "DeadAnky")
        {
            anim.SetBool("isEating", true);
            anim.SetBool("isHunting", false);
            GetComponent<Wander>().enabled = false;
            GetComponent<Pursue>().enabled = false;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Anky")
        {
            anim.SetBool("isAttacking", true);
            Debug.Log("Rapty Attacks an Anky");
        }

        if (col.gameObject.tag == "WaterSource")
        {
            anim.SetBool("isHunting", false);
            GetComponent<Wander>().enabled = false;
            GetComponent<Pursue>().enabled = false;
            anim.SetBool("isDrinking", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        anim.SetBool("isHunting", true);
    }
}
