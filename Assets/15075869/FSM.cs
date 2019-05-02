using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    //This is the basis for the finite state machine
    public class FSM<D>
    {
        public State<D> current_state { get; private set; }
        public D Parent;

        public FSM (D parent)
        {
            Parent = parent;
            current_state = null;
        }

        public void Change(State<D> new_state)
        {
            if (current_state != null)
                current_state.OnStateExit(Parent);
            current_state = new_state;
            current_state.OnStateEnter(Parent);
        }

        // Update is called once per frame
        public void Update()
        {
            if (current_state != null)
                current_state.OnStateUpdate(Parent);
        }
    }

    public abstract class State<D>
    {
        public abstract void OnStateEnter(D parent);
        public abstract void OnStateUpdate(D parent);
        public abstract void OnStateExit(D parent);
    }
}
