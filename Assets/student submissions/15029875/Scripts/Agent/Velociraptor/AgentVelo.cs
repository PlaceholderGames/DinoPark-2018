using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// The Bison agent is special in that it has the capacity to herd
// as well as idle.
public class AgentVelo : AgentBase
{

    // Constructor.
    private void Start()
    {
        this.stateMachine =  new StateMachine();
        this.FOV = this.GetComponent<FieldOfView>();
        this.health = Random.Range(90, 100);
        this.hunger = Random.Range(0, 40);
        this.speed = 6;
        this.stateMachine.SwitchState(new IdleState(this));
        this.hungry = false;
        this.dead = false;
    }

    public override void Update()
    {
        this.stateMachine.Update();
        Horology();

        // CLAMP ROTATION. Dinos are IDIOTS!
        if (this.transform.rotation.x <= 0.0f)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5.0f);
        }
        if (this.transform.rotation.z <= 0.0f)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5.0f);
        }
}

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dinos")
        {
            this.hunger -= 60;
        }
    }

    public override void Horology()
    {
        this.hunger += Time.deltaTime;
    }
}
