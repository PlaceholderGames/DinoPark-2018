using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//new attacking state
public class AttackState : DinoBaseClass
{
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //user message in console
        Debug.Log("Rapty is entering the Attacking State...");

        //reduce speed when in attack state
        speed = 1.0f;
        
        //keep attacking if you haven't exited this state
        base.OnStateEnter(animator, stateInfo, layerIndex);
        dino.GetComponent<RaptyAI>().startScratching();

    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //continue looking at the opponent
        dino.transform.LookAt(opponent.transform.position);

        //passing in from the Idle state
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //get hold of the other animal
        opponent = dino.GetComponent<RaptyAI>().getDino();
        opponent = dino.GetComponent<RaptyAI>().dieDino();

        animator.SetBool("deadOpponent", true);

    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //just before exiting, stop attacking
        dino.GetComponent<RaptyAI>().stopScratching();

        Debug.Log("Rapty is exiting the Attacking State...");
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