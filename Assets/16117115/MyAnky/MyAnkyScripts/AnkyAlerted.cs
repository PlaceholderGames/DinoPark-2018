using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class AnkyAlerted : State<MyAnky> // Anky alerted is so that the Anky has visual awareness of what is around it
{
    private static AnkyAlerted _ankyInstance; //Anky instance for Anky alerted

    private AnkyAlerted()
    {
        if (_ankyInstance != null)
            return;

        _ankyInstance = this;
    }

    public static AnkyAlerted AnkyInstance
    {
        get
        {
            if (_ankyInstance == null)
                new AnkyAlerted();

            return _ankyInstance;
        }
    }

    public override void EnterState(MyAnky CurrentParentOf)
    {
        CurrentParentOf.Animator.SetBool("isAlerted", true); // Enters Anky alert state
    }

    public override void UpdateState(MyAnky CurrentParentOf)
    {

        if (CurrentParentOf.AnkyPredators.Count == 0)
            CurrentParentOf.State.ChangeState(AnkyGrazing.AnkyInstance);

        if (CurrentParentOf.AnkyPredators.Count == 0)
        {
            CurrentParentOf.TrackNearestPredator();
            CurrentParentOf.State.ChangeState(AnkyAttack.AnkyInstance);   //Updates state to either attack, fleeing or grazing depending on the predator count near the Anky
        }

        if (CurrentParentOf.AnkyPredators.Count >= 2)
            CurrentParentOf.State.ChangeState(AnkyFleeing.AnkyInstance);
    }

    public override void ExitState(MyAnky CurrentParentOf)
    {
        CurrentParentOf.Animator.SetBool("isAlerted", false); //Exits the Anky alerted state
    }
}
