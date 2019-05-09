using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ankyFleeing : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Entered Fleeing State.");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float health = animator.gameObject.GetComponent<MyAnky>().health;
        float hunger = animator.gameObject.GetComponent<MyAnky>().hunger;
        float thirst = animator.gameObject.GetComponent<MyAnky>().thirst;

        if (health <= 0 || hunger <= 0 || thirst <= 0 || animator.gameObject.GetComponent<MyAnky>().timeToLive <= 0)    //
        {                                                                                                               //
                                                                                                                        // Enters Dead State if any of the arguments
            animator.SetBool("isDead", true);                                                                           // are activated, killing the Anky.
                                                                                                                        //
        }                                                                                                               //

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Left Fleeing State.");

        animator.SetBool("isFleeing", false);

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
