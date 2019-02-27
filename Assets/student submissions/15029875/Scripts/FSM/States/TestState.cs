using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

public class TestState : IState
{
    public void BeginState()
    {
        Debug.Log("Entered state.");
    }

    public void EndState()
    {
        Debug.Log("Exited state.");
    }

    public void UpdateState()
    {
        TestExists();
    }

    void TestExists()
    {
        Debug.Log("I exist!");
    }
}