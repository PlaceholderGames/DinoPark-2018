using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class AnkyDrinking : State<MyAnky> // This is a state which occurs when the Anky is drinking
{
    private static AnkyDrinking _ankyInstance; // Thirst instance

    private float ThirstTimer;
    private float ThirstTick = 0.5f;
    private float ThirstRegeneration = 1.75f;   // Variables for Thirst

    private AnkyDrinking()
    {
        if (_ankyInstance != null)
            return;

        _ankyInstance = this;
    }

    public static AnkyDrinking AnkyInstance // Thirst instance
    {
        get
        {
            if (_ankyInstance == null)
                new AnkyDrinking();

            return _ankyInstance;
        }
    }

    public override void EnterState(MyAnky CurrentParentOf)   // Enters thirst state
    {
        CurrentParentOf.Animator.SetBool("isDrinking", true);
    }

    public override void UpdateState(MyAnky CurrentParentOf)   // Updates thirst state
    {
        if (CurrentParentOf.AnkyPredators.Count != 0)
            CurrentParentOf.State.ChangeState(AnkyAlerted.AnkyInstance);  // If predator is nearby then switch to alert state
        else
        {
            if (CurrentParentOf.Anky_Thirst > 100.0f)
            {
                CurrentParentOf.Anky_Thirst = 100.0f;
                CurrentParentOf.State.ChangeState(AnkyGrazing.AnkyInstance);  // If thirst level max then switch to grazing state
            }

            if (ThirstTimer > ThirstTick) 
            {
                ThirstTimer = 0.0f;
                CurrentParentOf.Anky_Thirst += ThirstRegeneration;  // As the Anky is drinking, a slow thirst regeneration will begin
            }
        }

        ThirstTimer = ThirstTimer + Time.deltaTime;
    }

    public override void ExitState(MyAnky CurrentParentOf)   // Exits drinking state
    {
        CurrentParentOf.Animator.SetBool("isDrinking", false);
    }
}