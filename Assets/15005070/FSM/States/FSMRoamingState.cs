/*
    
    Roaming State

    Action:
        Instructs Entity to walk around randomly

    For:
        All

    Starts from:
        Any (temp)

    Exits into:
        Any (temp)

*/

using UnityEngine;

/// <summary>
/// Instructs Entity to walk around randomly.
/// </summary>
public class FSMRoamingState : FSMState
{
    /// <summary>
    /// 
    /// </summary>
    Vector3 targetDest;

    public FSMRoamingState(FSMCommon nCOM) : base(nCOM) { com = nCOM; }

    public override void Start()
    {
        if (com.debugging) Debug.Log(com.name + ": entered Roaming State");
        base.Start();

        //Randomise roaming destination
        Vector3 cur = com.parent.transform.position;
        Vector3 dest = new Vector3(cur.x + Random.Range(-1000, 1000), 0, cur.z + Random.Range(-1000, 1000));
        if (com.debugging) Debug.Log(com.name + ": roam towards " + dest);
    }

    public override void Update()
    {
        base.Update();

        //Move Entity in the target direction
        com.parent.MoveToPos(targetDest, 10f, 4.8f);
    }

    public override void Transition()
    {
        base.Transition();
    }
}