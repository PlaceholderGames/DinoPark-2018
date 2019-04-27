using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkState : DinoBaseClass
{
    private int thirstIncrease = 5;

    // Drinking - requires y value to be below 32 (?)
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //user message in console
        Debug.Log("Rapty is entering the Drinking State...");

        //When drinking decrease speed to 0
        speed = 0.0f;

        //passing in from the Idle state
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Increase thirst in raptor
        animator.SetFloat("thirst", animator.GetFloat("thirst") + thirstIncrease * Time.deltaTime);
        //use A * path finding to locate the closest water source
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //user message in console
        Debug.Log("Rapty is exiting the Drinking State...");
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
