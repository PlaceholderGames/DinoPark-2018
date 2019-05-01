using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyGrazing : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Grazing State");
        animator.gameObject.GetComponent<Wander>().enabled = true;
    }

    int count = 30;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (count == 30)
        {
            Debug.Log("In Grazing State");
            count = 0;
        }
        if (animator.gameObject.GetComponent<FieldOfView>().visibleRaptys.Count != animator.gameObject.GetComponent<MyAnky>().dangerCount)
        {
            animator.SetBool("isAlerted", true);
            animator.gameObject.GetComponent<MyAnky>().prevState = 4;
        }
        count++;
        bool foodFound = false;
        if (animator.gameObject.GetComponent<MyAnky>().health <= 0)
        {
            animator.SetBool("isDead", true);
        }
        List<Transform> ankyFoodList = animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource;
        if (ankyFoodList.Count != 0)
        {
            foodFound = true;
            
        }
        if (foodFound)
        {
            animator.SetBool("isEating", true);
            animator.gameObject.GetComponent<MyAnky>().prevState = 4;
        }
               
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Grazing State");
        animator.gameObject.GetComponent<Wander>().enabled = false;
        animator.SetBool("isGrazing", false);
    }
}
