using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyEating : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Entered Eating State.");

        GameObject foodForRapty = animator.GetComponent<FieldOfView>().ankysSeen[0].gameObject; // Creates targets for the Rapty to follow to "attack" the Anky.

        Seek raptySearch = animator.gameObject.GetComponent<Seek>();    // Uses the Seek script to search for the Anky's to attack/ eat.

        raptySearch.target = foodForRapty;  // Sets the Anky's as targets for the Rapty.

        raptySearch.enabled = true; // Enabled the Seek script for use in the Rapty GameObject.

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float health = animator.gameObject.GetComponent<MyRapty>().health;
        float hunger = animator.gameObject.GetComponent<MyRapty>().hunger;
        float thirst = animator.gameObject.GetComponent<MyRapty>().thirst;

        if (health <= 0 || thirst <= 0 || hunger <= 0 || animator.gameObject.GetComponent<MyRapty>().timeToLive <= 0)   //
        {                                                                                                               //
                                                                                                                        // Enters Dead State if any of the arguments
            animator.SetBool("isDead", true);                                                                           // are activated, killing the Rapty.
                                                                                                                        //
        }                                                                                                               //

        if (hunger >= animator.gameObject.GetComponent<MyRapty>().maxHunger)    //
        {                                                                       //
                                                                                // If the Rapty's hunger value is the same or higher than its max hunger value,
            animator.SetBool("isAttacking", true);                              // then it enters the Attacking State.
                                                                                //
        }                                                                       //

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("left Eating State");

        animator.gameObject.GetComponent<Seek>().enabled = false;   // Disables the Seek script in the Rapty GameObject.
        animator.gameObject.GetComponent<Seek>().target = null;     // Sets the Seek script target to null.

        animator.SetBool("isEating", false);

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
