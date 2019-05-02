using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//This class is for when the raptor is thirsty
public class RaptyThirsty : State<MyRapty>
{
    private static RaptyThirsty rapty_instance;

    private float timer;
    private float counter = 0.5f;
    private float health_up = 2.0f;

    //raptor thirst instance
    private RaptyThirsty()
    {
        if (rapty_instance != null)
            return;

        rapty_instance = this;
    }

    public static RaptyThirsty RaptyInstance
    {
        get
        {
            if (rapty_instance == null)
                new RaptyThirsty();
            return rapty_instance;
        }
    }

    public override void OnStateEnter(MyRapty parent)
    {
        parent.anim.SetBool("isDrinking", true);
    }

    public override void OnStateExit(MyRapty parent)
    {
        parent.anim.SetBool("isDrinking", false);
    }

    // Update is called once per frame
    public override void OnStateUpdate(MyRapty parent)
    {
        //if the raptors water is greater than 100 then set the raptors
        //water back to 100 and change its state to hunting
        if (parent.rapty_water > 100.0f)
        {
            parent.rapty_water = 100.0f;
            parent.State.Change(RaptyHunting.RaptyInstance);
        }
        //otherwise if the timer is greater than the counter then add
        //health to the raptor
        else   
        {
            if (timer > counter)
            {
                timer = 0.0f;
                parent.rapty_water = parent.rapty_water + health_up;
            }
        }
        timer = timer + Time.deltaTime;
    }
}

