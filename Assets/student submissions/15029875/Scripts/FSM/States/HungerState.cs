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

    public HungerState(AgentBase parsedAgent, List<Transform> parsedTargets)
    {
        agent = parsedAgent;
        targets = parsedTargets;
    }

    public void BeginState()
    {
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
    }

    public void UpdateState()
    {
        if (closestTarget != null)
        {
            Eat(closestTarget);
        }
        // If we reach 0 hunger, become idle.
        if (agent.hunger <= 0)
        {
            agent.hungry = false;
            agent.hunger = 0; // Just in case we hit the negatives.
            agent.stateMachine.SwitchState(new IdleState(agent));
        }
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
        if (tag == "Dinos")
        {
            if (GetClosestTarget(targets, tag).GetComponent<AgentAnky>().herding == true)
            {
                Debug.Log("I'm hungry, but this target is herding - dangerous!");
            }
            else
            {
                closestTarget = GetClosestTarget(targets, tag);
            }

        }
        else
        {
            closestTarget = GetClosestTarget(targets, tag);
        }
    }

    // Eat the food.
    public void Eat(Transform edible)
    {
        // Approach the food.
        var step = agent.speed * Time.deltaTime;
        var targetRotation = Quaternion.LookRotation(edible.transform.position - agent.transform.position);

        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRotation, step);
        agent.transform.position = Vector3.MoveTowards(agent.transform.position, edible.transform.position, step);
    }
}