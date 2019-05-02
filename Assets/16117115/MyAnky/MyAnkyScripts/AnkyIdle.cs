using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class AnkyIdle : State<MyAnky> // This state is the Anky idle state which is the state which the Anky is given at the start of the game
{
    private static AnkyIdle _ankyInstance;  // Anky Idle Instance

    private AnkyIdle()
    {
        if (_ankyInstance != null)
            return;

        _ankyInstance = this;
    }

    public static AnkyIdle AnkyInstance  // Anky Idle Instance
    {
        get
        {
            if (_ankyInstance == null)
                new AnkyIdle();

            return _ankyInstance;
        }
    }

    public override void EnterState(MyAnky CurrentParentOf)  // Enters Idle state
    {
        CurrentParentOf.Animator.SetBool("isIdle", true);
    }

    public override void UpdateState(MyAnky CurrentParentOf)   // Updates Idle state
    {
        CurrentParentOf.State.ChangeState(AnkyGrazing.AnkyInstance);
    }

    public override void ExitState(MyAnky CurrentParentOf)    // Exits Idle state
    {
        CurrentParentOf.Animator.SetBool("isIdle", false);
    }

}