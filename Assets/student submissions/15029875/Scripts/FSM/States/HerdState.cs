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
        this.agent.transform.Translate(Vector3.forward * Time.deltaTime);
    }
}