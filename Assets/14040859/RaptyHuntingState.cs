using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyHuntingState : StateMachineBehaviour {


    Transform raptyLocation;
    public float thirst;
    private float thirstIncreaseTick = 10;
    private float thirstTick = 0;
    public float hunger = 0;
    public float raptyX;
    public float raptyY;
    public float raptyZ;
   

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Rapty has entered Hunting State");
        animator.SetBool("isHunting", true);
        hunger = animator.GetFloat("Hunger");
        thirst = animator.GetFloat("Thirst");

       // raptyLocation = GetComponent<Transform>();
        animator.SetFloat("Rapty X", raptyLocation.transform.position.x);
        animator.SetFloat("Rapty Y", raptyLocation.transform.position.y);
        animator.SetFloat("Rapty Z", raptyLocation.transform.position.z);

    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

     //   animator.SetFloat("Rapty X", raptyLocation.transform.position.x);
      //  animator.SetFloat("Rapty Y", raptyLocation.transform.position.y);
      //  animator.SetFloat("Rapty Z", raptyLocation.transform.position.z);

        thirst += 0.5f;
        hunger += 10.1f; 
        animator.SetFloat("thirstValue", thirst);
        animator.SetFloat("hungerValue", hunger);
        if (thirst > 50000) { 
            animator.SetBool("isDrinking", true);
        }

    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Rapty has left Hunting State");
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
