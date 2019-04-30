using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyEating : StateMachineBehaviour {

    public bool foodGone = false;
    public bool firstrun = true;
    int listCount;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering Eating State");
        GameObject ankyFood = animator.GetComponent<FieldOfView>().visibleFoodSource[0].gameObject;
        animator.GetComponent<FieldOfView>().visibleFoodSource[0].GetComponent<Agent>().orientation = animator.GetComponent<FieldOfView>().visibleFoodSource[0].GetComponentInParent<Agent>().orientation;
        Seek ankySeek = animator.gameObject.GetComponent<Seek>();
        Face ankyFace = animator.gameObject.GetComponent<Face>();

        ankySeek.target = ankyFood;
        ankyFace.target = ankyFood;

        ankyFace.enabled = true;
        ankySeek.enabled = true;

        listCount = animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource.Count;
    }

    int count = 30;
    GameObject ankyFood;
    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (foodGone)
        {
            Debug.Log("It be true!!");

        }
        if (firstrun)
        {
            GameObject ankyFood = animator.GetComponent<FieldOfView>().visibleFoodSource[0].gameObject;
            firstrun = false;
        }
        if (count == 30)
        {
            Debug.Log("In Eating State");
            count = 0;
        }
        if (animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource.Count < listCount)
        {
            animator.gameObject.GetComponent<Seek>().target = null;
            animator.gameObject.GetComponent<Face>().target = null;
            animator.gameObject.GetComponent<Seek>().enabled = false;
            animator.gameObject.GetComponent<Face>().enabled = false;
            animator.SetBool("isGrazing", true);
        }
        //if (animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource[0].gameObject == null)
        //{
        //    animator.gameObject.GetComponent<Seek>().enabled = false;
        //    animator.gameObject.GetComponent<Face>().enabled = false;
        //    animator.SetBool("isGrazing", true);
        //}
        count++;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Eating State");
        animator.gameObject.GetComponent<Seek>().enabled = false;
        animator.gameObject.GetComponent<Face>().enabled = false;
        animator.SetBool("isEating", false);
    }

}
