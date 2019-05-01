using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class Food : State<RaptyAI>
{
    private static Food instance;
    private Food()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Food Instance
    {
        get
        {
            if (instance == null)
            {
                new Food();
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