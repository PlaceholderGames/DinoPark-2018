using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class Dead : State<MyRapty>
{
    bool eaten;
    private static Dead instance;
    private Dead()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Dead Instance
    {
        get
        {
            if (instance == null)
            {
                new Dead();
            }
            return instance;
        }
    }
    public override void EnterState(MyRapty owner)
    {
        Debug.Log("Entering Dead state");
        owner.animator.SetBool("isDead", true);
    }
    public override void ExitState(MyRapty owner)
    {
        owner.animator.SetBool("isDead", false);
    }
    public override void UpdateState(MyRapty owner)
    {
        GameObject.Destroy(owner.gameObject);
    }

}