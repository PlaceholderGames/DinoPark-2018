using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : DinoBaseClass
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Debug.Log("Rapty is entering the Hunting State..."); //user message in console
        animator.SetBool("isAttacking", true);
        pursue.enabled = true;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (opponent != null)
        {
            animator.SetFloat("distance", Vector3.Distance(animator.gameObject.transform.position, opponent.transform.position));
            //animator.gameObject.transform.LookAt(opponent.transform.position);
        }

    }


	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        pursue.enabled = false;
        animator.SetBool("isAttacking", false);
        //user message in console
        Debug.Log("Rapty is exiting the Hunting State...");
    }

}
