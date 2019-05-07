using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class Alert : State<MyRapty>
{
    bool eaten;
    private static Alert instance;
    private Alert()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Alert Instance
    {
        get
        {
            if (instance == null)
            {
                new Alert();
            }
            return instance;
        }
    }
    public override void EnterState(MyRapty owner)
    {
        owner.animator.SetBool("isAlert", true);
    }
    public override void ExitState(MyRapty owner)
    {
        owner.animator.SetBool("isAlert", false);
    }
    public override void UpdateState(MyRapty owner)
    {

    }

}