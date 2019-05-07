using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyDrinking : StateMachineBehaviour
{

    public GameObject Detector;

    public bool isDrinking = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Entered Drinking State.");

        GameObject waterDetector;
        waterDetector = Instantiate(Detector, animator.gameObject.transform);

        waterDetector.transform.parent = null;
        waterDetector.transform.position = animator.gameObject.transform.position;

        animator.gameObject.GetComponent<Seek>().target = waterDetector;
        animator.gameObject.GetComponent<Seek>().enabled = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float health = animator.gameObject.GetComponent<MyRapty>().health;
        float hunger = animator.gameObject.GetComponent<MyRapty>().hunger;
        float thirst = animator.gameObject.GetComponent<MyRapty>().thirst;

        if (health <= 0 || hunger <= 0 || thirst <= 0 || animator.gameObject.GetComponent<MyRapty>().timeToLive <= 0)
        {

            animator.SetBool("isDead", true);

        }

        if (animator.gameObject.transform.position.y <= 37)
        {

            //Destroy(animator.gameObject.GetComponent<Seek>().target);

            //animator.gameObject.GetComponent<Seek>().enabled = false;
            //animator.gameObject.GetComponent<Seek>().target = null;

            isDrinking = true;

        }

        if (isDrinking)
        {

            animator.gameObject.GetComponent<MyRapty>().thirst += 0.5f;

            if (thirst >= animator.gameObject.GetComponent<MyRapty>().maxThirst)
            {

                animator.SetBool("isHunting", true);

            }

        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Left Drinking State");

        Destroy(animator.gameObject.GetComponent<Seek>().target);

        animator.gameObject.GetComponent<Seek>().enabled = false;
        animator.gameObject.GetComponent<Seek>().target = null;

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
