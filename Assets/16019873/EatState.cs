using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : DinoBaseClass
{

    private int hungerIncrease = 8;

    // Eating - requires a box collision with a dead dino
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //user message in console
        Debug.Log("Rapty is entering the Eating State...");

        //When eating decrease speed to 0
        speed = 0.0f;

        //passing in from the Idle state
        base.OnStateEnter(animator, stateInfo, layerIndex);

    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Increase hunger in raptor
        animator.SetFloat("hunger", animator.GetFloat("hunger") + hungerIncrease * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //user message in console
        Debug.Log("Rapty is exiting the Eating State...");
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
