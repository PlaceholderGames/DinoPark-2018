using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyGrazing : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Grazing State");
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        List<Transform> ankyFoodList = animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource;
        if (ankyFoodList.Count == 0)
        {
            animator.gameObject.GetComponent<Wander>().enabled = true;

            animator.gameObject.GetComponent<ASAgentInstance>().enabled = false;
            animator.gameObject.GetComponent<ASPathFollower>().enabled = false;
            animator.gameObject.GetComponent<AStarSearch>().enabled = false;
        }
        else
        {
            animator.gameObject.GetComponent<ASAgentInstance>().enabled = true;
            animator.gameObject.GetComponent<ASPathFollower>().enabled = true;
            animator.gameObject.GetComponent<AStarSearch>().target = animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource[0].gameObject;
            animator.gameObject.GetComponent<AStarSearch>().enabled = true;

            animator.gameObject.GetComponent<Wander>().enabled = false;


        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
