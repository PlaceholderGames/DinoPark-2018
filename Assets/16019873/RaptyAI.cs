using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyAI : MonoBehaviour
{
    //creating a variable to be able to access the conditions set in the Unity Animator Controller
    Animator animator;
    //linked to the dino
    public GameObject thisDino;
    public GameObject opponent;
    public GameObject claws;
    public GameObject alpha;
    public GameObject Cube3;   //rapty's body
    //there is one Alpha male raptor that will control the rest when in the hunting stage
    //the rest will follow his commands
    public ASAgentInstance ASagent;
    public ASPathFollower followingDino;
    //A star path finding
    public AStarSearch aStarVar;
    public GameObject waterLocation;
    //alpha male variables
    public bool alphaRapty = false;
    public bool returnToAlphaRapty = false;
    //current hunger/thirst values
    public float currentHunger = 100;
    public float currentThirst = 100;
    public float currentHealth = 100;
    //values for enabling and disabling scripts in run time
    public FieldOfView fov;
    public Wander wander;
    public Pursue pursue;
    public Face face;

    //work out the distance between one dino to another
    public GameObject getDino()
    {
        return thisDino;
    }

    //work out the distance between one dino to another (the alpha one)
    public GameObject getAlpha()
    {
        return alpha;
    }

    //work out the distance between one dino to another
    public GameObject dieDino()
    {
        //https://learn.unity.com/tutorial/destroy-i?projectId=5c8920b4edbc2a113b6bc26a#5c8a6146edbc2a001f47d5c6 - to destroy within 3 sec
        //will stop rendering which means the thing will be removed from the scene only in runtime
        //not from the list of objects
        if (animator.GetBool("deadDino") == true)
        {
            Destroy(GetComponent<MeshRenderer>(), 3f);
        }
        return thisDino;
    }

    //this function is going to instantiate 'claws' object from a prefab
    //from the hands of the dino (a cube)
    //and pushes it in the rotation that has been chosen and the strength of the attack
    void scratching()
    {
        GameObject b = Instantiate(claws, Cube3.transform.position, Cube3.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(Cube3.transform.forward * 500);
    }


    //cancelling the invoke of the attaching method
    public void stopScratching()
    {
        CancelInvoke("Claws");
    }


    //this gets called every half-second
    //make the double(float) less to get it faster
    public void startScratching()
    {
        InvokeRepeating("Claws", 0.3f, 0.3f);
    }

    //use this for initialisation
    private void Start()
    {
        fov = thisDino.GetComponent<FieldOfView>();
        pursue = thisDino.GetComponent<Pursue>();
        wander = thisDino.GetComponent<Wander>();
        face = thisDino.GetComponent<Face>();
        ASagent = thisDino.GetComponent<ASAgentInstance>();
        //setting it to get data from the FSM created in Unity Animator
        animator = GetComponent<Animator>();
        //make other raptys follow the alpha dino
        aStarVar = GetComponent<AStarSearch>();
        followingDino = GetComponent<ASPathFollower>();

        //at the start of the game, dino got full health/hunger/thirst bar
        currentHealth = DinoBaseClass.maxHealth;
        currentHunger = DinoBaseClass. maxHunger;
        currentThirst = DinoBaseClass.maxThirst;

    }

    //update is called once per frame
    private void Update()
    {
        //make the dino start facing the target if in hunting mode,
        //(which will assume that his hunger is below 50
        if (animator.GetFloat("hunger") < 50)
           animator.SetFloat("distance", Vector3.Distance(transform.position, opponent.transform.position));
        
        //start taking off the hunger and thirst bar of dino
        animator.SetFloat("hunger", animator.GetFloat("hunger") - DinoBaseClass.decreaseHunger * Time.deltaTime);
        animator.SetFloat("thirst", animator.GetFloat("thirst") - DinoBaseClass.decreaseThirst * Time.deltaTime);
        animator.SetFloat("yAxis", animator.gameObject.transform.position.y);
        //displays raptys state of hunger atm
        //Debug.Log(animator.GetFloat("hunger"));

        //get hold of dino
        getDino();

        //take health from rapty if it has been attacked
        if (animator.GetBool("isAttacked") == true)
        {
            animator.SetFloat("health", animator.GetFloat("health") - DinoBaseClass.decreaseHealth * Time.deltaTime);
        }

        //if the rapty that has been selected is not the alpha male,
        //then return to alpha rapty and follow his actions
        if (alphaRapty == false)
        {
            if (Vector3.Distance(thisDino.transform.position, getAlpha().transform.position) >= 80)
            {
                ASagent.enabled = true;
                aStarVar.target = getAlpha();
            } else if(Vector3.Distance(thisDino.transform.position, getAlpha().transform.position) <= 30)
            {
                ASagent.enabled = false;
            }
        }
        
        //kill dino if he has his values to 0
        if (currentHunger <= 0 || currentThirst <= 0 || currentHealth <= 0)
        {
            dieDino();
            CancelInvoke();
        }

        //kill dino if he has his thirst has hit 0
        if (currentThirst <= 0)
        {
            dieDino();
        }

    }

    //move towards the target
    public void move(Vector3 directionVector)
    {
        directionVector *= 10 * Time.deltaTime;

        transform.Translate(directionVector, Space.World);
        transform.Rotate(transform.position + directionVector);
    }

}