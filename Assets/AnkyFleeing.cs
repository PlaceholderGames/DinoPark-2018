using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyFleeing : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Fleeing State");
        //setting the target of flee to be one of the raptys within the danger range
        animator.gameObject.GetComponent<Flee>().target = animator.GetComponent<FieldOfView>().visibleRaptys[animator.GetComponent<MyAnky>().raptyChasing].gameObject;
        //enabling flee state
        animator.gameObject.GetComponent<Flee>().enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float dist;
        dist = Vector3.Distance(animator.gameObject.transform.position, animator.gameObject.GetComponent<Flee>().target.gameObject.transform.position);
        if (dist > 60)
        {
            int dangerCount = 0;
            for (int i = 0; i < animator.gameObject.GetComponent<FieldOfView>().visibleRaptys.Count; i++)
            {
                dist = Vector3.Distance(animator.gameObject.transform.position, animator.gameObject.GetComponent<FieldOfView>().visibleRaptys[i].gameObject.transform.position);
                if (dist < 50)
                {
                    animator.gameObject.GetComponent<Flee>().target = animator.GetComponent<FieldOfView>().visibleRaptys[i].gameObject;
                    dangerCount++;
                }             
            }
            if (dangerCount == 0)
            {
                animator.gameObject.GetComponent<Flee>().enabled = false;
                animator.SetBool("isGrazing", true);

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Flee>().enabled = false;
        Debug.Log("Leaving Fleeing State");
        animator.SetBool("isFleeing", false);
    }

}
