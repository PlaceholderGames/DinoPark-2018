using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class AnkyGraze : State<MyAnky>
{
    Vector3 randTarget;
    private static AnkyGraze instance;
    private AnkyGraze()
    {
        if (instance != null) return;
        instance = this;
    }

    public static AnkyGraze Instance
    {
        get
        {
            if (instance == null)
            {
                new AnkyGraze();
            }
            return instance;
        }
    }
    public override void EnterState(MyAnky owner)
    {
        owner.animator.SetBool("isGrazing", true);
    }
    public override void ExitState(MyAnky owner)
    {
        owner.animator.SetBool("isGrazing", false);
    }
    public override void UpdateState(MyAnky owner)
    {
        owner.animator.SetBool("isAlert", true);
        RandomMove(owner);

    }
    void RandomMove(MyAnky owner)
    {
        if (randTarget == owner.transform.position)
        {
            randTarget = new Vector3(0, 0, 0);
        }
        else if (randTarget == new Vector3(0, 0, 0))
        {
            randTarget = new Vector3(UnityEngine.Random.Range(0, 2000), 0, UnityEngine.Random.Range(0, 2000));
        }
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, randTarget, owner.Speed * Time.deltaTime);
        owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.position, randTarget - owner.transform.position, owner.Speed * 5 * Time.deltaTime, 0.0f));
    }

}