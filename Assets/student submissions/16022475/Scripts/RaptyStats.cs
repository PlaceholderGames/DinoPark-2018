using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyStats : MonoBehaviour
{
    public float age = 0;
    bool adult = true;
    //Energy Stats
    public float energyMax;
    public float energy;
    float energyModifier = 1;
    //Health Stats
    public float healthMax;
    public float health;
    float healthModifier = 1;
    //Speed Stats
    float speedMax;
    public float speed;
    float speedModifier = 1;
    //Acceleration Stats
    float accelMax;
    protected float accel;
    float accelModifier = 1;
    //Rotation Stats
    float rotMax;
    public float rot;
    float rotModifier = 1;
    //Angular Rotation Stats
    float angularAccelMax;
    protected float angularAccel;
    float angularAccelModifier = 1;
    //Cohesion Variables
    public  float cohesionRadius;
    public float cohesionPriority;
    //Alignment Variables
    public float alignmentRadius;
    public float alignmentPriority;
    //Seperation Variables
    public float sepprationRadius;
    public float seperationPriority;
    //Avoidance Variables
    public float avoidanceRadius;
    public float avoidancePriority;

    public float hunger;
    public float thirst;
    public float hungerMax;
    public float thirstMax;

    public float attack;
    public float attackBase = 10;
    public float attackModifier = 1;
    public Agent A;
    
    // Start is called before the first frame update
    void Start()
    {
        A = GetComponent<Agent>();
        if (adult == true)
        {
            energyModifier = Random.Range((0.9f * energyModifier), (1.1f * energyModifier));
            healthModifier = Random.Range((0.9f * healthModifier), (1.1f * healthModifier));
            speedModifier = Random.Range((0.9f * speedModifier), (1.1f * speedModifier));
            accelModifier = Random.Range((0.9f * accelModifier), (1.1f * accelModifier));
            rotModifier = Random.Range((0.9f * rotModifier), (1.1f * rotModifier));
            angularAccelModifier = Random.Range((0.9f * angularAccelModifier), (1.1f * angularAccelModifier));
            attackModifier = Random.Range((0.9f * attackModifier), (1.1f * attackModifier));

            energyMax = (100 * energyModifier);
            healthMax = (100 * healthModifier);
            speedMax = (A.maxSpeed * speedModifier);
            accelMax = (A.maxAccel * accelModifier);
            rotMax = (A.maxRotation * rotModifier);
            angularAccelMax = (A.maxAngularAccel * angularAccelModifier);
            attack = (attackBase * attackModifier);

            A.maxSpeed = speedMax;
            A.maxAccel = accelMax;
            A.maxRotation = rotMax;
            A.maxAngularAccel = angularAccelMax;

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
