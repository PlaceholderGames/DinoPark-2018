using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class Breed : State<RaptyAI>
{
    private static Breed instance;
    private Breed()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Breed Instance
    {
        get
        {
            if (instance == null)
            {
                new Breed();
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