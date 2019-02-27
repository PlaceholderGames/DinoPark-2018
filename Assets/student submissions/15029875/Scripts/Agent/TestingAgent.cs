using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

public class TestingAgent : AgentBase {

    private StateMachine stateMachine = new StateMachine();
    // Constructor.
    public TestingAgent()
    {
        this.health = 100;
        this.stateMachine.SwitchState(new TestState());
    }

    public override void Update()
    {
        this.stateMachine.Update();
    }
}
