using UnityEngine;
using FiniteStateMachine;

public class AI_Wander : State<AI>
{
    private static AI_Wander _instance;
    Vector3 waypoint;

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
        GenerateWaypoint(_owner);
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
        Wander(_owner);
    }

    void GenerateWaypoint(AI _owner)
    {
        waypoint = Random.insideUnitSphere * 50 + _owner.transform.position;
        waypoint.y = _owner.transform.position.y;
        Debug.Log(_owner.transform.position + "   " + waypoint);
    }

    void Wander(AI _owner)
    {
        var direction = waypoint - _owner.transform.position;
        _owner.transform.rotation = Quaternion.Slerp(_owner.transform.rotation,
                                    Quaternion.LookRotation(direction),
                                    5.0f * Time.deltaTime);
        _owner.transform.Translate(0, 0, Time.deltaTime * 5.0f);
    }


}