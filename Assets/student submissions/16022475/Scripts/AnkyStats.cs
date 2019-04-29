using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyStats : MonoBehaviour
{
    bool adult = true;
    //Energy Stats
    float energyMax;
    float energy;
    float energyModifier = 1;
    //Health Stats
    float healthMax;
    float health;
    float healthModifier = 1;
    //Speed Stats
    float speedMax;
    float speed;
    float speedModifier = 1;
    //Acceleration Stats
    float accelMax;
    float accel;
    float accelModifier = 1;
    //Rotation Stats
    float rotMax;
    float rot;
    float rotModifier = 1;
    //Angular Rotation Stats
    float angularAccelMax;
    float angularAccel;
    float angularAccelModifier = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (adult == true)
        {
            energyModifier = Random.Range((0.9f * energyModifier), (1.1f * energyModifier));
            healthModifier = Random.Range((0.9f * healthModifier), (1.1f * healthModifier));
            speedModifier = Random.Range((0.9f * speedModifier), (1.1f * speedModifier));
            accelModifier = Random.Range((0.9f * accelModifier), (1.1f * accelModifier));
            rotModifier = Random.Range((0.9f * rotModifier), (1.1f * rotModifier));
            angularAccelModifier = Random.Range((0.9f * angularAccelModifier), (1.1f * angularAccelModifier));


            energyMax = (100 * energyModifier);
            healthMax = (100 * healthModifier);
            speedMax = (2.415f * speedModifier);
            accelMax = (1 * accelModifier);
            rotMax = (80 * rotModifier);
            angularAccelMax = (150 * angularAccelModifier);

            energy = energyMax;
            health = healthMax;
            speed = speedMax;
            accel = accelMax;
            rot = rotMax;
            angularAccel = angularAccelMax;
        }

        if (adult == false)
        {

            //will grab the parents stat modifiers and use them in place of their own
            energyModifier = Random.Range((0.9f * energyModifier), (1.1f * energyModifier));
            healthModifier = Random.Range((0.9f * healthModifier), (1.1f * healthModifier));
            speedModifier = Random.Range((0.9f * speedModifier), (1.1f * speedModifier));

            energyMax = (100 * energyModifier) / 2;
            healthMax = (100 * healthModifier) / 2;
            speedMax = (100 * speedModifier) / 2;

            energy = energyMax;
            health = healthMax;
            speed = speedMax;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
