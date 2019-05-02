using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyHerd : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Herding State");
        animator.gameObject.GetComponent<Seek>().target = animator.gameObject.GetComponent<MyAnky>().gameObject.GetComponent<MyAnky>().alpha;
        animator.gameObject.GetComponent<Seek>().enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.gameObject.transform.position, animator.gameObject.GetComponent<MyAnky>().alpha.transform.position) < 20)
        {
            animator.SetBool("isGrazing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Herding State");
        animator.SetBool("isHerding", false);
        animator.gameObject.GetComponent<Seek>().enabled = false;
    }

}
