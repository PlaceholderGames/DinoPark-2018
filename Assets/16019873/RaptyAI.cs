using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyAI : MonoBehaviour
{
    Animator animator;
    //linked to the dino
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
    public int maxHunger = 100;
    public int maxThirst = 100;
    public int maxHealth = 100;
    //hunger and thirst will be decreased or increased, depending on the state by 3 values each time
    public int decreaseHunger = 3;
    public int increaseHunger = 3;
    //hunger and thirst time to be started as soon as the game starts
    private float startTimerHung;
    private float startTimerThir;
    //sea level that depends on the grid size 
    public int seaLevel = 25;


    //work out the distance between one dino to another
    public GameObject getDino()
    {
        return otherDino;
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

        //starts decreasing the hunger time
        startTimerHung = Time.time;
        //starts decreasing the hunger time
        startTimerThir = Time.time;

        //???
        //aStar = GetComponent<AStarSearch>();
        //follower = GetComponent<ASPathFollower>();
    }

    //update is called once per frame
    private void Update()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, otherDino.transform.position));
        float h = Time.time - startTimerHung;
        float t = Time.time - startTimerHung;

        if (startTimerHung < h)
        {
            maxHunger -= decreaseHunger;
        }

        //pushing rapty back
        if (startTimerThir < t)
        {
            getDino();

            if (transform.position.z <= seaLevel)
            {

            }
        }

    }

}