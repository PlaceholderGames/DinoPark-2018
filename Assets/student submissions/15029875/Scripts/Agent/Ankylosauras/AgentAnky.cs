using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// The Bison agent is special in that it has the capacity to herd
// as well as idle.
public class AgentAnky : AgentBase
{
    private StateMachine stateMachine = new StateMachine();

    // Constructor.
    private void Start()
    {
        this.health = 200;
        this.speed = 4;
        this.stateMachine.SwitchState(new IdleState(this.gameObject, this.speed));
    }

    public override void Update()
    {
        this.stateMachine.Update();
    }

    public override void Horology()
    {
        throw new System.NotImplementedException();
    }

}