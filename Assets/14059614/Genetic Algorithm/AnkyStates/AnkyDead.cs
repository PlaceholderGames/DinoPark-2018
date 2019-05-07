using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class AnkyDead : State<MyAnky>
{
    bool eaten;
    private static AnkyDead instance;
    private AnkyDead()
    {
        if (instance != null) return;
        instance = this;
    }

    public static AnkyDead Instance
    {
        get
        {
            if (instance == null)
            {
                new AnkyDead();
            }
            return instance;
        }
    }
    public override void EnterState(MyAnky owner)
    {
        Debug.Log("Entering Dead state");
        owner.animator.SetBool("isDead", true);
    }
    public override void ExitState(MyAnky owner)
    {
        owner.animator.SetBool("isDead", false);
    }
    public override void UpdateState(MyAnky owner)
    {
        GameObject.Destroy(owner.gameObject);
    }

}