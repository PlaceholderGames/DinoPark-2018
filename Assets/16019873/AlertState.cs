using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : DinoBaseClass
{
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Rapty is entering the Alert State...");
        animator.SetBool("isAttacked", true);

        //play animation with exclamation mark
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if rapty is alert then it means that some other animal is near by
        //and he can go into fleeing if his health is under the minimum
        //but to try to be not noticed by anyone, the dino will not move for a few seconds
        if (animator.GetBool("nobodyAround") == false)
        {
            //When someone is around
            //stop moving for few secs (decrease speed to 0)
            speed = 0.0f;
            //WaitForSeconds(4);
        }
        //reset speed
        speed = 7.0f;
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Rapty is exiting the Alert State...");
        animator.SetBool("isAttacked", false);
        
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
