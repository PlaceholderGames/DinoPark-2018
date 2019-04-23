using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// The Bison agent is special in that it has the capacity to herd
// as well as idle.
public class AgentBison : AgentBase
{
    private StateMachine stateMachine = new StateMachine();

    // Constructor.
    private void Start()
    {
        // Extra health... bison are tough.
        this.health = 200;
        this.speed = 1;
        this.stateMachine.SwitchState(new HerdState(this.gameObject));
    }

    public override void Update()
    {
        this.stateMachine.Update();
    }
}
