using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class AnkyDead : State<MyAnky> { // This is the state which occurs when Anky health is 0 so it dies

    private static  AnkyDead _ankyInstance; // Anky Instance for death

    private AnkyDead()
    {
        if (_ankyInstance != null)
            return;

        _ankyInstance = this;
    }

    public static AnkyDead AnkyInstance // Anky Instance for death
    {
        get
        {
            if (_ankyInstance == null)
                new AnkyDead();

            return _ankyInstance;
        }
    }

    public override void EnterState(MyAnky CurrentParentOf) // Enters the Anky state
    {
        CurrentParentOf.Animator.SetBool("isDead", true);
    }

    public override void UpdateState(MyAnky CurrentParentOf) // Updates the death state 
    {
        CurrentParentOf.Wandering.enabled = false;

        if (CurrentParentOf.IsDead)
            CurrentParentOf.Dead();
    }

    public override void ExitState(MyAnky CurrentParentOf) // Exits the death state
    {
        //Once dead it cannot exit this state
    }

}
