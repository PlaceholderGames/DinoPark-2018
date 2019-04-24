﻿using UnityEngine;
using FiniteStateMachine;

public class AI_Idle : State<AI>
{
    private static AI_Idle _instance;

    private AI_Idle()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AI_Idle Instance
    {
        get
        {
            if (_instance == null)
            {
                new AI_Idle();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering Idle State");
        _owner.animator.Play("Idle");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Idle State");
    }

    public override void UpdateState(AI _owner)
    {
        if (_owner.switchState)
        {
            _owner.stateMachine.ChangeState(AI_Wander.Instance);
        }
    }
}