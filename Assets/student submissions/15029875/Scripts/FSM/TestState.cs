using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : StateBase
{
    // Static variable declared once.
    private static AgentBase agent;

    public TestState(AgentBase owner) { agent = owner; }

    public override void BeginState(StateMachine SM)
    {
        TestExists();
    }

    public override void EndState(StateMachine SM)
    {
        TestExists();
    }

    public override void UpdateState(StateMachine SM)
    {
        TestExists();
    }

    void TestExists()
    {
        Debug.Log("I exist!");
    }
}
