using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyAI : MonoBehaviour
{
    Animator animator;
    //linked to the dino
    public GameObject thisDino;
    public GameObject otherDino;
    public GameObject claws;
    public GameObject Cube3;   //rapty's body
    //there is one Alpha male raptor that will control the rest when in the hunting stage
    //the rest will follow his commands
    public ASPathFollower follower;
    //A star path finding
    public AStarSearch aStar;
    //alpha male variables
    public bool alphaRapty = false;
    public bool returnToAlphaRapty = false;
    //hunger, thirst and health are represented as values
    //that increase or decrease over time, 
    //depending on what stage the dino is in and what was the last tracked activity
    public static int maxHunger = 100;
    public static int maxThirst = 100;
    public static int maxHealth = 100;
    //current hunger/thirst values
    public float currentHunger = 100;
    public float currentThirst = 100;
    public float currentHealth = 100;
    //hunger and thirst will be decreased or increased, depending on the state by 3 values each time
    public int decrease = 3;
    public int increase = 3;
    //sea level that depends on the grid size 
    public int seaLevel = 25;


    //work out the distance between one dino to another
    public GameObject getDino()
    {
        return otherDino;
    }

    //work out the distance between one dino to another
    public GameObject dieDino()
    {
        //https://answers.unity.com/questions/802351/destroyobject-vs-destroy.html
        DestroyImmediate(thisDino);
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
        //setting it to get data from the FSM created in Unity Animator
        animator = GetComponent<Animator>();
        alphaRapty = animator.gameObject;
        //make other raptys follow the alpha dino
        aStar = GetComponent<AStarSearch>();
        follower = GetComponent<ASPathFollower>();

        //at the start of the game, dino got full health/hunger/thirst bar
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        
    }

    //update is called once per frame
    private void Update()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, otherDino.transform.position));

        //start taking off the hunger and thirst bar of dino
        currentThirst -= decrease * Time.deltaTime;

        animator.SetFloat("hunger", animator.GetFloat("hunger") - decrease * Time.deltaTime);
        Debug.Log(animator.GetFloat("hunger"));

        //displays raptys state of hunger atm
        

        //get hold of dino
        getDino();
        //and push him back to not drown
        if (transform.position.z <= seaLevel)
        {
            getDino();
            animator.SetFloat("thirst", Vector3.Distance(transform.position, thisDino.transform.position));
        }
        
        //kill dino if he has his values to 0
        if (currentHunger <= 0 || currentThirst <= 0 || currentHealth <= 0)
        {
            dieDino();
            CancelInvoke();
        }

    }

}