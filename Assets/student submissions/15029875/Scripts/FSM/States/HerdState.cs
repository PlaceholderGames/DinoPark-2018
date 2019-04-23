using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// State called to make multiple agents herd together. This is cherry and not yet implemented,
// but will make for some fun behaviour as opposed to the standard idle state.
public class HerdState : IState
{
    private GameObject agent;

    public HerdState(GameObject agent)
    {
        this.agent = agent;
    }

    public void BeginState()
    {
        Debug.Log("Entered herd state.");
    }

    public void EndState()
    {
        Debug.Log("Exited herd state.");
    }

    public void UpdateState()
    {
        Herd();
    }

    void Herd()
    {
        MoveForward();
        // Every random seconds, change direction:
        // I am VERY UNCERTAIN about the while true...
        while(true)
        {
            var randomNumber = Random.Range(0.0f, 5.0f);

        }

    }

    void MoveForward()
    {
        // Work here. Figure out why we can't use speed instead of 0.1f.
        // Must use MovePosition to take into account the terrain.
        agent.GetComponent<Rigidbody>().MovePosition(agent.transform.position + agent.transform.forward * 0.1f);
    }

    // Get a vector to move the agent in a random direction.
    void RandomDirection()
    {
        // First set a random direction.
        var x = Random.Range(0.0f, 5.0f);
        var z = Random.Range(0.0f, 5.0f);

        Vector3 newDirection = new Vector3(x, 0, z);
        // Sort the y here.
        Debug.Log("THE Y IS: ");
        Debug.Log(newDirection);
        Debug.Log(newDirection.y);
        // Then move towards this location.

        // Make sure we clamp Y axis to the height map (if we have to).
    }
}