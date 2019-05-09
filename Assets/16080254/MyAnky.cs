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
    float health = 100;
    float hunger = 60;
    float thirst = 100;
    const int METERTHRESHOLD = 50;

    ankyState CurrentState; //Used as part of the statecheck switch where the anim bools are updated to reflect the current state of the anky

    AStarSearch aStarScript;
    PathFollower PathFollowScript;

    // Use this for initialization
    protected override void Start()
    {
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
        anim.SetFloat("health", health);
        anim.SetFloat("hunger", hunger);
        anim.SetFloat("thirst", thirst);

        CurrentState = ankyState.IDLE;

        aStarScript = GetComponent<AStarSearch>();
        PathFollowScript = GetComponent<PathFollower>();
        
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller
        base.Start();

    }

    protected override void Update()
    {
        //Path finding setup
        //if (PathFollowScript.path.nodes.Count < 1 || PathFollowScript.path == null) //If there's no path
        //    PathFollowScript.path = aStarScript.path; //set to the path created by the a star script

        //DEBUG
        hunger -= 1 * Time.deltaTime;

        // Idle - should only be used at startup
        if (anim.GetBool("isIdle") == true)
        {
            if (thirst < METERTHRESHOLD || hunger < METERTHRESHOLD)
                CurrentState = ankyState.GRAZING;
        }



        // Eating - requires to be within grass collider
        if(anim.GetBool("isEating") == true)
        {
            Debug.Log("Anky: Eating Grass");
            hunger += 1 * Time.deltaTime;
        }

        // Drinking - requires y value to be below 32 (?)
        if (anim.GetBool("isDrinking") == true)
        {
            Debug.Log("Anky: Drinking Water");
            thirst += 1 * Time.deltaTime;
        }

        // Alerted - up to the student what you do here

        if (anim.GetBool("isAlerted") == true) //What the anky does while in the alerted state
        {
            //via "RaptorCloseCheck", in alert state, will be more aware of any possible raptors sneaking up to anky
        }

        // Hunting - up to the student what you do here
        if (anim.GetBool("isGrazing") == true)
        {
            if (hunger < METERTHRESHOLD)
                PathFollowScript.target = FindClosestGrass();
            if (thirst < METERTHRESHOLD)
                PathFollowScript.target = FindClosestWater();

           // move(PathFollowScript.getdirectionvector());
        }
        // Fleeing - up to the student what you do here
        if(anim.GetBool("isFleeing") == true)
        {
            //Code for running away from rapty
        }

        // Dead - If the animal is being eaten, reduce its 'health' until it is consumed
        if (anim.GetBool("isDead") == true)
        {
            //code to stop anky from moving and basically be a corpse for the raptors to eat
        }

        //Update animation floats to match actual floats
        anim.SetFloat("health", health);
        anim.SetFloat("hunger", hunger);
        anim.SetFloat("thirst", thirst);

        StateCheck();
        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    int StateCheck() //Checks to see what the current state of the anky is supposed to be and updates all anim bools to reflect the state.
    {
        if (CurrentState == ankyState.IDLE)
        {
            Debug.Log("Anky state is IDLE");
            anim.SetBool("isIdle", true);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            return 0;
        }

        if (CurrentState == ankyState.EATING)
        {
            Debug.Log("Anky state is EATING");
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", true);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            return 0;
        }

        if (CurrentState == ankyState.DRINKING)
        {
            Debug.Log("Anky state is DRINKING");
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", true);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            return 0;
        }

        if (CurrentState == ankyState.ALERTED)
        {
            Debug.Log("Anky state is ALERTED");
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", true);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            return 0;
        }

        if (CurrentState == ankyState.GRAZING)
        {
            Debug.Log("Anky state is GRAZING");
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            return 0;
        }

        if (CurrentState == ankyState.ATTACKING)
        {
            Debug.Log("Anky state is ATTACKING");
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", true);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", false);
            return 0;
        }

        if (CurrentState == ankyState.FLEEING)
        {
            Debug.Log("Anky state is FLEEING");
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", true);
            anim.SetBool("isDead", false);
            return 0;
        }

        if (CurrentState == ankyState.DEAD)
        {
            Debug.Log("Anky state is DEAD");
            anim.SetBool("isIdle", false);
            anim.SetBool("isEating", false);
            anim.SetBool("isDrinking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isGrazing", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFleeing", false);
            anim.SetBool("isDead", true);
            return 0;
        }
        return 0;
    }

    public GameObject FindClosestGrass() //Collects all grass areas and looks for which one the anky is closest to
    {
        GameObject[] grassArray;
        grassArray = GameObject.FindGameObjectsWithTag("Grass");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject g in grassArray)
        {
            Vector3 diff = g.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = g;
                distance = curDistance;
            }
        }
        return closest;
    }

    public GameObject FindClosestWater() //Collects all water areas and looks for which one the anky is closest to
    {
        GameObject[] WaterArray;
        WaterArray = GameObject.FindGameObjectsWithTag("Water");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject w in WaterArray)
        {
            Vector3 diff = w.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = w;
                distance = curDistance;
            }
        }
        return closest;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water") //If anky is in water area, then set state to drinking
        {
            Debug.Log("Anky: Is on water");
            CurrentState = ankyState.DRINKING;
        }

        if (collision.gameObject.tag == "Grass") //If anky is in grass area, then set state to eating
        {
            Debug.Log("Anky: Is on grass");
            CurrentState = ankyState.EATING;
        }
    }

    void RaptorCloseCheck() //checks if there is a raptor close to anky
    {
        GameObject[] RaptorArray;
        RaptorArray = GameObject.FindGameObjectsWithTag("Rapty");
        float alertDistance = 50;
        float fleeDistance = 20;
        Vector3 position = transform.position;
        foreach (GameObject r in RaptorArray)
        {
            Vector3 diff = r.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < alertDistance) //if distance from anky to a rapty is less than 50...
            {
                if(curDistance < fleeDistance) //Check if rapty is close enough to anky to cause fleeing if already alerted
                {
                    CurrentState = ankyState.FLEEING;
                }
                else
                    CurrentState = ankyState.ALERTED;
            }
        }
    }
}

    //void move(Vector3 directionVector)
    //{
    //    directionVector *= maxSpeed * Time.deltaTime;

    //    transform.Translate(directionVector, Space.World);
    //    transform.LookAt(transform.position + directionVector);

    //}
