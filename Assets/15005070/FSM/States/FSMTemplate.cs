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
/// Brief what this State represents.
/// </summary>
public class FSMTemplate : FSMState
{
    public FSMTemplate(FSMCommon nCOM) : base(nCOM) { com = nCOM; }
    
    /// <summary>
    /// Brief what these tasks are.
    /// </summary>
    public override void Start()
    {
        if (com.debugging) Debug.Log(com.name + ": entered Template State");
        base.Start();
    }

    /// <summary>
    /// Brief what these tasks are.
    /// </summary>
    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Brief what these tasks are.
    /// </summary>
    public override void Transition()
    {
        base.Transition();
    }
}