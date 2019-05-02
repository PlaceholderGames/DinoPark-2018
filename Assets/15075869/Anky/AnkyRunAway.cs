using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//this state is for when the anky needs to run away from
//an attack due to low health
public class AnkyRunAway : State<MyAnky>
{
    private static AnkyRunAway anky_instance;

    public static AnkyRunAway AnkyInstance
    {
        get
        {
            if (anky_instance == null)
                new AnkyRunAway();
            return anky_instance;
        }
    }

    private AnkyRunAway()
    {
        if (anky_instance != null)
            return;
        anky_instance = this;
    }


    public override void OnStateEnter(MyAnky parent)
    {
        parent.anim.SetBool("isFleeing", true);
    }

    public override void OnStateExit(MyAnky parent)
    {
        parent.anim.SetBool("isFleeing", false);
    }

    public override void OnStateUpdate(MyAnky parent)
    {
        //if the anky health is less than zero it will change its
        //state to dead
        if (parent.anky_health < 0.0f)
            parent.State.Change(AnkyDead.AnkyInstance);
        else
        {
            if (!parent.runAway.enabled)
            {
                parent.runAway.target = parent.getEnemies().gameObject;
                parent.runAway.enabled = true;
            }
            //the anky will transform position and run away to distance set
            else if (Vector3.Distance(parent.transform.position, parent.runAway.target.transform.position) > 100.0f)
            {
                parent.runAway.enabled = false;
                parent.runAway.target = null;
                //parent.State.Change(AnkyGrass.AnkyInstance);
            }
        }
    }
}
