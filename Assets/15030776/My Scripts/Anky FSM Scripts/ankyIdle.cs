using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ankyIdle : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Entered Idle State");

        Wander anky = animator.gameObject.GetComponent<Wander>();   // Used to implement the Wander script onto the Anky GameObject.

        anky.enabled = true;    // Enables the Wander script on the Anky GameObject.

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        float hunger = animator.gameObject.GetComponent<MyAnky>().hunger;
        float thirst = animator.gameObject.GetComponent<MyAnky>().thirst;

        if (hunger <= 90 || thirst <= 90)           //
        {                                           //
                                                    // Once one of the thresholds has been met,
            animator.SetBool("isGrazing", true);    // the Anky will enter the Grazing State.
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
