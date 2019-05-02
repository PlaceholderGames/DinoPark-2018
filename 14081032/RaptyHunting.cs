using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyHunting : StateMachineBehaviour
{

    public Transform foodSource;
    public float thirst = 0;
    public float hunger = 0;
    public float speed;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Rapty has entered Hunting State");

        animator.SetBool("isHunting", true);
        animator.SetBool("isIdle", false);

        hunger = animator.GetFloat("hungerValue");
        thirst = animator.GetFloat("thirstValue");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //simple ticker that increases the hunger and thirst of the Rapty
        thirst += 0.3f;
        hunger += 0.8f;
        animator.SetFloat("thirstValue", thirst);
        animator.SetFloat("hungerValue", hunger);

        if (hunger >= 5)
        {
            //locate closet Anky and move towards it
            foodSource = GameObject.FindGameObjectWithTag("Anky").transform;
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, foodSource.position, speed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
