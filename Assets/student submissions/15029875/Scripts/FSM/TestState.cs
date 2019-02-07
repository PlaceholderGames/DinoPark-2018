using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : StateBase
{
    public override void BeginS(StateMachine SM)
    {
        throw new System.NotImplementedException();
    }

    public override void EndS(StateMachine SM)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateS(StateMachine SM)
    {
        Debug.Log("UPDATING");
    }
}
