using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class RaptyEating : State<MyRapty>   // This is a state for the Rapty eating
{
    private static RaptyEating _raptyInstance;   // Eating Instance

    private float HungerTime;
    private float HungerTick = 0.5f;
    private float HungerRegeneration = 3.75f;   // Local Hunger variables

    private RaptyEating()
    {
        if (_raptyInstance != null)
            return;

        _raptyInstance = this;
    }

    public static RaptyEating RaptyInstance   // Eating Instance
    {
        get
        {
            if (_raptyInstance == null)
                new RaptyEating();

            return _raptyInstance;
        }
    }

    public override void EnterState(MyRapty CurrentParentOf)   // Enters eating state
    {
        CurrentParentOf.Animator.SetBool("isEating", true);
    }

    public override void UpdateState(MyRapty CurrentParentOf)   // Updates eating state
    {

        if (CurrentParentOf.RetrieveNearestDeadAnky().gameObject.GetComponent<MyAnky>().Anky_Meat < 0.0f)   // If meat is less than 0 then set Anky to dead and swtich Rapty to hunt state
        {
            CurrentParentOf.RetrieveNearestDeadAnky().gameObject.GetComponent<MyAnky>().IsDead = true;
            CurrentParentOf.State.ChangeState(RaptyHunt.RaptyInstance);
        }
        else
        {
            if (HungerTime > HungerTick)
            {
                HungerTime = 0.0f;
                CurrentParentOf.RetrieveNearestDeadAnky().gameObject.GetComponent<MyRapty>().Rapty_MeatOnRapty = CurrentParentOf.RetrieveNearestDeadAnky().gameObject.GetComponent<MyRapty>().Rapty_MeatOnRapty - HungerRegeneration;   // When eating, Rapty's hunger level will slowly increase
                CurrentParentOf.Rapty_Hunger += HungerRegeneration;

                if (CurrentParentOf.Rapty_Hunger > 100.0f)   // If hunger level goes above 100 then set it back to 100
                    CurrentParentOf.Rapty_Hunger = 100.0f;
            }
        }

        HungerTime = HungerTime + Time.deltaTime;
    }

    public override void ExitState(MyRapty CurrentParentOf)  // Exits eating state
    {
        CurrentParentOf.Animator.SetBool("isEating", false);

    }
}