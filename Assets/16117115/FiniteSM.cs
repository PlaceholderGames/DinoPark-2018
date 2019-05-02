using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace FiniteSM     // This is a finite state machine which is made within a namespace which can be included in every script. In each script there will be a different state
{
    public class FiniteSM<T>
    {
        public State<T> ActiveState { get; private set; }  // Sets the active state
        public T Parent;

        public FiniteSM(T CurrentParentOf)   // Sets the current parent and sets active state to null
        {
            Parent = CurrentParentOf;
            ActiveState = null;
        }

        public void ChangeState(State<T> newActiveState)  // Change state function which if active state is not equal to null then it will exit and go to new active state and enter it
        {
            if (ActiveState != null)
                ActiveState.ExitState(Parent);
            ActiveState = newActiveState;
            ActiveState.EnterState(Parent);
        }

        public void UpdateCurrentState()  // Updates the current state if active state is not equal to null
        {
            if (ActiveState != null)
                ActiveState.UpdateState(Parent);
        }

    }

    public abstract class State<T>
    {
        public abstract void EnterState(T CurrentParentOf);
        public abstract void UpdateState(T CurrentParentOf);    // State class for updating, exiting and entering states.
        public abstract void ExitState(T CurrentParentOf);
    }

}
