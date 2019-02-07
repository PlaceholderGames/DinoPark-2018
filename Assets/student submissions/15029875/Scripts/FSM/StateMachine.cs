// Owned by an agent.
// Manages the behaviour for said agent.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    // Store the current state.
    private StateBase currentState;
    
    public void SwitchState(StateBase nState)
    {
        // First end the current state.
        currentState.EndS(this);
        // Then set the current state to the new state.
        currentState = nState;
        // Then begin the new state.
        currentState.BeginS(this);
    }

    // Run an update function here.
    private void Update()
    {
        // If we have a current state:
        if (currentState != null)
        {
            // Run the state's update function.
            currentState.UpdateS(this);
        }
    }
}
