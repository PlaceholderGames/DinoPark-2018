using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// State called to make multiple agents herd together. This is cherry and not yet implemented,
// but will make for some fun behaviour as opposed to the standard idle state.
public class HerdState : IState
{
    private AgentBase agent;
    List<Transform> targets;
    GameObject[] availableGoals;
    Vector3 herdVector;

    public HerdState(AgentBase parsedAgent, List<Transform> parsedTargets)
    {
        agent = parsedAgent;
        targets = parsedTargets;
        agent.herding = true;
    }

    public void BeginState()
    {
    }

    public void EndState()
    {
    }

    public void UpdateState()
    {
        // If we're near another anky, then we must be herding.
        foreach (Transform target in targets)
        {
            if (target.gameObject.tag == "Dinos") // Dinos are Ankys. Can't be faffed to make a new tag right now.
            {
                Herd();
            }
            else
            {
                agent.herding = false;
                agent.stateMachine.SwitchState(new IdleState(agent));
            }
        }

        if (agent.hunger >= 45 && agent.hunger <= 100 && agent.hungry == false)
        {
            Debug.Log(agent.name + "Hunger is true.");
            agent.herding = false;
            agent.hungry = true;
            agent.stateMachine.SwitchState(new HungerState(agent, agent.FOV.visibleTargets));
        }
        // Else end the state and idle until otherwise.
    }

    void Herd()
    {
        Vector3 A = Alignment(agent) * 0.5f * Time.deltaTime;
        Vector3 S = Separation(agent) * 0.1f * Time.deltaTime;
        Vector3 C = Cohesion(agent) * 0.9f * Time.deltaTime;

        agent.GetComponent<Rigidbody>().velocity += (A + S + C);

        // Find the closest "goal" on the map.
        availableGoals = GameObject.FindGameObjectsWithTag("Goal");
        agent.transform.LookAt(GetClosestGoal(availableGoals, "Goal"));
        agent.transform.position = Vector3.MoveTowards(agent.transform.position, GetClosestGoal(availableGoals, "Goal").position, agent.speed - 3.95f);
    }

    // Maintain an average direction.
    private Vector3 Alignment(AgentBase boid)
    {
        Vector3 alignment = new Vector3();
        // Get a list of transforms for all nearby Anky dinosauras.
        foreach (Transform target in targets)
        {
            if (target.gameObject.tag == "Dinos") // Dinos are Ankys. Can't be faffed to make a new tag right now.
            {
                alignment += target.forward;
            }
        }

        // Normalize the vector.
        alignment.Normalize();

        agent.transform.forward = alignment;
        return alignment;
    }

    // Maintain an average velocity.
    private Vector3 Cohesion(AgentBase boid)
    {
        Vector3 cohesion = new Vector3();
        // Get a list of transforms for all nearby Anky dinosauras.
        foreach (Transform target in targets)
        {
            if (target.gameObject.tag == "Dinos") // Dinos are Ankys. Can't be faffed to make a new tag right now.
            {
                cohesion += target.gameObject.GetComponent<Rigidbody>().velocity;
            }
        }

        // Normlize the vector.
        cohesion.Normalize();
        return cohesion;
    }

    // Keep a certain distance away from other boids.
    private Vector3 Separation(AgentBase boid)
    {
        Vector3 separation = new Vector3();
        // Get a list of transforms for all nearby Anky dinosauras.
        foreach (Transform target in targets)
        {
            if (target.gameObject.tag == "Dinos") // Dinos are Ankys. Can't be faffed to make a new tag right now.
            {
                separation += target.position - agent.transform.position;
            }
        }
        return separation;
    }

    Transform GetClosestGoal(GameObject[] goals, string tag)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = agent.transform.position;
        for (int t = 0; t < goals.Length; t ++)
        {
            // Only search for the parsed tag - anky cannot eat velociraptor...
            if (goals[t].tag == tag)
            {
                float dist = Vector3.Distance(goals[t].transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = goals[t].transform;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }
}