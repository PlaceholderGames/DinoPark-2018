using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//This class is for when the raptor is alerted of
//nearby ankys
public class RaptyIsAlert : State<MyRapty>
{
    private static RaptyIsAlert rapty_instance;

    public static RaptyIsAlert RaptyInstance
    {
        get
        {
            if (rapty_instance == null)
                new RaptyIsAlert();
            return rapty_instance;
            
        }
    }

    private RaptyIsAlert()
    {
        if (rapty_instance != null)
            return;
        rapty_instance = this;
    }

    public override void OnStateEnter(MyRapty parent)
    {
        parent.anim.SetBool("isAlerted", true);
    }

    public override void OnStateExit(MyRapty parent)
    {
        parent.anim.SetBool("isAlerted", false);
    }

    //if the food count is at zero then the raptor will go into
    //hunting mode
    //if the raptors health is more than or equal to 25 then it will attack
    //but if its less then the raptor will run away
    public override void OnStateUpdate(MyRapty parent)
    {
        if (parent.Food.Count == 0)
        {
            parent.State.Change(RaptyHunting.RaptyInstance);
        }
        else
        {
            if (parent.rapty_health >= 25.0f)
                parent.State.Change(RaptyAttack.RaptyInstance);
            else
               parent.State.Change(RaptyRunAway.RaptyInstance);
        }
    }
}
