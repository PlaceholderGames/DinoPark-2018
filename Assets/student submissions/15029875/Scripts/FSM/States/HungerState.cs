using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// Class called when the agent is idling. This may need to be inherited from to
// call behaviour for different agents, but this basic state
// is essentially going to wander in different directions.
public class HungerState : IState
{
    GameObject agent;

    public HungerState(GameObject parsedAgent)
    {
        agent = parsedAgent;
    }

    public void BeginState()
    {
        // Decrease speed.
        
    }

    public void EndState()
    {
        // If we reach 0 hunger, become idle.

        // Else, we must be dead!
    }

    public void UpdateState()
    {
        // To save writing two states for each dinosaur, it's probably more efficient to perform
        // a check here and see whether we're an anky or a velociraptor:
        if(agent.name == "Velociraptor(Clone)")
        {
            Debug.Log("I am a velociraptor");
        }
        if (agent.name == "Ankylosi(Clone)")
        {
            Debug.Log("I am a anky");
        }
    }

    // Search for food.
    public void SeekFood()
    {

    }

    // Eat the food.
    public void EatFood()
    {

    }

}