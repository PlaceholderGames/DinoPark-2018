using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class Sleep : State<RaptyAI>
{
    private static Sleep instance;
    private Sleep()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Sleep Instance
    {
        get
        {
            if (instance == null)
            {
                new Sleep();
            }
            return instance;
        }
    }
    public override void EnterState(RaptyAI owner)
    {

    }
    public override void ExitState(RaptyAI owner)
    {

    }
    public override void UpdateState(RaptyAI owner)
    {

    }

}