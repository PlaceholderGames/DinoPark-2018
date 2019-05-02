using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyHunting : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Hunting State");
        animator.gameObject.GetComponent<Wander>().enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.GetComponent<MyRapty>().health <= 0 || animator.gameObject.GetComponent<MyRapty>().dead)
        {
            animator.SetBool("isDead", true);
        }
        bool foodFound = false;
        List<Transform> raptyFoodList = animator.gameObject.GetComponent<FieldOfView>().visibleAnkys;
        if (raptyFoodList.Count != 0)
        {
            foodFound = true;

        }
        if (foodFound)
        {
            animator.SetBool("isEating", true);
            animator.gameObject.GetComponent<MyRapty>().prevState = 4;
        }
        //water

        if (animator.gameObject.GetComponent<MyRapty>().thirst < 30)
        {
            animator.SetBool("isDrinking", true);
        }
        //herding checks
        if (animator.gameObject.GetComponent<MyRapty>().alpha == null && animator.gameObject.GetComponent<FieldOfView>().visibleRaptys.Count > 0)
        {
            animator.gameObject.GetComponent<MyRapty>().alpha = animator.gameObject.GetComponent<FieldOfView>().visibleRaptys[0].gameObject;
        }
        //checking if alpha anky has died
        if (animator.gameObject.GetComponent<MyRapty>().alpha.GetComponent<MyRapty>().dead)
        {
            if (animator.gameObject.GetComponent<FieldOfView>().visibleRaptys.Count > 1)
            {
                animator.gameObject.GetComponent<MyRapty>().alpha = animator.gameObject.GetComponent<FieldOfView>().visibleRaptys[1].gameObject;
            }
            else
            {
                animator.gameObject.GetComponent<MyRapty>().alpha = null;
            }
        }
        //moving towards alpha anky if it starts to get close to end of field of view
        if (Vector3.Distance(animator.gameObject.transform.position, animator.gameObject.GetComponent<MyRapty>().alpha.transform.position) > 30)
        {
            animator.SetBool("isHerding", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isHunting", false);
        animator.gameObject.GetComponent<Wander>().enabled = false;
    }

}
