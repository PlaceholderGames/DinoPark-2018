using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class rapAttack : State<MyRapty>
{
    bool eaten;
    private static rapAttack instance;
    private rapAttack()
    {
        if (instance != null) return;
        instance = this;
    }

    public static rapAttack Instance
    {
        get
        {
            if (instance == null)
            {
                new rapAttack();
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