﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyDrinkingState : StateMachineBehaviour {

    private float drinkingRate = 8;
    public float thirst;
    private Wander walkingScript;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Raptor has entered drinking state.");
        animator.SetBool("isDrinking", true);
        thirst = animator.GetFloat("thirstValue");
        walkingScript.enabled = false;

        //Agent = g

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        thirst = thirst - drinkingRate;
        animator.SetFloat("thirstValue", thirst);

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Rapty has left drinking State");
        walkingScript.enabled = true;

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