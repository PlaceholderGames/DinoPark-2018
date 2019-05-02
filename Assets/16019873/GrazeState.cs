using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrazeState : DinoBaseClass {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.enabled = false;
	}

	 //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.SetFloat("distance", Vector3.Distance(dino.transform.position, pursue.target.transform.position));
        //animator.gameObject.transform.LookAt(pursue.target.transform.position);

        if (animator.GetFloat("thirst") < 20)
        {
            //ankyAI.move(ASfollower.getDirectionVector());
            AS.target = raptyAI.waterLocation;
            ASagent.enabled = true;
        } else
        {
            var lookPos = pursue.target.transform.position - animator.gameObject.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            animator.gameObject.transform.rotation = Quaternion.Slerp(animator.gameObject.transform.rotation, rotation, Time.deltaTime * 1.0f);
            animator.gameObject.transform.position = Vector3.MoveTowards(dino.transform.position, pursue.target.transform.position, 3.0f * Time.deltaTime);
        }

        
    }

	 //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.enabled = true;
    }
}
