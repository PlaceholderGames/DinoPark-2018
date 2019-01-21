using System;
using UnityEngine;

/// <summary>
/// Finite-state machine component. Manages the update cycle and changing of States.
/// The FSM is designed to be self-contained and basically independent as possible
/// from Entity component class so that modifications to code are easier. FSMCommon
/// will be the bridge between them.
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
    /// Allows for proper (managed) change between FSM states.
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