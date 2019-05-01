using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : DinoBaseClass
{
    // Dead - If the animal is being eaten, reduce its 'health' until it is consumed
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //user message in console
        Debug.Log("Rapty is entering the Dead State...");
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //No leaving the dead state
        animator.SetBool("deadDino", false);

        //https://answers.unity.com/questions/802351/destroyobject-vs-destroy.html
        //also: https://learn.unity.com/tutorial/destroy-i?projectId=5c8920b4edbc2a113b6bc26a#5c8a6146edbc2a001f47d5c6 - to destroy within 3 sec
        DinoBaseClass.DestroyImmediate(dino);

    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
	    //do not exit if in dead state
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}