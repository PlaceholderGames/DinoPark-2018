using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//this is the class for the raptor attack
public class RaptyAttack : State<MyRapty>
{
    private static RaptyAttack rapty_instance;

    private RaptyAttack()
    {
        if (rapty_instance != null)
            return;

        rapty_instance = this;
    }

    public static RaptyAttack RaptyInstance
    {
        get
        {
            if (rapty_instance == null)
                new RaptyAttack();
            return rapty_instance;
        }
    }

	// Use this for initialization
	public override void OnStateEnter (MyRapty parent)
    {
        parent.anim.SetBool("isAttacking", true);
	}

    // Update is called once per frame
    public override void OnStateUpdate(MyRapty parent)
    {
        if (!parent.seeking.enabled)
        {
            //seek for food
            parent.seeking.target = parent.getFood().gameObject;
            parent.seeking.enabled = true;
        }
        else
        {
            //if anky gameobject is not dead
            if (!(parent.seeking.target.gameObject.GetComponent<MyAnky>().State.current_state is AnkyDead))
            {
                //if raptys health is less than zero then set rapty to dead
                if (parent.rapty_health < 0.0f)
                {
                    parent.seeking.enabled = false;
                    parent.seeking.target = null;
                    parent.State.Change(RaptyDead.RaptyInstance);
                }
                //if its less than 15 then set rapty to run away
                else if (parent.rapty_health < 15.0f)
                {
                    parent.seeking.enabled = false;
                    parent.seeking.target = null;
                    parent.State.Change(RaptyRunAway.RaptyInstance);
                }
            }
            //otherwise set rapty to hunt
            else
            {
                parent.seeking.enabled = false;
                parent.seeking.target = null;
                parent.State.Change(RaptyHunting.RaptyInstance);
            }
        }
    }

    public override void OnStateExit(MyRapty parent)
    {
        parent.anim.SetBool("isAttacking", false);
    }
}
