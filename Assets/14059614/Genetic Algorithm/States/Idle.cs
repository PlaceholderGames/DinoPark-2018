using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class Idle : State<RaptyAI>
{

    int move;
    Vector3 randTarget;
    float moveDuration;
    private static Idle instance;
    private Idle()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Idle Instance
    {
        get
        {
            if (instance == null)
            {
                new Idle();
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
        RandomMove(owner);
        moveDuration += Time.deltaTime;
        if (moveDuration > 5) moveDuration = 0;
    }
    void RandomMove(RaptyAI owner)
    {
        move = UnityEngine.Random.Range(0, 3);
        while (moveDuration < 5)
        {
            if (move == 1)
            {
                if (randTarget == owner.transform.position)
                {
                    randTarget = new Vector3(0, 0, 0);
                }
                else if (randTarget == new Vector3(0, 0, 0))
                {
                    randTarget = new Vector3(UnityEngine.Random.Range(0, 10), 0, UnityEngine.Random.Range(0, 10));
                }
                owner.transform.position = Vector3.MoveTowards(owner.transform.position, randTarget, owner.Speed * Time.deltaTime);
                owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.position, randTarget - owner.transform.position, owner.Speed * 2 * Time.deltaTime, 0.0f));
            }
        }

    }


}