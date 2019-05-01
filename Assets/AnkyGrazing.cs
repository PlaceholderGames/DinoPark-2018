using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyGrazing : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Grazing State");
        animator.gameObject.GetComponent<Wander>().enabled = true;
    }

    int count = 30;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (count == 30)
        {
            Debug.Log("In Grazing State");
            count = 0;
        }
        if (animator.gameObject.GetComponent<FieldOfView>().visibleRaptys.Count != animator.gameObject.GetComponent<MyAnky>().dangerCount)
        {
            animator.SetBool("isAlerted", true);
            animator.gameObject.GetComponent<MyAnky>().prevState = 4;
        }
        count++;
        bool foodFound = false;
        if (animator.gameObject.GetComponent<MyAnky>().health <= 0)
        {
            animator.SetBool("isDead", true);
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
        //food
        List<Transform> ankyFoodList = animator.gameObject.GetComponent<FieldOfView>().visibleFoodSource;
        if (ankyFoodList.Count != 0)
        {
            foodFound = true;
            
        }
        if (foodFound)
        {
            animator.SetBool("isEating", true);
            animator.gameObject.GetComponent<MyAnky>().prevState = 4;
        }
        //herding checks
        if (animator.gameObject.GetComponent<MyAnky>().alpha == null && animator.gameObject.GetComponent<FieldOfView>().visibleAnkys.Count > 0)
        {
            animator.gameObject.GetComponent<MyAnky>().alpha = animator.gameObject.GetComponent<FieldOfView>().visibleAnkys[0].gameObject;
        }       
        //checking if alpha anky has died
        if (animator.gameObject.GetComponent<MyAnky>().alpha.GetComponent<MyAnky>().dead)
        {
            if (animator.gameObject.GetComponent<FieldOfView>().visibleAnkys.Count>1)
            {
                animator.gameObject.GetComponent<MyAnky>().alpha = animator.gameObject.GetComponent<FieldOfView>().visibleAnkys[1].gameObject;
            }
            else
            {
                animator.gameObject.GetComponent<MyAnky>().alpha = null;
            }
        }
        //moving towards alpha anky if it starts to get close to end of field of view
        if (Vector3.Distance(animator.gameObject.transform.position, animator.gameObject.GetComponent<MyAnky>().alpha.transform.position) > 60)
        {
            animator.SetBool("isHerding", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving Grazing State");
        animator.gameObject.GetComponent<Wander>().enabled = false;
        animator.SetBool("isGrazing", false);
    }
}
