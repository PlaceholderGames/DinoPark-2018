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
public class FSMRoaming : FSMState
{
    /// <summary>
    /// 
    /// </summary>
    Quaternion targetDir;

    public FSMRoaming(FSMCommon nCOM) : base(nCOM) { com = nCOM; }

    public override void Start()
    {
        if (com.debugging) Debug.Log(com.name + ": entered Roaming State");
        base.Start();

        //Randomise roaming direction
        float angle = Random.Range(0, 360);
        if (com.debugging) Debug.Log(com.name + ": roam towards angle " + angle);

        //Calculate relative direction to use 
        targetDir = Quaternion.Euler(com.parent.transform.rotation.eulerAngles) * Quaternion.Euler(0, angle, 0);
        if (com.debugging) Debug.Log(com.name + ": rotate to " + targetDir.eulerAngles);
    }

    public override void Update()
    {
        base.Update();

        //Move Entity in the target direction
        com.parent.MoveAtDir(targetDir);
    }

    public override void Transition()
    {
        base.Transition();
    }
}