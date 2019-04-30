using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// The Bison agent is special in that it has the capacity to herd
// as well as idle.
public class AgentVelo : AgentBase
{
    private StateMachine stateMachine = new StateMachine();

    // Constructor.
    private void Start()
    {
        this.health = 100;
        this.speed = 1;
        this.stateMachine.SwitchState(new HungerState());
    }

    public override void Update()
    {
        this.stateMachine.Update();
        Horology();
        Debug.Log("My health is: " + this.health);
    }

    public override void Horology()
    {
        this.health -= Time.deltaTime;
    }
}
