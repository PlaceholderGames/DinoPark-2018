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

    //find all game objects with this tag
    void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //this will be executed no matter what the dinos are doing at this moment
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //even if the 'quest' that the dino is doing at the moment, it would still go back to the original position
        //and restart 'idle' again after being interrupted
        moveDino = animator.gameObject;
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWaypoint = 0;

        //user message in console
        Debug.Log("Rapty is entering the Idle State...");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
        wander.enabled = false;
        Debug.Log("Rapty is exiting the Idle State...");
    }
}