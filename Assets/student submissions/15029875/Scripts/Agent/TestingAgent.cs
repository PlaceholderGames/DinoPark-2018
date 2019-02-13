using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAgent : AgentBase {

    // Create an instance of the state machine.
    private StateMachine currentState;

    // Constructor.
    public TestingAgent()
    {
        this.HP = 100;
        // Set a state.
        currentState.SwitchState(TestState.Instance);
    }

    public override void Update()
    {
        currentState.Update();
    }
}
