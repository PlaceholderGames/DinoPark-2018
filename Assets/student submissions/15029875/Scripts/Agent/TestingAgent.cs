using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAgent : AgentBase {

    // Create an instance of the state machine.
    StateMachine stateMachine;

    // Constructor.
    public TestingAgent()
    {
        this.HP = 100;
        // Set a state.
        stateMachine.SwitchState(new TestState(this));
    }

    public override void Update()
    {

    }
}
