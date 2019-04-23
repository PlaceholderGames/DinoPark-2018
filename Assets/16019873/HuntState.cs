using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : DinoBaseClass
{

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //user message in console
        Debug.Log("Rapty is entering the Hunting State...");

        //passing in from the Idle state
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //when entering this state,
        //the walking speed is increasing for rapty
        speed++;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //rotate towards the detected target
        //the hunting code is here (moving code)
        var direction = opponent.transform.position - dino.transform.position;
        dino.transform.rotation = Quaternion.Slerp(dino.transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        dino.transform.Translate(0, 0, Time.deltaTime * speed);
	}


	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //user message in console
        Debug.Log("Rapty is exiting the Hunting State...");
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
