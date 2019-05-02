using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyDrinking : StateMachineBehaviour {

    public GameObject finder;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Drinking State");
        GameObject watFind;
        watFind = Instantiate(finder, animator.gameObject.transform);
        animator.gameObject.GetComponent<Seek>().target = watFind;
        animator.gameObject.GetComponent<Seek>().enabled = true;
    }

    public bool drinking = false;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //checking if anky has died
        if (animator.gameObject.GetComponent<MyAnky>().health <= 0)
        {
            animator.SetBool("isDead", true);
        }
        //checking if anky is at water
        if (animator.gameObject.transform.position.y < 34)
        {
            animator.gameObject.GetComponent<Seek>().enabled = false;
            animator.gameObject.GetComponent<Seek>().target = null;
            drinking = true;
        }
        if (drinking)
        {
            animator.gameObject.GetComponent<MyAnky>().thirst += 0.01f;
        }
        if (animator.gameObject.GetComponent<MyAnky>().thirst >= 45.0f)
        {
            animator.SetBool("isGrazing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Drinking State");
        //reseting seek states in case anky dies
        animator.gameObject.GetComponent<Seek>().enabled = false;
        animator.gameObject.GetComponent<Seek>().target = null;
        animator.SetBool("isDrinking", false);
    }

}
