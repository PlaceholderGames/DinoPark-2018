using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class AnkyAttack : State<MyAnky> { // Anky attack state so when Rapty is in the near vicinity it will switch to this state

    private static AnkyAttack _ankyInstance; // Anky attack instance

    private AnkyAttack()
    {
        if (_ankyInstance != null)
            return;

        _ankyInstance = this;
    }
    
    public static AnkyAttack AnkyInstance // Anky attack instance
    {
        get
        {
            if (_ankyInstance == null)
                new AnkyAttack();

            return _ankyInstance;
        }
    }

    public override void EnterState(MyAnky CurrentParentOf) // Enters attacking state
    {
        CurrentParentOf.Animator.SetBool("isAttacking", true);
    }

    public override void UpdateState(MyAnky CurrentParentOf) // Updates the attacking state
    {
        if (CurrentParentOf.Anky_Health < 0.0f)
        {
            CurrentParentOf.Facing.enabled = false;
            CurrentParentOf.Facing.target = null;
            CurrentParentOf.State.ChangeState(AnkyDead.AnkyInstance); // If Anky health is less than 0 then switch to Anky dead
        }

        if (CurrentParentOf.AnkyPredators.Count == 0)
        {
            CurrentParentOf.Facing.enabled = false;
            CurrentParentOf.Facing.target = null;
            CurrentParentOf.State.ChangeState(AnkyAlerted.AnkyInstance); // If no predators close then switch to Anky alert
        }

        if (!CurrentParentOf.Facing.enabled)
        {
            CurrentParentOf.Facing.enabled = true;
            CurrentParentOf.Facing.target = CurrentParentOf.TrackNearestPredator().gameObject; // If Face script is not enabled on the Anky then face and track the nearest predator
        }
    }

    public override void ExitState(MyAnky CurrentParentOf)  //Exits the attack state
    {
        CurrentParentOf.Animator.SetBool("isAttacking", false);
    }
}
