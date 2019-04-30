using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// Class called when the agent is idling. This may need to be inherited from to
// call behaviour for different agents, but this basic state
// is essentially going to wander in different directions.
public class HungerState : IState
{
    public void BeginState()
    {
        Debug.Log("Entered hunger state.");
    }

    public void EndState()
    {
        Debug.Log("Exited hunger state.");
    }

    public void UpdateState()
    {
    }

}