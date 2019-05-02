using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class RaptyAlerted : State<MyRapty>  // State for Rapty alert
{
    private static RaptyAlerted _raptyInstance;  // Alert Instance

    private RaptyAlerted()
    {
        if (_raptyInstance != null)
            return;

        _raptyInstance = this;
    }

    public static RaptyAlerted RaptyInstance  // Alert Instance
    {
        get
        {
            if (_raptyInstance == null)
                new RaptyAlerted();

            return _raptyInstance;
        }
    }

    public override void EnterState(MyRapty CurrentParentOf)  // Enters alert state
    {
        CurrentParentOf.Animator.SetBool("isAlerted", true);
    }

    public override void UpdateState(MyRapty CurrentParentOf)  // Updates alert state
    {
        if (CurrentParentOf.NearestLiveAnky.Count == 0)
        {
            CurrentParentOf.State.ChangeState(RaptyHunt.RaptyInstance);  // If number of dino is equal to 0 then switch state to Hunt
        }
        else
        {
            if (CurrentParentOf.Rapty_Health >= 20.0f)
                CurrentParentOf.State.ChangeState(RaptyAttack.RaptyInstance);   // If Rapty health is greater than or equal to then switch to attack state
            else
                CurrentParentOf.State.ChangeState(RaptyFleeing.RaptyInstance);  // Else switch to flee state if less than 20 health
        }
    }

    public override void ExitState(MyRapty CurrentParentOf)   // Exits alert state
    {
        CurrentParentOf.Animator.SetBool("isAlerted", false);
    }
}