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
        this.hunger = Random.Range(0, 40);
        this.speed = 4;
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
        if (hungry == true && other.gameObject.tag == "FoliageTag")
        {
            // We lose the current target after this. I've worked a bit on a solution but as I'm low on time
            // I think we can just switch to an idle state to reset the hunger state if we need to find a new target.
            this.stateMachine.SwitchState(new HungerState(this, FOV.visibleTargets));
            //Destroy(other.gameObject); Don't destroy the game object. Too late to fix.
            this.hunger -= 80;

        }
    }

    public override void Horology()
    {
        this.hunger += Time.deltaTime;
    }
}