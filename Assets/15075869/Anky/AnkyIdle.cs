using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//This class is for when the Anky is in idle state
public class AnkyIdle : State<MyAnky>
{
    private static AnkyIdle anky_instance;


    public static AnkyIdle AnkyInstance
    {
        get
        {
            if (anky_instance == null)
                new AnkyIdle();
            return anky_instance;
        }
    }

    private AnkyIdle()
    {
        if (anky_instance != null)
            return;
        anky_instance = this;
    }

    public override void OnStateEnter(MyAnky parent)
    {
        parent.anim.SetBool("isIdle", true);
    }

    public override void OnStateExit(MyAnky parent)
    {
        parent.anim.SetBool("isIdle", false);
    }

    //Once anky has finished in idle state he will enter the
    //hunting state
    public override void OnStateUpdate(MyAnky parent)
    {
        //parent.State.Change(AnkyGrass.RaptyInstance);
    }
}
