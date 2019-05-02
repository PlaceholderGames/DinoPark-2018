using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class RaptyFleeing : State<MyRapty> // This is a script for Rapty fleeing
{
    private static RaptyFleeing _raptyInstance;  // Fleeing Instance

    private RaptyFleeing()
    {
        if (_raptyInstance != null)
            return;

        _raptyInstance = this;
    }

    public static RaptyFleeing RaptyInstance    // Fleeing Instance
    {
        get
        {
            if (_raptyInstance == null)
                new RaptyFleeing();

            return _raptyInstance;
        }
    }

    public override void EnterState(MyRapty CurrentParentOf)   // Enter fleeing state
    {
        CurrentParentOf.Animator.SetBool("isFleeing", true);
    }

    public override void UpdateState(MyRapty CurrentParentOf)   // Updates fleeing state
    {
        if (CurrentParentOf.Rapty_Health < 0.0f)
            CurrentParentOf.State.ChangeState(RaptyDead.RaptyInstance);   // If Rapty health is less than 0 then switch to Rapty dead state
        else
        {
            if (!CurrentParentOf.Fleeing.enabled)
            {
                CurrentParentOf.Fleeing.target = CurrentParentOf.RetrieveNearestDeadAnky().gameObject;   // If fleeing script not enabled then enable it and retrieve nearby Anky
                CurrentParentOf.Fleeing.enabled = true;
            }
            else if (Vector3.Distance(CurrentParentOf.transform.position, CurrentParentOf.Fleeing.target.transform.position) > 300.0f)   // If distance greater than 300 then switch state to Rapty hunt
            {
                CurrentParentOf.Fleeing.enabled = false;
                CurrentParentOf.Fleeing.target = null;
                CurrentParentOf.State.ChangeState(RaptyHunt.RaptyInstance);
            }
        }
    }

    public override void ExitState(MyRapty CurrentParentOf)  // Exits fleeing state
    {
        CurrentParentOf.Animator.SetBool("isFleeing", false);
    }

}