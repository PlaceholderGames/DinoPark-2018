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
        //Face ankyFace = animator.gameObject.GetComponent<Face>();

        ankySeek.target = ankyFood;
        //ankyFace.target = ankyFood;

        //ankyFace.enabled = true;
        ankySeek.enabled = true;
        //was going to be used so if another anky ate the food source being travelled to and more were available then the anky would switch to another food source
        listCount = animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource.Count;
    }

    int count = 30;
    GameObject ankyFood;
    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.GetComponent<MyAnky>().health <= 0)
        {
            animator.SetBool("isDead", true);
        }
        //checking if more raptys have entered field of view
        if (animator.gameObject.GetComponent<FieldOfView>().visibleRaptys.Count != animator.gameObject.GetComponent<MyAnky>().dangerCount)
        {
            animator.SetBool("isAlerted", true);
            animator.gameObject.GetComponent<MyAnky>().prevState = 4;
        }
        //checking distance between anky and rapty when there is only one
        if (animator.gameObject.GetComponent<MyAnky>().dangerCount == 1)
        {
            float dist;
            dist = Vector3.Distance(animator.gameObject.transform.position, animator.gameObject.GetComponent<FieldOfView>().visibleRaptys[0].gameObject.transform.position);
            if (dist < 50)
            {
                animator.SetBool("isFleeing", true);
                animator.gameObject.GetComponent<MyAnky>().prevState = 4;
            }
        }
        //checks distance between all raptys
        //else if used to avoid for loop being used unessecarily
        else if (animator.gameObject.GetComponent<MyAnky>().dangerCount >= 1)
        {
            for (int i = 0; i < animator.gameObject.GetComponent<MyAnky>().dangerCount; i++)
            {
                float dist;
                dist = Vector3.Distance(animator.gameObject.transform.position, animator.gameObject.GetComponent<FieldOfView>().visibleRaptys[i].gameObject.transform.position);
                if (dist < 50)
                {
                    animator.SetBool("isFleeing", true);
                    animator.gameObject.GetComponent<MyAnky>().raptyChasing = i;
                    animator.gameObject.GetComponent<MyAnky>().prevState = 4;
                }
            }
        }
        //check if anky ate the food
        if (animator.gameObject.GetComponent<MyAnky>().foodGone)
        {
            
            animator.gameObject.GetComponent<Seek>().enabled = false;
            animator.gameObject.GetComponent<Seek>().target = null;
            animator.SetBool("isGrazing", true);
            Destroy(animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource[0].gameObject);
            animator.gameObject.GetComponent<MyAnky>().foodGone = false; //reseting bool
            animator.gameObject.GetComponent<MyAnky>().prevState = 1;
        }
        //check if the another anky ate food
        if(animator.gameObject.GetComponent<Seek>().target == null)
        {
            animator.gameObject.GetComponent<Seek>().enabled = false;
            animator.SetBool("isGrazing", true);
            animator.gameObject.GetComponent<MyAnky>().prevState = 1;
        }

        if (count == 30)
        {
            Debug.Log("In Eating State");
            count = 0;
        }
        
        count++;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Eating State");
        animator.gameObject.GetComponent<Seek>().enabled = false;
        //animator.gameObject.GetComponent<Face>().enabled = false;
        animator.SetBool("isEating", false);
    }

}
