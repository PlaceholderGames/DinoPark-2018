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
        //enable the dead script
        deadD.enabled = true;
        animator.SetBool("deadDino", true);
        speed = 0;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    //no leaving the dead state
	}


}