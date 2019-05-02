using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//this is the class for the anky attack
public class AnkyAttack : State<MyAnky>
{
    private static AnkyAttack anky_instance;
    
    public static AnkyAttack AnkyInstance
    {
        get
        {
            if (anky_instance == null)
                new AnkyAttack();
            return anky_instance;
        }
    }

    private AnkyAttack()
    {
        if (anky_instance != null)
            return;
        anky_instance = this;
    }
    // Use this for initialization
    public override void OnStateEnter(MyAnky parent)
    {
        parent.anim.SetBool("isAttacking", true);
    }

    public override void OnStateUpdate(MyAnky parent)
    {
        //if ankys health is less than zero then set anky to dead
        if (parent.anky_health < 0.0f)
        {
            parent.faceTowards.target = null;
            parent.faceTowards.enabled = false;
            parent.State.Change(AnkyDead.AnkyInstance);
        }

        //if enemy count is equal to zero then targets are null
        //set anky to alert
        if (parent.Enemies.Count == 0)
        {
            parent.faceTowards.enabled = false;
            parent.faceTowards.target = null;
            //parent.State.Change(AnkyIsAlert.AnkyInstance);
        }

        //if face towards isnt enabled then face towards enemy and get enemy 
        //game object
        if (!parent.faceTowards.enabled)
        {
            parent.faceTowards.enabled = true;
            parent.faceTowards.target = parent.getEnemies().gameObject;
        }


    }

    public override void OnStateExit(MyAnky parent)
    {
        parent.anim.SetBool("isAttacking", false);
    }
}
