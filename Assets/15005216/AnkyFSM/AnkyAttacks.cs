using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyAttacks : StateMachineBehaviour {


    Collider[] Targets;
    float AttackTime = 0;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < animator.GetComponent<AnkyFieldOfView>().coll().Length; i++)
        {
            Targets = animator.GetComponent<AnkyFieldOfView>().coll();
        }
        AttackTime += Time.deltaTime;
        if (AttackTime > animator.GetComponent<AnkyStats>().GetAttackSpeed())
        {
            for (int i = 0; i < animator.GetComponent<AnkyFieldOfView>().coll().Length; i++)
            {
                if (Targets[i].gameObject.name == "Rapty")
                {
                    if (Targets[i].GetComponent<MyRapty>().CurrentBehaviour() == "isAttacking") //Only attacks in self-defense
                    {
                        Debug.Log("Anky Swipe");
                        Targets[i].GetComponent<RaptyStats>().SetHealth(-20);
                        Debug.Log("Rapty Health " + Targets[i].GetComponent<RaptyStats>().GetHealth());

                        if (Targets[i].GetComponent<RaptyStats>().GetHealth() < 0)
                        {
                            Targets[i].GetComponent<MyRapty>().BehaviourSwitch("isDead");
                            Targets[i].GetComponent<Face>().enabled = false;
                            animator.GetComponent<MyAnky>().BehaviourSwitch("isAlert");

                        }
                        if (Targets[i].GetComponent<RaptyStats>().GetHealth() < 40)
                        {
                            Targets[i].GetComponent<MyRapty>().BehaviourSwitch("isFleeing");
                            animator.GetComponent<MyAnky>().BehaviourSwitch("isAlert");
                        }
                    }
                    else
                    {
                        animator.GetComponent<MyAnky>().BehaviourSwitch("isAlert");
                    }
                }
            }
            AttackTime = 0;
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
