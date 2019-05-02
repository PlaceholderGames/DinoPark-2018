using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class RaptyIdle : State<MyRapty>  // This is a state for the Rapty idle which is the beginning state
{
    private static RaptyIdle _raptyInstance; // Idle Instance

    private RaptyIdle()
    {
        if (_raptyInstance != null)
            return;

        _raptyInstance = this;
    }

    public static RaptyIdle RaptyInstance  // Idle Instance
    {
        get
        {
            if (_raptyInstance == null)
                new RaptyIdle();

            return _raptyInstance;
        }
    }

    public override void EnterState(MyRapty CurrentParentOf)   // Enters idle state
    {
        CurrentParentOf.Animator.SetBool("isIdle", true);
    }

    public override void UpdateState(MyRapty CurrentParentOf)   // Updates idle state
    {
        CurrentParentOf.State.ChangeState(RaptyHunt.RaptyInstance);  // Change state to Rapty hunt
    }

    public override void ExitState(MyRapty CurrentParentOf)  // Exits idle state
    {
        CurrentParentOf.Animator.SetBool("isIdle", false);
    }

  
}
