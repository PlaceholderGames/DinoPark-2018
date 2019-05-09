using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyDrinking : StateMachineBehaviour
{

    public GameObject Detector;

    public bool isDrinking = false; // Used for whether or not the Rapty is drinking.

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Entered Drinking State.");

        GameObject waterDetector;   // Used as a target to "guide" the Rapty towards a water source, ie. anywhere lower than the y position of 37.
        waterDetector = Instantiate(Detector, animator.gameObject.transform);

        waterDetector.transform.parent = null;                                      // Allowing the waterDetector to be free from its parent Object
        waterDetector.transform.position = animator.gameObject.transform.position;  // so that it can be moved without any hindrance.

        animator.gameObject.GetComponent<Seek>().target = waterDetector;    // Implements the Seek script for the waterDetector, allowing the Rapty to follow it.
        animator.gameObject.GetComponent<Seek>().enabled = true;            // Enables the Seek script for use.

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

        if (animator.gameObject.transform.position.y <= 37) //
        {                                                   //
                                                            // If the Rapty's y position is at this coordinate or lower,
            isDrinking = true;                              // then it starts drinking.
                                                            //
        }                                                   //

        if (isDrinking)
        {

            animator.gameObject.GetComponent<MyRapty>().thirst += 0.5f; // Increases the Rapty's thirst after drinking.

            if (thirst >= animator.gameObject.GetComponent<MyRapty>().maxThirst)    //
            {                                                                       //
                                                                                    // Once the Rapty's thirst has reached and/ or exceeded the maximum thirst,
                animator.SetBool("isHunting", true);                                // it goes back into the Hunting State.
                                                                                    //
            }                                                                       //

        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Left Drinking State");

        Destroy(animator.gameObject.GetComponent<Seek>().target);   // Destroys the waterDetector GameObject.

        animator.gameObject.GetComponent<Seek>().enabled = false;   // Disables the Seek script for use with the waterDetector or the Rapty GameObject.
        animator.gameObject.GetComponent<Seek>().target = null;     // Causes the waterDetector to stop being a target for the Seek script/ Rapty.

        animator.SetBool("isDrinking", false);

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
