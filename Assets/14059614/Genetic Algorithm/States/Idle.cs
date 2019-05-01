using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class Idle : State<RaptyAI>
{

    private static Idle instance;
    private Idle()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Idle Instance
    {
        get
        {
            if (instance == null)
            {
                new Idle();
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