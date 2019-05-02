using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// Class called when the agent is idling. This may need to be inherited from to
// call behaviour for different agents, but this basic state
// is essentially going to wander in different directions.
public class HungerState : IState
{
    AgentBase agent;
    List<Transform> targets;
    Transform closestTarget;
    float speed;

    public HungerState(AgentBase parsedAgent, float parsedSpeed, List<Transform> parsedTargets)
    {
        agent = parsedAgent;
        targets = parsedTargets;
        speed = parsedSpeed;
    }

    public void BeginState()
    {
        // Decrease speed.
        // To save writing two states for each dinosaur, it's probably more efficient to perform
        // a check here and see whether we're an anky or a velociraptor:
        if (agent.name == "Velociraptor(Clone)" || agent.name == "Velociraptor")
        {
            Seek("Dinos");
        }
        if (agent.name == "Ankylosi(Clone)" || agent.name == "Ankylosi")
        {
            Seek("FoliageTag");
        }
    }

    public void EndState()
    {
        // If we reach 0 hunger, become idle.

        // Else, we must be dead!
    }

    public void UpdateState()
    {

    }

    Transform GetClosestTarget(List<Transform> desiredTarget, string tag)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = agent.transform.position;
        foreach (Transform t in desiredTarget)
        {
            // Only search for the parsed tag - anky cannot eat velociraptor...
            if (t.tag == tag)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }

    // Search for something.
    public void Seek(string tag)
    {
        Debug.Log("I am a " + agent.name + "And I am searching for: " + tag);
        Eat(GetClosestTarget(targets, tag));
    }

    // Eat the food.
    public void Eat(Transform edible)
    {
        // Approach the food.
        agent.transform.LookAt(edible);
    }

}