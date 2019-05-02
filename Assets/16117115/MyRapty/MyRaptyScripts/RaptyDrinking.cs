using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class RaptyDrinking : State<MyRapty>   // This is a state for the Rapty drinking
{
    private static RaptyDrinking _raptyInstance;   // Drink Instance

    private float ThirstTime;
    private float ThirstTick = 0.5f;
    private float ThirstRegeneration = 1.75f;    // Local variables for thirst characteristics

    private RaptyDrinking()
    {
        if (_raptyInstance != null)
            return;

        _raptyInstance = this;
    }

    public static RaptyDrinking RaptyInstance   // Drink Instance
    {
        get
        {
            if (_raptyInstance == null)
                new RaptyDrinking();

            return _raptyInstance;
        }
    }

    public override void EnterState(MyRapty CurrentParentOf)  // Enters drinking state
    {
        CurrentParentOf.Animator.SetBool("isDrinking", true);
    }

    public override void UpdateState(MyRapty CurrentParentOf)   // Updates drinking state
    {
        if (CurrentParentOf.Rapty_Thirst > 100.0f)
        {
            CurrentParentOf.Rapty_Thirst = 100.0f;
            CurrentParentOf.State.ChangeState(RaptyHunt.RaptyInstance);   // If thirst level is greater than 100 then switch to Rapty hunt state
        }
        else
        {
            if (ThirstTime > ThirstTick)
            {
                ThirstTime = 0.0f;

                CurrentParentOf.Rapty_Thirst = CurrentParentOf.Rapty_Thirst + ThirstRegeneration;   // When drinking, regenerate thirst level
            }
        }

        ThirstTime = ThirstTime + Time.deltaTime;
    }

    public override void ExitState(MyRapty CurrentParentOf)    // Exits drinking state
    {
        CurrentParentOf.Animator.SetBool("isDrinking", false);
    }
}
