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

        //passing in from the Idle state
        base.OnStateEnter(animator, stateInfo, layerIndex);

        animator.SetBool("deadOpponent", true);

        //When eating decrease speed to 0
        speed = 0.0f;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Increase hunger in raptor
        animator.SetFloat("hunger", animator.GetFloat("hunger") + hungerIncrease * Time.deltaTime);
        //if you have been eaten, die in this state
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //user message in console
        Debug.Log("Rapty is exiting the Eating State...");
    }
}