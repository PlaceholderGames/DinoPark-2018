using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyStateMachine : RaptyStats
{
    public float hungerDisplay;
    public float thirstDisplay;
    // Start is called before the first frame update
    void Start()
    {
       
       InvokeRepeating("loop", 2.0f, 1.0f);
    }
    
    // Update is called once per frame
    void Update()
    {

        hungerDisplay = hunger;
        thirstDisplay = thirst;
    }

    void loop()
    {
        hunger -= 0.5f;
        thirst -= 1f;
        Wander();
        energyMax = hunger + thirst;
    }

    void Wander()
    {
        if (hunger < (hunger/2))
        {
            Hungry(); 
        }
        else if (thirst < (thirst/2))
        {
            Thirsty();
        }
        else if (health < healthMax)
        {
            Injured();
        }
    }

    void Hungry()
    {
        Grouping();
        
    }

    void Thirsty()
    {
        Searching();
    }

    void Hunting()
    {
        //search for some anky to attack or scavenge
        Attack();
    }

    void Grouping()
    {
        Hunting();
    }

    void Attack()
    {        //if in range of a target attack, if health is below a certain amount,retreat
        if (health <= healthMax/2)
        {
            Injured();
        }
    }

    void Eating()
    {
        hunger++;
    }

    void Injured()
    {
        //move somewhere away from other creatures and rest
        Resting();
    }

    void Resting()
    {
        if (health == healthMax)
        {
            Wander();
        }
        health++;
        energy--;
    }

    void Searching()
    {
        //look for water and when found go to it, and when reached go to drinking
        Drinking();
    }

    void Drinking()
    {
        while (thirst < thirstMax)
        {
            thirst++;
        }
    }

    void Breed()
    {
        //when energy is high and the place is safe, choose 1 of the other rapty to breed
        if (energy > (energyMax * 0.9f) && health == healthMax)
        {
            //using a list or array of the nearby members, grab the MAX values of the parents and combine them
        }
    }
}
