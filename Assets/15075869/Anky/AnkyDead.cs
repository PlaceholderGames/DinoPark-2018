using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//this class is so the anky can be set to dead
public class AnkyDead : State<MyAnky>
{
    private static AnkyDead anky_instance;

    private AnkyDead()
    {
        if (anky_instance != null)
            return;
        anky_instance = this;
    }

    public static AnkyDead AnkyInstance
    {
        get
        {
            if (anky_instance == null)
                new AnkyDead();
            return anky_instance;
        }
    }

    public override void OnStateEnter(MyAnky parent)
    {
        parent.anim.SetBool("isDead", true);
    }

    public override void OnStateExit(MyAnky parent)
    {
        parent.anim.SetBool("isDead", false);
    }

    //here the wander script is used
    //if the anky is not wandering then the anky is dead and
    //it will set it as dead
    public override void OnStateUpdate(MyAnky parent)
    {
        parent.wandering.enabled = false;
        if (parent.anky_dead)
            parent.Dead();
    }
}
