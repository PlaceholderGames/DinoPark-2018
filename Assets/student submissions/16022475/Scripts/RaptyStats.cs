using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyStats : MonoBehaviour
{
    public float age = 0;
    bool adult = true;
    //Energy Stats
    protected float energyMax;
    protected float energy;
    float energyModifier = 1;
    //Health Stats
    protected float healthMax;
    protected float health;
    float healthModifier = 1;
    //Speed Stats
    float speedMax;
    protected float speed;
    float speedModifier = 1;
    //Acceleration Stats
    float accelMax;
    protected float accel;
    float accelModifier = 1;
    //Rotation Stats
    float rotMax;
    protected float rot;
    float rotModifier = 1;
    //Angular Rotation Stats
    float angularAccelMax;
    protected float angularAccel;
    float angularAccelModifier = 1;

    protected float hunger;
    protected float thirst;
    protected float hungerMax;
    protected float thirstMax;

    
    
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

            hungerMax = energyMax / 2;
            thirstMax = energyMax / 2;
            hunger = hungerMax;
            thirst = thirstMax;

            
        }
        
    }
        // Update is called once per frame
        void Update()
    {
        age += Time.deltaTime;
      
    }
}
