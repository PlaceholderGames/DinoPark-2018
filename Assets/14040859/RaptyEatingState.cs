using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyEatingState : StateMachineBehaviour {

    private float hunger;
    public float eatingRate = 10.0f;
    private BoxCollider col;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Rapty has entered Eating State");
        hunger = animator.GetFloat("hungerValue");
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (hunger >= 1)
        {
            hunger -= eatingRate;
        }
        animator.SetFloat("hungerValue", hunger);
        if (animator.GetFloat("hungerValue") <= 0)
        {
            animator.SetBool("isHunting", true);
        }

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Rapty has left Eating State");
        animator.SetBool("isEating", false);
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
