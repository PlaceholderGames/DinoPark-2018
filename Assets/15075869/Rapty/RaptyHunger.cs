using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//this class is for when the raptor is hungry and
//needs to eat
public class RaptyHunger : State<MyRapty>
{
    private static RaptyHunger rapty_instance;

    private float counter = 1.0f;
    private float timer;
    private float health_up = 3.5f;

    //setting up raptor instance
    public static RaptyHunger RaptyInstance
    {
        get
        {
            if (rapty_instance == null)
                new RaptyHunger();
            return rapty_instance;
        }
    }

    private RaptyHunger()
    {
        if (rapty_instance != null)
            return;
        rapty_instance = this;
    }


    public override void OnStateEnter(MyRapty parent)
    {
        parent.anim.SetBool("isEating", true);
    }

    public override void OnStateExit(MyRapty parent)
    {
        parent.anim.SetBool("isEating", false);
    }

    public override void OnStateUpdate(MyRapty parent)
    {
        //if dinosaur meat is less than zero then set raptor to hunger state
        if (parent.getDinoMeat().gameObject.GetComponent<MyAnky>().anky_meat < 0.0f)
        {
            parent.getDinoMeat().gameObject.GetComponent<MyAnky>().anky_dead = true;
            parent.State.Change(RaptyHunting.RaptyInstance);
        }
        else
        {
            if (timer > counter)
            {
                timer = 0.0f;
                parent.getDinoMeat().gameObject.GetComponent<MyRapty>().rapty_meat = parent.getDinoMeat().gameObject.GetComponent<MyRapty>().rapty_meat - health_up;
                parent.rapty_hunger = parent.rapty_hunger + health_up;

                if (parent.rapty_hunger > 100.0f)
                    parent.rapty_hunger = 100.0f;
            }
        }
        timer = timer + Time.deltaTime;

    }

}
