using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyAlerted : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Alerted State");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<MyAnky>().dangerCount = animator.gameObject.GetComponent<FieldOfView>().visibleRaptys.Count;
        float dist;
        bool run = false;
        for (int i=0;i<animator.gameObject.GetComponent<MyAnky>().dangerCount;i++)
        {
            dist = Vector3.Distance(animator.gameObject.transform.position, animator.gameObject.GetComponent<FieldOfView>().visibleRaptys[i].gameObject.transform.position);
            if (dist < 50)
            {
                run = true;
            }
        }
        if (run)
        {
            animator.SetBool("isFleeing", true);
        }
        else if (!run)
        {
            if (animator.gameObject.GetComponent<MyAnky>().prevState == 4)
            {
                animator.SetBool("isGrazing", true);
            }
        }
        else if (!run)
        {
            if (animator.gameObject.GetComponent<MyAnky>().prevState == 1)
            {
                animator.SetBool("isEating", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Alerted State");
        animator.SetBool("isAlerted", false);
    }
}
