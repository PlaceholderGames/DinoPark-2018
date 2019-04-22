using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//new base class for the dinos (that will contain information regarding distances from other surroundings)
public class DinoBaseClass : StateMachineBehaviour 
{
    public GameObject dino;
    public GameObject opponent;
    //speed properties regarding distance
    public float speed = 3.0f;
    public float rotationSpeed = 2.0f;
    public float accuracy = 5.0f;
    //hunger, thirst and health are represented as values
    //that increase or decrease over time, 
    //depending on what stage the dino is in and what was the last tracked activity
    public int hunger = 100;
    public int thirst = 100;
    public int health = 100;


    //reusing and overwritting this function every time the dino goes into the state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //get hold of the dino
        dino = animator.gameObject;
        //get hold of the other animal
        opponent = dino.GetComponent<RaptyAI>().getDino();
    }

}
