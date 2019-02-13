using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : StateBase
{
    // Static variable declared once.
    private static TestState instance;

    // Accessor function.
    public static TestState Instance
    {
        get
        {
            // If there isn't an instance,
            if (instance == null)
            {
                // Call our constructor.
                new TestState();
            }
            // Once an instance exists, return it.
            return instance;
        }
    }

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
