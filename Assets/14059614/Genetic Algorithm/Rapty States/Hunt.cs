using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;

public class Hunt : State<RaptyAI>
{
    private static Hunt instance;
    private Hunt()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Hunt Instance
    {
        get
        {
            if (instance == null)
            {
                new Hunt();
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
    void Attack(RaptyAI owner)
    {
        //AnkylosaurusAI[] ankies = UnityEngine.Object.FindObjectOfType<AnkylosaurusAI>();
        if (Vector3.Distance(owner.transform.position, owner.ankylosaurus.transform.position) - Vector3.Distance(owner.transform.position, owner.player.transform.position) < 0)
        {
            //closer to anky, attack anky

        }
    }
}