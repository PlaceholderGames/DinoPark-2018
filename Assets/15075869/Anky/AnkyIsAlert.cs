using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//This class is for when the raptor is alerted of
//nearby raptors
public class AnkyIsAlert : State<MyAnky>
{
    private static AnkyIsAlert anky_instance;

    public static AnkyIsAlert AnkyInstance
    {
        get
        {
            if (anky_instance == null)
                new AnkyIsAlert();
            return anky_instance;
        }
    }

    private AnkyIsAlert()
    {
        if (anky_instance != null)
            return;
        anky_instance = this;
    }

    public override void OnStateEnter(MyAnky parent)
    {
        parent.anim.SetBool("isAlerted", true);
    }

    public override void OnStateExit(MyAnky parent)
    {
        parent.anim.SetBool("isAlerted", false);
    }

    //if the enemy (raptor) count is at zero then the ankys will
    //countinue to eat grass
    //if there is more than one raptor around then the anky
    //will go into the run away state
    public override void OnStateUpdate(MyAnky parent)
    {
        if (parent.Enemies.Count == 0)
        {
            //parent.State.Change(AnkyGrass.AnkyInstance);
        }
        if (parent.Enemies.Count > 1)
            parent.State.Change(AnkyRunAway.AnkyInstance);
    }
}

