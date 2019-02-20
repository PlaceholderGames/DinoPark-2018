// Owned by an agent.
// Manages the behaviour for said agent.
namespace StateMachineInternals
    {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StateMachine : MonoBehaviour {

        // Point to the current state.
        private StateBase currentState;

        public void SwitchState(StateBase nState)
        {
            // First end the current state.
            currentState.EndState(this);
            // Then set the current state to the new state.
            currentState = nState;
            // Then begin the new state.
            currentState.BeginState(this);
        }

        public void Update()
        {
            // If we have a current state:
            if (currentState != null)
            {
                // Run the state's update function.
                currentState.UpdateState(this);
            }
        }
    }

    // As this is a base class for inherited states
    // it's by best design to make this an abstract class
    // allowing us to initialise functions in their own inherited classes.
    public abstract class StateBase
    {

        public abstract void BeginState(StateMachine SM);
        public abstract void UpdateState(StateMachine SM);
        public abstract void EndState(StateMachine SM);

    }

}