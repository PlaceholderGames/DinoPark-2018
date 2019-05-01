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
        this.health = Random.Range(90, 100);
        this.hunger = Random.Range(0, 40);
        this.speed = 3;
        this.stateMachine.SwitchState(new IdleState(this.gameObject, this.speed));
    }

    public override void Update()
    {
        this.stateMachine.Update();
        Horology();
        

        if(this.hunger >= 100)
        {
            this.stateMachine.SwitchState(new HungerState(this.gameObject));
        }

        this.stateMachine.Update();
    }

    public override void Horology()
    {
        this.hunger += Time.deltaTime;
    }
}
