using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorWalk : StateMachineBehaviour {

    //to get the raptor
    GameObject Raptor;
    //to keep track of where the raptor is in the Dino Park
    GameObject[] dinoPark;
    //so i can keep track of where the raptors are and where they are going
    int dinoParkLocation;

    private void Awake()
    {
        dinoPark = GameObject.FindGameObjectsWithTag("dinoPark");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        Raptor = animator.gameObject;
        dinoParkLocation = 0;
	
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //checking if there is anywhere to go in the dinoPark
        //if not then return
        if (dinoPark.Length == 0) return;
        //this is calculating the distance between the raptor and the current location its heading to
        //if it has reached the dinoPark location then it will look for a new location
        if(Vector3.Distance(dinoPark[dinoParkLocation].transform.position, Raptor.transform.position) < 10.0f)
        {
            //adds the dinoPark location by 1
            dinoParkLocation++;
            //but if its greater than or equal to the length of the array then
            //it will set the dinoParkLocation back to zero
            if(dinoParkLocation >= dinoPark.Length)
            {
                dinoParkLocation = 0;
            }
        }

        //rotating towards target
        //calculate the direction that the Raptor is going towards
        var direction = dinoPark[dinoParkLocation].transform.position - Raptor.transform.position;
        //turns the Raptor towards the location
        Raptor.transform.rotation = Quaternion.Slerp(Raptor.transform.rotation, Quaternion.LookRotation(direction),
            1.0f * Time.deltaTime);
        //makes the raptor go fowards along the z axis
        Raptor.transform.Translate(0, 0, Time.deltaTime * 2.0f);
	
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
