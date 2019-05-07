using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class AnkyAlert : State<MyAnky>
{
    bool eaten;
    private static AnkyAlert instance;
    private AnkyAlert()
    {
        if (instance != null) return;
        instance = this;
    }

    public static AnkyAlert Instance
    {
        get
        {
            if (instance == null)
            {
                new AnkyAlert();
            }
            return instance;
        }
    }
    public override void EnterState(MyAnky owner)
    {
        owner.animator.SetBool("isAlert", true);
    }
    public override void ExitState(MyAnky owner)
    {
        owner.animator.SetBool("isAlert", false);
    }
    public override void UpdateState(MyAnky owner)
    {

    }

}