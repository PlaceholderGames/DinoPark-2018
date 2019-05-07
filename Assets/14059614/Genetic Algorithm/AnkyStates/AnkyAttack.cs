using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class AnkyAttack : State<MyAnky>
{
    bool eaten;
    private static AnkyAttack instance;
    private AnkyAttack()
    {
        if (instance != null) return;
        instance = this;
    }

    public static AnkyAttack Instance
    {
        get
        {
            if (instance == null)
            {
                new AnkyAttack();
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