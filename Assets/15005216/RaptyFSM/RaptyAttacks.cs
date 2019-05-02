using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyAttacks : StateMachineBehaviour {


    Collider[] Targets;
    float AttackTime = 0; 
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        for (int i = 0; i < animator.GetComponent<RaptyFieldOfView1>().coll().Length; i++)
        {
            Targets = animator.GetComponent<RaptyFieldOfView1>().coll();
        }
        AttackTime += Time.deltaTime;
        if (AttackTime > animator.GetComponent<RaptyStats>().GetAttackSpeed())
        {
            Debug.Log("Rapty Swipe");
            for (int i = 0; i < animator.GetComponent<RaptyFieldOfView1>().coll().Length; i++)
            {
                if (Targets[i].gameObject.name == "Anky")
                {
                    Targets[i].GetComponent<AnkyStats>().SetHealth(-20);
                    Debug.Log("Anky Health " + Targets[i].GetComponent<AnkyStats>().GetHealth());

                    if (Targets[i].GetComponent<AnkyStats>().GetHealth() < 140)
                    {
                        Targets[i].GetComponent<MyAnky>().BehaviourSwitch("isFleeing");
                        animator.GetComponent<MyRapty>().BehaviourSwitch("isHunting");
                    }
                    if (Targets[i].GetComponent<AnkyStats>().GetHealth() <= 100)
                    {
                        Targets[i].GetComponent<MyAnky>().BehaviourSwitch("isDead");
                        animator.GetComponent<MyRapty>().BehaviourSwitch("isHunting");
                        animator.GetComponent<RaptyStats>().SetHealth(20);
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
