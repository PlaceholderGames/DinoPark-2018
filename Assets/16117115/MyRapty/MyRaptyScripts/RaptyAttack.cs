using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class RaptyAttack : State<MyRapty>   // This state is for the Rapty attack
{
    private static RaptyAttack _raptyInstance;  // Attack Instance

    private RaptyAttack()
    {
        if (_raptyInstance != null)
            return;

        _raptyInstance = this;
    }

    public static RaptyAttack RaptyInstance   // Attack Instance
    {
        get
        {
            if (_raptyInstance == null)
                new RaptyAttack();

            return _raptyInstance;
        }
    }

    public override void EnterState(MyRapty CurrentParentOf)   // Enters attack state
    {
        CurrentParentOf.Animator.SetBool("isAttacking", true);
    }

    public override void UpdateState(MyRapty CurrentParentOf)   // Updates attack state
    {
        if (!CurrentParentOf.Seeking.enabled)
        {
            CurrentParentOf.Seeking.target = CurrentParentOf.RetrieveNearestLiveAnky().gameObject;   // If seeking is disabled then enable seeking and retrieve nearby Anky
            CurrentParentOf.Seeking.enabled = true;
        }
        else
        {
            if (!(CurrentParentOf.Seeking.target.gameObject.GetComponent<MyAnky>().State.ActiveState is AnkyDead))
            {
                if (CurrentParentOf.Rapty_Health < 0.0f)
                {
                    CurrentParentOf.Seeking.enabled = false;
                    CurrentParentOf.Seeking.target = null;
                    CurrentParentOf.State.ChangeState(RaptyDead.RaptyInstance);   // Rapty health 0 or less than switch state to Rapty dead
                }
                else if (CurrentParentOf.Rapty_Health < 20.0f)
                {
                    CurrentParentOf.Seeking.enabled = false;
                    CurrentParentOf.Seeking.target = null;
                    CurrentParentOf.State.ChangeState(RaptyFleeing.RaptyInstance);   // If Rapty health is less than 20 then switch to Rapty flee
                }
            }
            else
            {
                CurrentParentOf.Seeking.enabled = false;
                CurrentParentOf.Seeking.target = null;
                CurrentParentOf.State.ChangeState(RaptyHunt.RaptyInstance);   // Else switch state to Rapty hunt
            }
        }
    }

    public override void ExitState(MyRapty CurrentParentOf)  // Exits attack state
    {
        CurrentParentOf.Animator.SetBool("isAttacking", false);
    }

}