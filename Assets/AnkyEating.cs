using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyEating : StateMachineBehaviour {

    public bool foodGone = false;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering Eating State");
        GameObject ankyFood = animator.GetComponent<FieldOfView>().visibleFoodSource[0].gameObject;

        animator.gameObject.GetComponent<Seek>().target = ankyFood;
        animator.gameObject.GetComponent<Face>().target = ankyFood;

        animator.gameObject.GetComponent<Seek>().enabled = true;
        animator.gameObject.GetComponent<Face>().enabled = true;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (foodGone)
        {
            animator.gameObject.GetComponent<Seek>().enabled = false;
            animator.gameObject.GetComponent<Face>().enabled = false;
            animator.SetBool("isGrazing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Eating State");
        animator.gameObject.GetComponent<Seek>().enabled = false;
        animator.gameObject.GetComponent<Face>().enabled = false;
        animator.SetBool("isEating", false);
    }

}
