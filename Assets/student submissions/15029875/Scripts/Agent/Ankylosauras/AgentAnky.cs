using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// The Bison agent is special in that it has the capacity to herd
// as well as idle.
public class AgentAnky : AgentBase
{

    // Constructor.
    private void Start()
    {
        this.stateMachine = new StateMachine();
        this.FOV = this.GetComponent<FieldOfView>();
        this.health = 200;
        //this.hunger = Random.Range(0, 40);
        this.hunger = 40;
        this.speed = 4;
        this.stateMachine.SwitchState(new IdleState(this, this.speed));
    }

    public override void Update()
    {
        this.stateMachine.Update();
        Horology();
    }

    public override void Horology()
    {
        this.hunger += Time.deltaTime;
    }

}