using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using FSM;
public class Eat : State<MyRapty>
{
    bool eaten;
    private static Eat instance;
    private Eat()
    {
        if (instance != null) return;
        instance = this;
    }

    public static Eat Instance
    {
        get
        {
            if (instance == null)
            {
                new Eat();
            }
            return instance;
        }
    }
    public override void EnterState(MyRapty owner)
    {

    }
    public override void ExitState(MyRapty owner)
    {

    }
    public override void UpdateState(MyRapty owner)
    {

    }
    /*
    void Eat(RaptyAI owner)
    {
        if (Vector3.Distance(owner.transform.position, owner.target.transform.position) < 5.0f)
        {
            owner.Weight += 10;
            owner.Hunger -= 20;
        }
    }
    */
}