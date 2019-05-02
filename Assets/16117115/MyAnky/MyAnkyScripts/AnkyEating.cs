using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class AnkyEating : State<MyAnky> // This state is for Anky eating which will be determined by a few variables such as 
{
    private static AnkyEating _ankyInstance; // Instance for Anky eating

    private float HungerTimer;
    private float HungerTicks = 0.5f;         // Floats for hunger timer and levels
    private float HungerRegeneration = 1.5f;

    private AnkyEating()
    {
        if (_ankyInstance != null)
            return;

        _ankyInstance = this;
    }

    public static AnkyEating AnkyInstance // Instance for Anky eating
    {
        get
        {
            if (_ankyInstance == null)
                new AnkyEating();

            return _ankyInstance;
        }
    }

    public override void EnterState(MyAnky CurrentParentOf) // Enters the eating state
    {
        CurrentParentOf.Animator.SetBool("isEating", true);
    }

    public override void UpdateState(MyAnky CurrentParentOf)  // Updates the eating state depending on parameters
    {
        if (CurrentParentOf.AnkyPredators.Count != 0)
            CurrentParentOf.State.ChangeState(AnkyAlerted.AnkyInstance); // If predator is near change to Alert
        else
        {
            if (CurrentParentOf.Anky_Hunger > 100.0f)
            {
                CurrentParentOf.Anky_Hunger = 100.0f;
                CurrentParentOf.State.ChangeState(AnkyGrazing.AnkyInstance);  // If Anky hunger is max then change to Graze
            }

            if (HungerTimer > HungerTicks)
            {
                HungerTimer = 0.0f;
                CurrentParentOf.Anky_Hunger = CurrentParentOf.Anky_Hunger + HungerRegeneration;   // When eating a slow hunger regeneration will begin
            }
        }

        HungerTimer = HungerTimer + Time.deltaTime;
    }

    public override void ExitState(MyAnky CurrentParentOf)   // Exits eating state
    {
        CurrentParentOf.Animator.SetBool("isEating", false);
    }
}
