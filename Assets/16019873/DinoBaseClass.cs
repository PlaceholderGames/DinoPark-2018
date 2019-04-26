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
    //speed properties regarding distance
    public float speed = 3.0f;
    public float rotationSpeed = 2.0f;
    public float accuracy = 5.0f;
    //water search variable
    public GameObject waterLocation;
    //variables for the dead state
    public bool deadOpponent = false;
    public bool deadDino = false;
    //hunger, thirst and health are represented as values
    //that increase or decrease over time, 
    //depending on what stage the dino is in and what was the last tracked activity
    public static int maxHunger = 100;
    public static int maxThirst = 100;
    public static int maxHealth = 100;
    //hunger and thirst will be decreased or increased, depending on the state by 3 values each time
    public const int decrease = 3;
    public const int increase = 3;
    //sea level that depends on the grid size 
    public const int seaLevel = 25;


    //reusing and overwritting this function every time the dino goes into the state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //Really bad to do this, should be loaded on awake
        fov = dino.GetComponent<FieldOfView>();
        pursue = dino.GetComponent<Pursue>();
        wander = dino.GetComponent<Wander>();
        face = dino.GetComponent<Face>();

        //get hold of the dino
        dino = animator.gameObject;
        //get hold of the other animal
        opponent = dino.GetComponent<RaptyAI>().getDino();
        

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

    }
}