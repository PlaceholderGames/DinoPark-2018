﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyIdle : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Idle State");
        Wander rapty = animator.gameObject.GetComponent<Wander>();
        rapty.enabled = true;

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
        float health = animator.gameObject.GetComponent<MyRapty>().health;
        float maxHealth = animator.gameObject.GetComponent<MyRapty>().maxHealth;
        if (maxHealth > health)
        {
            animator.SetBool("isHunting", true);
            animator.gameObject.GetComponent<MyRapty>().prevState = 0;
        }
        count++;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Idle State");
        animator.SetBool("isIdle", false);
    }
}