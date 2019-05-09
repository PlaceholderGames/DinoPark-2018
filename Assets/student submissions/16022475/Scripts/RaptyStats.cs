using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaptyStats : MonoBehaviour
{
    public float age = 0;
    bool adult = true;
    //Energy Stats
    public float energyMax;
    public float energy;
    public float energyModifier = 1;
    //Health Stats
    public float healthMax;
    public float health;
    public float healthModifier = 1;
    //Speed Stats
    float speedMax;
    public float speed;
    public float speedModifier = 1;
    //Acceleration Stats
    float accelMax;
    protected float accel;
    public float accelModifier = 1;
    //Rotation Stats
    float rotMax;
    public float rot;
    public float rotModifier = 1;
    //Angular Rotation Stats
    float angularSpeedMax;
    protected float angularSpeed;
    public float angularSpeedModifier = 1;
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
    public  NavMeshAgent A;
    //Potential extensions for breeding
    public RaptyStats P1;
    public RaptyStats P2;
  

    // Start is called before the first frame update
    void Start()
    {
        A = GetComponent<NavMeshAgent>();
        if (adult == true)
        {
            energyModifier = Random.Range((0.9f * energyModifier), (1.1f * energyModifier));
            healthModifier = Random.Range((0.9f * healthModifier), (1.1f * healthModifier));
            speedModifier = Random.Range((0.9f * speedModifier), (1.1f * speedModifier));
            accelModifier = Random.Range((0.9f * accelModifier), (1.1f * accelModifier));
            rotModifier = Random.Range((0.9f * rotModifier), (1.1f * rotModifier));
            angularSpeedModifier = Random.Range((0.9f * angularSpeedModifier), (1.1f * angularSpeedModifier));
            attackModifier = Random.Range((0.9f * attackModifier), (1.1f * attackModifier));

            energyMax = (100 * energyModifier);
            healthMax = (100 * healthModifier);
            speedMax = (A.speed * speedModifier);
            accelMax = (A.acceleration * accelModifier);
         
            angularSpeedMax = (A.angularSpeed * angularSpeedModifier);
            attack = (attackBase * attackModifier);

            A.speed = speedMax;
            A.acceleration = accelMax;
            A.angularSpeed = angularSpeedMax;

            energy = energyMax;
            health = healthMax;
            speed = speedMax;
            accel = accelMax;
        
            angularSpeed = angularSpeedMax;

            hungerMax = energyMax / 2;
            thirstMax = energyMax / 2;
            hunger = hungerMax;
            thirst = thirstMax;

            
        }
        else if (adult == false)
        {
            //Parental Variation
            energyModifier = (P1.energyModifier + P2.energyModifier / 2);
            healthModifier = (P1.healthModifier + P2.healthModifier / 2);
            speedModifier = (P1.speedModifier + P2.speedModifier / 2);
            accelModifier = (P1.accelModifier + P2.accelModifier / 2);
            rotModifier = (P1.rotModifier + P2.rotModifier / 2);
            angularSpeedModifier = (P1.angularSpeedModifier + P2.angularSpeedModifier / 2);
            attackModifier = (P1.attackModifier + P2.attackModifier / 2);
            //Random Variation
            energyModifier = Random.Range((0.9f * energyModifier), (1.1f * energyModifier));
            healthModifier = Random.Range((0.9f * healthModifier), (1.1f * healthModifier));
            speedModifier = Random.Range((0.9f * speedModifier), (1.1f * speedModifier));
            accelModifier = Random.Range((0.9f * accelModifier), (1.1f * accelModifier));
            rotModifier = Random.Range((0.9f * rotModifier), (1.1f * rotModifier));
            angularSpeedModifier = Random.Range((0.9f * angularSpeedModifier), (1.1f * angularSpeedModifier));
            attackModifier = Random.Range((0.9f * attackModifier), (1.1f * attackModifier));

            energyMax = (50 * energyModifier);
            healthMax = (50 * healthModifier);
            speedMax = (A.speed * speedModifier);
            accelMax = (A.acceleration * accelModifier);

            angularSpeedMax = (A.angularSpeed * angularSpeedModifier);
            attack = (attackBase * attackModifier / 2);

            A.speed = speedMax;
            A.acceleration = accelMax;
            A.angularSpeed = angularSpeedMax;

            energy = energyMax;
            health = healthMax;
            speed = speedMax;
            accel = accelMax;

            angularSpeed = angularSpeedMax;

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
