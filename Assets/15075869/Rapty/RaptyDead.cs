using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//this class is so the raptor can be set to dead
public class RaptyDead : State<MyRapty>
{
    private static RaptyDead rapty_instance;

    private RaptyDead()
    {
        if (rapty_instance != null)
            return;
        rapty_instance = this;
    }

    public static RaptyDead RaptyInstance
    {
        get
        {
            if (rapty_instance == null)
                new RaptyDead();
            return rapty_instance;
        }
    }

    public override void OnStateEnter(MyRapty parent)
    {
        parent.anim.SetBool("isDead", true);
    }

    public override void OnStateExit(MyRapty parent)
    {
        parent.anim.SetBool("isDead", false);
    }

    //here the wander script is used
    //if the raptor is not wandering then the raptor is dead and
    //it will set it as dead
    public override void OnStateUpdate(MyRapty parent)
    {
        parent.wandering.enabled = false;
        if (parent.rapty_dead)
            parent.Dead();
    }
}
