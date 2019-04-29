using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyGrazing : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Grazing State");
        animator.gameObject.GetComponent<Wander>().enabled = true;

        animator.gameObject.GetComponent<ASAgentInstance>().enabled = false;
        animator.gameObject.GetComponent<ASPathFollower>().enabled = false;
        animator.gameObject.GetComponent<AStarSearch>().enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        List<Transform> ankyFoodList = animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource;
        bool travelling = false;
        bool foodfound = false;
        bool foodReached = false;
        //checking for nearby food
        if (ankyFoodList.Count !=0)
        {
            animator.SetBool("isEating", true);
        }
        //will continue to wander until food is found if none are visible
        //if (ankyFoodList.Count == 0 && !foodfound )
        //{
        //    animator.gameObject.GetComponent<Wander>().enabled = true;

        //    animator.gameObject.GetComponent<ASAgentInstance>().enabled = false;
        //    animator.gameObject.GetComponent<ASPathFollower>().enabled = false;
        //    animator.gameObject.GetComponent<AStarSearch>().enabled = false;
        //    foodfound = true;
        //    Debug.Log("Trying to find food");
        //}
        //will travel to first food source in nearby area
        else if (travelling == false && foodfound && !foodReached)
        {
            animator.gameObject.GetComponent<ASPathFollower>().enabled = true;
            animator.gameObject.GetComponent<AStarSearch>().target = animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource[0].gameObject;
            animator.gameObject.GetComponent<ASAgentInstance>().enabled = true;            
            animator.gameObject.GetComponent<AStarSearch>().enabled = true;

            animator.gameObject.GetComponent<Wander>().enabled = false;
            travelling = true;
            Debug.Log("Trying to travel to food");
        }
        else if (travelling && foodfound && !foodReached)
        {
            //if (animator.gameObject.GetComponent<AStarSearch>().end == animator.gameObject.GetComponent<AStarSearch>().start)
            //{
            //    foodReached = true;
            //}
            //foodfound = false;
            Debug.Log("Ate and looking for new food");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
