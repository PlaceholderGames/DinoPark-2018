using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyRecovering : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Recovery State");
    }

    float rec = 0.0f;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rec = rec + 0.01f;
        if (rec == 5.0f)
        {
            animator.SetBool("isEating", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Recovery State");
        animator.SetBool("isEating", false);
    }

}
