using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyHunting : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Entered Hunting State.");

        animator.gameObject.GetComponent<Wander>().enabled = true;  // Enables the Wander script in the Rapty GameObject.

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float health = animator.gameObject.GetComponent<MyRapty>().health;
        float hunger = animator.gameObject.GetComponent<MyRapty>().hunger;
        float thirst = animator.gameObject.GetComponent<MyRapty>().thirst;

        if (health <= 0 || hunger <= 0 || thirst <= 0 || animator.gameObject.GetComponent<MyRapty>().timeToLive <= 0)   //
        {                                                                                                               //
                                                                                                                        // Enters Dead State if any of the arguments
            animator.SetBool("isDead", true);                                                                           // are activated, killing the Rapty.
                                                                                                                        //
        }                                                                                                               //

        if (health != animator.gameObject.GetComponent<MyRapty>().maxHealth)    //
        {                                                                       //
                                                                                // If the Rapty's health is less than its max health,
            animator.gameObject.GetComponent<MyRapty>().health++;               // then it starts regenerating health over time.
                                                                                //
        }                                                                       //

        if (thirst <= 50)                           //
        {                                           //
                                                    // If the Rapty's thirst value is less than the threshold,
            animator.SetBool("isDrinking", true);   // then it enters the Drinking State.
                                                    //
        }                                           //

        if (hunger <= 50)                           //
        {                                           //
                                                    // If the Rapty's hunger value is less than the threshold,
            animator.SetBool("isAttacking", true);  // then it enters the Attacking State.
                                                    //
        }                                           //

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Left Hunting State");

        animator.gameObject.GetComponent<Wander>().enabled = false; // Disables the Wander script in the Rapty GameObject.

        animator.SetBool("isHunting", false);

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
