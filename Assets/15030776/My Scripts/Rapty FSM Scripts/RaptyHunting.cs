﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyHunting : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Entered Hunting State.");

        animator.gameObject.GetComponent<Wander>().enabled = true;

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

        if (health != animator.gameObject.GetComponent<MyRapty>().maxHealth)
        {

            animator.gameObject.GetComponent<MyRapty>().health++;

        }

        if (thirst <= 50)
        {

            animator.SetBool("isDrinking", true);

        }

        if (hunger <= 50)
        {

            animator.SetBool("isAttacking", true);

        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("Left Hunting State");

        animator.gameObject.GetComponent<Wander>().enabled = false;

        animator.SetBool("isHunting", false);

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