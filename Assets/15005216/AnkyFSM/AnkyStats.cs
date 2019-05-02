using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyStats : MonoBehaviour
{

    public int health = 200;
    public float thirst = 100;
    public float hunger = 100;

    public int AttackDamage = 20;
    public float AttackSpeed = 2.5f;
    
    float time = 0;
    // Update is called once per frame
    void Update ()
    {
        //Debug.Log(health);
        time += Time.deltaTime;
        if (time > 3)
        {
            thirst -= 2.5f;
            hunger -= 2.5f;
            time = 0;
        }
    }

    public float GetHunger()
    {
        return hunger;
    }
    public void SetHunger(float addition)
    {
        hunger += addition;
    }


    public float GetThirst()
    {
        return thirst;
    }
    public void SetThirst(float addition)
    {
        thirst += addition;
    }

    public int GetHealth()
    {
        return health;
    }
    public void SetHealth(int addition)
    {
        health += addition;
    }

    public float GetAttackSpeed()
    {
        return AttackSpeed;
    }

    public int GetAttackDamage()
    {
        return AttackDamage;
    }

}
