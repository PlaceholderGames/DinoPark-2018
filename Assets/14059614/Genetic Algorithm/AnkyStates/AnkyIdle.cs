using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class AnkyIdle : State<AnkylosaurusAI>
{

    int move;
    Vector3 randTarget;
    float moveDuration;
    private static AnkyIdle instance;
    private AnkyIdle()
    {
        if (instance != null) return;
        instance = this;
    }

    public static AnkyIdle Instance
    {
        get
        {
            if (instance == null)
            {
                new AnkyIdle();
            }
            return instance;
        }
    }
    public override void EnterState(AnkylosaurusAI owner)
    {
    }
    public override void ExitState(AnkylosaurusAI owner)
    {

    }
    public override void UpdateState(AnkylosaurusAI owner)
    {
        RandomMove(owner);
        moveDuration += Time.deltaTime;
        if (moveDuration > 5) moveDuration = 0;
    }
    void RandomMove(AnkylosaurusAI owner)
    {
        move = UnityEngine.Random.Range(0, 3);
        //move = 1;
        if (moveDuration < 5)
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
                owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.position, randTarget - owner.transform.position, owner.Speed * 5 * Time.deltaTime, 0.0f));
            }
        }

    }
}