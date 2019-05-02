using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//this state is for when the raptor needs to run away from
//an attack due to low health
public class RaptyRunAway : State<MyRapty>
{
    private static RaptyRunAway rapty_instance;

    public static RaptyRunAway RaptyInstance
    {
        get
        {
            if (rapty_instance == null)
                new RaptyRunAway();
            return rapty_instance;
        }
    }

    private RaptyRunAway()
    {
        if (rapty_instance != null)
            return;
        rapty_instance = this;
    }


    public override void OnStateEnter(MyRapty parent)
    {
        parent.anim.SetBool("isFleeing", true);
    }

    public override void OnStateExit(MyRapty parent)
    {
        parent.anim.SetBool("isFleeing", false);
    }

    public override void OnStateUpdate(MyRapty parent)
    {
        //if the rapty health is less than zero it will change its
        //state to dead
        if (parent.rapty_health < 0.0f)
            parent.State.Change(RaptyDead.RaptyInstance);
        else
        {
            if (!parent.runAway.enabled)
            {
                parent.runAway.target = parent.getFood().gameObject;
                parent.runAway.enabled = true;
            }
            //the raptor will transform position and run away to distance set
            else if (Vector3.Distance(parent.transform.position, parent.runAway.target.transform.position) > 250.0f)
            {
                //stopping the raptor running away and
                //changing the state to hunting
                parent.runAway.enabled = false;
                parent.runAway.target = null;
                parent.State.Change(RaptyHunting.RaptyInstance);
            }
        }
    }
}
