using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//adding the namespace that was just created
using StateAttributes;


public class AI : MonoBehaviour
{
    //holds the data for the transition state
    public bool switchingState = false;
    public float gameTimer;
    public int seconds = 0;

    public StateMachine<AI> stateMachine { get; set; }

    private void Start()
    {
        //referencing the new state 
        stateMachine = new StateMachine<AI>(this);
        //not declaring a new state but using the instance that already exists
        stateMachine.changingStates(FirstState.Instance);
        gameTimer = Time.time;
    }

    private void Update()
    {
        //check if the game time has hit the gool
        //and reset
        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            //pass in how many seconds the time to go by
            Debug.Log(seconds);
        }

        //switch the state every 30 seconds
        if (seconds == 30)
        {
            //reset the state and start counting again
            seconds = 0;
            switchingState = !switchingState;
        }

        //update, wthaever the current state is 
        stateMachine.update();
    }
}