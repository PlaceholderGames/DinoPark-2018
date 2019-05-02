using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class AnkyFleeing : State<MyAnky> // This state is for the Anky to flee from predators
{
    private static AnkyFleeing _ankyInstance; // Instance for fleeing

    private AnkyFleeing()
    {
        if (_ankyInstance != null)
            return;

        _ankyInstance = this;
    }

    public static AnkyFleeing AnkyInstance   // Instance for fleeing
    {
        get
        {
            if (_ankyInstance == null)
                new AnkyFleeing();

            return _ankyInstance;
        }
    }

    public override void EnterState(MyAnky CurrentParentOf)  // Enters Fleeing state
    {
        CurrentParentOf.Animator.SetBool("isFleeing", true);
    }

    public override void UpdateState(MyAnky CurrentParentOf)  // Updates fleeing state
    {
        if (!CurrentParentOf.Fleeing.enabled)
        {
            CurrentParentOf.Fleeing.target = CurrentParentOf.TrackNearestPredator().gameObject;   // If flee script not enabled then enable and track nearest predator
            CurrentParentOf.Fleeing.enabled = true;
        }
        else
        {
            if (CurrentParentOf.Anky_Health <= 0.0f)
            {
                CurrentParentOf.Fleeing.enabled = false;
                CurrentParentOf.Fleeing.target = null;
                CurrentParentOf.State.ChangeState(AnkyDead.AnkyInstance);   // If Anky health is 0 or less, then return Anky dead state
            }
            else if (Vector3.Distance(CurrentParentOf.transform.position, CurrentParentOf.Fleeing.target.transform.position) > 150.0f)
            {
                CurrentParentOf.Fleeing.enabled = false;
                CurrentParentOf.Fleeing.target = null;
                CurrentParentOf.State.ChangeState(AnkyAlerted.AnkyInstance);  // If predator is over 150.0f away then change to alert state
            }
        }
    }

    public override void ExitState(MyAnky CurrentParentOf)   // Exits fleeing state
    {
        CurrentParentOf.Animator.SetBool("isFleeing", false);
    }

}