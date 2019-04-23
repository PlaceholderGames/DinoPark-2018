// Owned by an agent.
// Manages the behaviour for said agent.
// Using a namespace because it better encapsulates the base state class along with the
// state machine.
namespace StateMachineInternals
    {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class StateMachine : MonoBehaviour
    {

        // Point to the current state.
        private IState currentState;
        // Also hold a reference to the previous state.
        private IState previousState;

        public void SwitchState(IState nState)
        {
            // If a current state exists:
            if (this.currentState != null)
            {
                // Exit the current state.
                this.currentState.EndState();
            }            
            // Set any existing previous state to the current state.
            this.previousState = this.currentState;
            // Set the current state to the new state.
            this.currentState = nState;
            // Begin the new state.
            this.currentState.BeginState();
        }

        public void Update()
        {
            if (this.currentState != null)
            {
                this.currentState.UpdateState();
            }
        }

        public void SwitchToPrevious()
        {
            this.currentState.EndState();
            this.currentState = this.previousState;
            this.currentState.BeginState();
        }
    }


    // Interface for each state. Sets the begin, update and end functionality
    // for each state.
    public interface IState
    {

        void BeginState();
        void UpdateState();
        void EndState();

    }

}