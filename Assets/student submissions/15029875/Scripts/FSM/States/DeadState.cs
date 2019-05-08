using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

public class DeadState : IState
{
    AgentBase agent;

    public DeadState(AgentBase parsedAgent)
    {
        agent = parsedAgent;
    }

    public void BeginState()
    {
        Debug.Log("Entered DEAD state.");
    }

    public void EndState()
    {
        Debug.Log("Exited state.");
    }

    public void UpdateState()
    {
        Debug.Log("I'm dead.");
    }
}