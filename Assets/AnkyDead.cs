using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyDead : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Dead State Entered");
        animator.gameObject.GetComponent<MyAnky>().dead = true;
    }

    public float decompose = 0.0f;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (decompose == 1.0f)
        {
            animator.gameObject.GetComponent<MyAnky>().decompose = true;
            animator.SetBool("isDone", true);
        }
        decompose = decompose + 0.01f;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDead", false);
    }

}
