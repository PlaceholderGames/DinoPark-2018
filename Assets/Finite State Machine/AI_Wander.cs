using UnityEngine;
using FiniteStateMachine;

public class AI_Wander : State<AI>
{
    private static AI_Wander _instance;

    private AI_Wander()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AI_Wander Instance
    {
        get
        {
            if (_instance == null)
            {
                new AI_Wander();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering Wander State");
        _owner.animator.Play("Wander");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Wander State");
    }

    public override void UpdateState(AI _owner)
    {
        if (!_owner.switchState)
        {
            _owner.stateMachine.ChangeState(AI_Idle.Instance);
        }
    }
}