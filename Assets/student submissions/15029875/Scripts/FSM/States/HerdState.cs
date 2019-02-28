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
        agent.transform.Translate(Vector3.forward * Time.deltaTime);
        RandomDirection();
    }

    // Get a vector to move the agent in a random direction.
    Vector3 RandomDirection()
    {
        // First set a random direction.
        var x = Random.Range(0.0f, 5.0f);
        var z = Random.Range(0.0f, 5.0f);

        Vector3 newDirection = new Vector3(x, 0, z);
        // Sort the y here.
        Debug.Log("THE Y IS: ");
        Debug.Log(newDirection);
        Debug.Log(newDirection.y);

        return newDirection;
        // Then move towards this location.

        // Make sure we clamp Y axis to the height map (if we have to).

    }
}