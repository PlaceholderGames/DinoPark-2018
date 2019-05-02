using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class Hunt : State<RaptyAI>
{
    private static Hunt instance;
    private Hunt()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Hunt Instance
    {
        get
        {
            if (instance == null)
            {
                new Hunt();
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