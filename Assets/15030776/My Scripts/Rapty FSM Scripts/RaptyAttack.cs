using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyAttack : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Entered Attacking State.");

        animator.gameObject.GetComponent<Wander>().enabled = true;  // Enables the Wander script within the Rapty GameObject.

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        List<Transform> targetsForRaptyList = animator.gameObject.GetComponent<FieldOfView>().ankysSeen;    // Uses the FieldOfView script to see the amount Anky's within the Rapty's sight.

        float health = animator.gameObject.GetComponent<MyRapty>().health;
        float hunger = animator.gameObject.GetComponent<MyRapty>().hunger;
        float thirst = animator.gameObject.GetComponent<MyRapty>().thirst;

        if (health <= 0 || hunger <= 0 || thirst <= 0 || animator.gameObject.GetComponent<MyRapty>().timeToLive <= 0)   //
        {                                                                                                               //
                                                                                                                        // Enters Dead State if any of the arguments
            animator.SetBool("isDead", true);                                                                           // are activated, killing the Rapty.
                                                                                                                        //
        }                                                                                                               //

        if (hunger >= animator.gameObject.GetComponent<MyRapty>().maxHunger)    //
        {                                                                       //
                                                                                // If the Rapty's hunger value is the same or higher than its maximum hunger value,
            animator.SetBool("isHunting", true);                                // then the Rapty returns the Hunting State.
                                                                                //
        }                                                                       //

        if (targetsForRaptyList.Count != 0)                                     //
        {                                                                       //
                                                                                // If the list of targets for the Rapty is more than 0,
            animator.SetBool("isEating", true);                                 // then it enters the Eating State.
                                                                                //
        }                                                                       //

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Left Attacking State.");

        animator.gameObject.GetComponent<Wander>().enabled = false; // Disables the Wander script within the Rapty GameObject.

        animator.SetBool("isAttacking", false);

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
