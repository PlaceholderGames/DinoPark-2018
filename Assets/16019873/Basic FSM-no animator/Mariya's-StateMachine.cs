using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creating a state machine namespace
namespace StateAttributes
{
    //in this class, the owner of the states, the state status etc., can be changed
    //created this class to use it as <T> because 
    //it is desired that all other components from this program can utilise and use this class
    public class StateMachine<T>
    {
        //states declaration
        public State<T> currentState { get; private set; }
        //object that is using the state machine
        public T Owner;

        //constructor
        public StateMachine(T o)
        {
            //referencing the AI scripts for the Rapty and Anky
            Owner = o;

            //at the moment, there should be no current state
            currentState = null;
        }

        //this method changes the current state to a new one
        public void changingStates(State<T> newState)
        {
            //checking if there is no current state yet
            //so that it doesn't crash when it tries to exit state
            if(currentState != null)
            {
                //1-exit the current state
                currentState.exitState(Owner);
            }

            //2-switch the state to a new one
            //3-call the enter function of the new state
            currentState = newState;
            currentState.enterState(Owner);

        }

        //updating all new updates that have came through
        public void update()
        {
            //checking if there is no current state yet
            //so that it doesn't crash when it tries to update the state
            if (currentState != null)
                currentState.updateState(Owner);
        }
    }

    //abstract state class - all state files will be in here
    public abstract class State<T>
    {
        //basic outline of the state transition function
        public abstract void enterState(T owner);
        public abstract void exitState(T owner);
        public abstract void updateState(T owner);
    }


}