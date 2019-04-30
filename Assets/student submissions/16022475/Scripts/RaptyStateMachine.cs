using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyStateMachine : MonoBehaviour
{
    
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public List<RaptyStateMachine> members;
    public List<AnkyStateMachine> enemies;
    public RaptyStats S;
    public AnkyStats A;
    public Wander W;
    public Pursue P;
    public Face F;
    public GameObject deadModel;
    // Start is called before the first frame update
    void Start()
    {
        S = GetComponent<RaptyStats>();
        W = GetComponent<Wander>();
        P = GetComponent<Pursue>();
        F = GetComponent<Face>();
        members = new List<RaptyStateMachine>();
        InvokeRepeating("loop", 2.0f, 1.0f);

        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Anky")
        {
            float a = Random.Range(1, 10);
            if (a >= 8)
            {
                float i = Random.Range(1, 2);
                if (i <= 1.5)
                {
                    A = other.gameObject.GetComponent<AnkyStats>();

                    A.health -= S.attack;
                }
            }
            //chance to attack and do damage
        }
    }
    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        if (S.health <= 0)
        {
            A = null;
            W.target = null;
            P.target = null;
            F.target = null;
            Instantiate(deadModel, transform);
            Destroy(gameObject);

        }
        if (S.hunger < 0 && S.thirst < 0)
        {
            S.health -= 0.004f;
        }
        else if (S.hunger < 0)
        {
            S.health -= 0.001f;
        }
        else if (S.thirst < 0)
        {
            S.health -= 0.001f;
        }
    }

    void loop()
    {
        S.hunger -= 0.5f;
        S.thirst -= 1f;
        Wander();
        S.energy = S.hunger + S.thirst;
    }

    void Wander()
    {
        
        if (S.hunger < (S.hunger/2))
        {
            Hungry(); 
        }
        else if (S.thirst < (S.thirst/2))
        {
            Thirsty();
        }
        else if (S.health < S.healthMax)
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

    public List<RaptyStateMachine> GetNeighbours(RaptyStateMachine member, float radius)
    {
        List<RaptyStateMachine> neighboursFound = new List<RaptyStateMachine>();

        foreach (var otherRapty in members)
        {
          //  if (otherRapty = member)
            {
                continue;
            }
            if (Vector3.Distance(member.position, otherRapty.position) <= radius)
            {
                neighboursFound.Add(otherRapty);
            }
        }
        return neighboursFound;
    }
    void Grouping()
    {
        
        Hunting();
    }

    void Attack()
    {        //if in range of a target attack, if health is below a certain amount,retreat
        if (S.health <= S.healthMax/2)
        {
            Injured();
        }
    }

    void Eating()
    {
        S.hunger++;
    }

    void Injured()
    {
        //move somewhere away from other creatures and rest
        Resting();
    }

    void Resting()
    {
        if (S.health == S.healthMax)
        {
            loop();
        }
        S.health++;
        S.energy--;
    }

    void Searching()
    {
        Wander();
        //look for water and when found go to it, and when reached go to drinking
        Drinking();
    }

    void Drinking()
    {
        while (S.thirst < S.thirstMax)
        {
            S.thirst++;
        }
    }

    void Breed()
    {
        //when energy is high and the place is safe, choose 1 of the other rapty to breed
        if (S.energy > (S.energyMax * 0.9f) && S.health == S.healthMax)
        {
            //using a list or array of the nearby members, grab the MAX values of the parents and combine them
        }
    }
}
