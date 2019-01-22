/*
    
    Template State

    Action:
        Summarise overall action

    For:
        What Entity this State is suitable for

    Starts from:
        The predecessor State(s)

    Exits into:
        The successor State(s)

*/

using UnityEngine;

/// <summary>
/// Summarise overall action.
/// </summary>
public class FSMTemplateState : FSMState
{
    public FSMTemplateState(FSMCommon nCOM) : base(nCOM) { com = nCOM; }

    public override void Start()
    {
        if (com.debugging) Debug.Log(com.name + ": entered Template State");
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Transition()
    {
        base.Transition();
    }
}