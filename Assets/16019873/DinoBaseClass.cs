using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//new base class for the dinos (that will contain information regarding distances from other surroundings)
public class DinoBaseClass : StateMachineBehaviour
{
    //creating a variable to be able to access the conditions set in the Unity Animator Controller
    Animator animator;
    public GameObject dino;
    public GameObject opponent;
    public FieldOfView fov;
    public Wander wander;
    public Pursue pursue;
    public Face face;
    public AStarSearch AS;
    public ASPathFollower ASfollower;
    public ASAgentInstance ASagent;
    public Flee flee;
    public RaptyAI raptyAI;
    //speed properties regarding distance
    public float speed = 3.0f;
    public float rotationSpeed = 2.0f;
    public float accuracy = 5.0f;
    //water search variable
    public GameObject waterLocation;
    //variables for the dead state
    public bool deadOpponent = false;
    //hunger, thirst and health are represented as values
    //that increase or decrease over time, 
    //depending on what stage the dino is in and what was the last tracked activity
    public static int maxHunger = 100;
    public static int maxThirst = 100;
    public static int maxHealth = 100;
    //hunger and thirst will be decreased or increased, depending on the state by 3 values each time
    public const int decreaseHunger = 3;
    public const int decreaseThirst = 1;
    public const int decreaseHealth = 5;
    public const int increaseHealth = 5;

    //reusing and overwritting this function every time the dino goes into the state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dino = animator.gameObject;
        //Really bad to do this, should be loaded on awake
        wander = animator.gameObject.GetComponent<Wander>();
        pursue = animator.gameObject.GetComponent<Pursue>();
        face = animator.gameObject.GetComponent<Face>();
        fov = animator.gameObject.GetComponent<FieldOfView>();
        AS = animator.gameObject.GetComponent<AStarSearch>();
        ASfollower = animator.gameObject.GetComponent<ASPathFollower>();
        ASagent = animator.gameObject.GetComponent<ASAgentInstance>();
        flee = animator.gameObject.GetComponent<Flee>();
        raptyAI = animator.gameObject.GetComponent<RaptyAI>();

        
        //get hold of the dino
        //dino = dino.GetComponent<RaptyAI>().getDino();

        //get hold of the other animal
        //opponent = opponent.GetComponent<RaptyAI>().getDino();

        //set the nobody around bool to be always anky
        //opponent = GameObject.FindGameObjectsWithTag("Anky");

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

    }
}