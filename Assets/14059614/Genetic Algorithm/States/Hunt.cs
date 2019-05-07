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
        AnkylosaurusAI[] anky = UnityEngine.Object.FindObjectsOfType<AnkylosaurusAI>();
        AnkylosaurusAI ankyTarget = anky[0];

        if (anky.Length >= 1)
        {
            for (int i = 1; i < anky.Length; i++)
            {
                if (Vector3.Distance(anky[i].transform.position, owner.transform.position) < Vector3.Distance(ankyTarget.transform.position, owner.transform.position))
                {
                    ankyTarget = anky[i];
                }
            }
            if (owner.transform.position != ankyTarget.transform.position)
            {
                owner.transform.position = Vector3.MoveTowards(owner.transform.position, ankyTarget.transform.position, owner.Speed * Time.deltaTime);
                owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, ankyTarget.transform.position - owner.transform.position, (owner.Speed * 2) * Time.deltaTime, 0.0f));
            }
        }
        if (ankyTarget != null)
        {
            if (Vector3.Distance(owner.transform.position, ankyTarget.transform.position) < 1f)
            {
                //if next to anky, kill anky
                GameObject.Destroy(ankyTarget.gameObject);
            }
        }
    

        if (Vector3.Distance(owner.transform.position, owner.ankylosaurus.transform.position) - Vector3.Distance(owner.transform.position, owner.player.transform.position) < 0)
        {
            //closer to anky, attack anky

        }
    }
}