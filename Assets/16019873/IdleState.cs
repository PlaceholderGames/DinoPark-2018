using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : DinoBaseClass
{
    //first object is to grab and get hold of the dino object
    GameObject moveDino;
    //loop through this array to move the dino around and make him look at the surroundings
    GameObject[] waypoints;
    //check which waypoint we are currently at when swapping states
    int currentWaypoint;

    //example of a basic Idle state: https://www.youtube.com/watch?v=aEPSuGlcTUQ
    //this is not thge method that has been used (see report for more information)

    //find all game objects with this tag
    void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //this will be executed no matter what the dinos are doing at this moment
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Debug.Log("Rapty is entering the Idle State..."); //user message in console
        
        //even if the 'quest' that the dino is doing at the moment, it would still go back to the original position
        //and restart 'idle' again after being interrupted
        moveDino = animator.gameObject;
        currentWaypoint = 0;
        ASagent.enabled = false;
        wander.enabled = true;
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        /*
        //this function will run over and over again until the dino is in this state
        if (waypoints.Length == 0) return;

        //if a waypoint is reached 
        //then update and get a new one
        //as it starts at 0
        if (Vector3.Distance(waypoints[currentWaypoint].transform.position, moveDino.transform.position) < accuracy)
        {
            currentWaypoint++;
            //creates a circuit of waypoints
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        */

        if (animator.GetFloat("thirst") < 50)
        {
            raptyAI.move(ASfollower.getDirectionVector());
            AS.target = raptyAI.waterLocation;
            ASagent.enabled = true;
        }
      

       
        //rotate towards the target that has been detected
        //move the dino forward
        //slerp is to slowly turn and start facing the waypoint
        var direction = waypoints[currentWaypoint].transform.position - moveDino.transform.position;
        moveDino.transform.rotation = Quaternion.Slerp(moveDino.transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        //pushing it forward to the z axis
        moveDino.transform.Translate(0, 0, Time.deltaTime * speed);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        //disable scripts
        wander.enabled = false;
        ASagent.enabled = false;
        Debug.Log("Rapty is exiting the Idle State...");
    }
}