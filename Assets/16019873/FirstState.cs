using UnityEngine;
//adding the namespace that was just created
using StateAttributes;


//class that will be attached to the main AI script
public class FirstState : State<AI>
{
    //all states will be shader between all classes (AI, MyAnky, MyRapty, etc.)
    //it saves a lot of memory and is good for the CPU
    private static FirstState instance;

    //constructor - this state can only be created within the state class
    private FirstState()
    {
        if (instance != null)
        {
            return;
        }

        //setting the instance to only be etual to one instance of the state
        instance = this;
    }

    //this method is created in order to be able to access the instance value above
    public static FirstState Instance
    {
        //static instance
        get
        {
            //if there is no instance, create one by calling the private constructor
            if (instance == null)
            {
                new FirstState();
            }

            //return the value back 
            return instance;
        }
    }

    //this method wil create a unity log to display in the terminal 
    //if an event has been carried out
    public override void enterState(AI owner)
    {
        Debug.Log("Now entering the first state..");
    }

    //this method wil create a unity log to display in the terminal 
    //if an event has been carried out
    public override void exitState(AI owner)
    {
        Debug.Log("Now exiting the first state..");
    }

    public override void updateState(AI owner)
    {
        //if the owner is equal to true
        if(owner.switchingState)
        {
            owner.stateMachine.changingStates(SecondState.Instance);
        }
    }
}