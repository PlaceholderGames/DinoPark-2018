using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyIdle : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Idle State");
        Wander anky = animator.gameObject.GetComponent<Wander>();
        anky.enabled = true;

    }
    int count = 30;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (count == 30)
        {
            Debug.Log("In Idle State");
            count = 0;
        }
        float health = animator.gameObject.GetComponent<MyAnky>().health;
        float maxHealth = animator.gameObject.GetComponent<MyAnky>().maxHealth;
        if (maxHealth>health)
        {
            animator.SetBool("isGrazing", true);
            
        }
        count++;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Idle State");
        //Wander anky = animator.gameObject.GetComponent<Wander>();
        //anky.enabled = false;
        animator.SetBool("isIdle", false);
    }
}
