using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//This class is for when the Anky is in idle state
public class RaptyIdle : State<MyRapty>
{
    private static RaptyIdle rapty_instance;

    private RaptyIdle()
    {
        if (rapty_instance != null)
            return;
        rapty_instance = this;
    }

    public static RaptyIdle RaptyInstance
    {
        get
        {
            if (rapty_instance == null)
                new RaptyIdle();
            return rapty_instance;
        }
    }

    public override void OnStateEnter(MyRapty parent)
    {
        parent.anim.SetBool("isIdle", true);
    }

    public override void OnStateExit(MyRapty parent)
    {
        parent.anim.SetBool("isIdle", false);
    }

    //Once anky has finished in idle state he will enter the
    //hunting state
    public override void OnStateUpdate (MyRapty parent)
    {
        parent.State.Change(RaptyHunting.RaptyInstance);
    }
}
