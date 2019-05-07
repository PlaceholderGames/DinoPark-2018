using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class AnkyEat : State<AnkylosaurusAI>
{
    bool eaten;
    private static AnkyEat instance;
    private AnkyEat()
    {
        if (instance != null) return;
        instance = this;
    }

    public static AnkyEat Instance
    {
        get
        {
            if (instance == null)
            {
                new AnkyEat();
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

    }
    void Eat(AnkylosaurusAI owner)
    {
        if (Vector3.Distance(owner.transform.position, owner.target.transform.position) < 5.0f)
        {
            owner.Weight += 10;
            owner.Hunger -= 20;
        }
    }

}