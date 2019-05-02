using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;


public class RaptyDead : State<MyRapty>  // This is a state for the Rapty death
{
    private static RaptyDead _raptyInstance;   // Death Instance

    private RaptyDead()
    {
        if (_raptyInstance != null)
            return;

        _raptyInstance = this;
    }

    public static RaptyDead RaptyInstance   // Death Instance
    {
        get
        {
            if (_raptyInstance == null)
                new RaptyDead();

            return _raptyInstance;
        }
    }

    public override void EnterState(MyRapty CurrentParentOf)  // Enters death state
    {
        CurrentParentOf.Animator.SetBool("isDead", true);
    }

    public override void UpdateState(MyRapty CurrentParentOf)  // Updates death state
    {
        CurrentParentOf.Wandering.enabled = false;    // If wandering is set to false then set parent to dead

        if (CurrentParentOf.IsDead)
            CurrentParentOf.Dead();
    }

    public override void ExitState(MyRapty CurrentParentOf)   // Exits death state
    {
        CurrentParentOf.Animator.SetBool("isDead", false);
    }
}
