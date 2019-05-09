using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyIdle : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Entered Idle State.");

        Wander rapty = animator.gameObject.GetComponent<Wander>();  // Used to implement the Wander script onto the Rapty GameObject.

        rapty.enabled = true;   // Enables the Wander script on the Rapty GameObject.

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float hunger = animator.gameObject.GetComponent<MyRapty>().hunger;
        float thirst = animator.gameObject.GetComponent<MyRapty>().thirst;

        if (hunger <= 75 || thirst <= 75)           //
        {                                           //
                                                    // Once one of the thresholds has been met,
            animator.SetBool("isHunting", true);    // the Rapty will enter the Hunting State.
                                                    //
        }                                           //

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Left Idle State.");

        animator.SetBool("isIdle", false);

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
