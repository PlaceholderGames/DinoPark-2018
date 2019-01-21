using System;
using UnityEngine;

/// <summary>
/// Finite-state machine class.
/// Manages the update cycle and changing of states.
/// </summary>
[RequireComponent(typeof(Entity))]
public class FSM : MonoBehaviour
{
    /// <summary>
    /// Current state the FSM is current at.
    /// </summary>
    private FSMState current;

    private void Update()
    {
        if (current != null)
            current.Update();
    }

    /// <summary>
    /// Allows for proper  change between FSM states.
    /// </summary>
    /// <param name="nState">New state to change to.</param>
    public void ChangeState(FSMState nState)
    {
        current = nState;
        current.Start();
    }

    /// <summary>
    /// Returns the current state as is.
    /// </summary>
    public FSMState GetState() { return current; }
}