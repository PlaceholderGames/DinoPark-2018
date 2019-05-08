using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// The alert state is triggered when an Ankys
// detects a nearby dinosaur.

public class AlertState : IState
{
    AgentBase agent;
    List<Transform> targets;

    public AlertState(AgentBase parsedAgent, List<Transform> parsedTargets)
    {
        agent = parsedAgent;
        targets = parsedTargets;
    }

    public void BeginState()
    {
    }

    public void EndState()
    {
        agent.alert = false;
        agent.stateMachine.SwitchState(new IdleState(agent));
    }

    public void UpdateState()
    {
    }

    public void Scan()
    {
        // Scan the FOV. If we see no dinos, all good.
        // Otherwise head the opposite direction.
        foreach(Transform target in targets)
        {
            if (target.tag == "Rapty")
            {
                agent.transform.position = agent.transform.position - target.transform.position;
                Debug.Log("Should be turning from threat.");
            }
            else
            {
                EndState();
            }
        }

        // Set safe to true.

    }
}
