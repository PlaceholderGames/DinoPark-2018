using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyEating : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering Eating State");
        GameObject raptyFood = animator.GetComponent<FieldOfView>().visibleAnkys[0].gameObject;
        //animator.GetComponent<FieldOfView>().visibleFoodSource[0].GetComponent<Agent>().orientation = animator.GetComponent<FieldOfView>().visibleFoodSource[0].GetComponentInParent<Agent>().orientation;
        Pursue raptySeek = animator.gameObject.GetComponent<Pursue>();
        Face raptyFace = animator.gameObject.GetComponent<Face>();

        raptySeek.target = raptyFood;
        raptyFace.target = raptyFood;

        raptyFace.enabled = true;
        raptySeek.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.gameObject.GetComponent<Pursue>().enabled = true;
        //checking if dead
        if (animator.gameObject.GetComponent<MyRapty>().health <= 0 || animator.gameObject.GetComponent<MyRapty>().dead)
        {
            animator.SetBool("isDead", true);
        }
        //check if rapty has hit an anky
        if (animator.gameObject.GetComponent<MyRapty>().recover)
        {

        }
        //check if rapty ate the food
        if (animator.gameObject.GetComponent<MyRapty>().foodGone)
        {

            animator.gameObject.GetComponent<Pursue>().enabled = false;
            animator.gameObject.GetComponent<Pursue>().target = null;
            animator.SetBool("isHunting", true);
            //Destroy(animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource[0].gameObject);
            animator.gameObject.GetComponent<MyRapty>().foodGone = false; //reseting bool
            animator.gameObject.GetComponent<MyRapty>().prevState = 1;
        }
        //check if the another rapty ate food
        if (animator.gameObject.GetComponent<Pursue>().target == null)
        {
            animator.gameObject.GetComponent<Pursue>().enabled = false;
            animator.SetBool("isHunting", true);
            animator.gameObject.GetComponent<MyRapty>().prevState = 1;
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Eating State");
        animator.gameObject.GetComponent<Pursue>().enabled = false;
        animator.gameObject.GetComponent<Face>().enabled = false;
        animator.SetBool("isEating", false);
    }

}
