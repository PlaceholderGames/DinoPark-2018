using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaptyStateMachine : MonoBehaviour
{
    public int State = 0;
    private float wanderRadius;
    private float wanderTimer = 1;
    private float timer;
    private Transform target;

    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;
    private Vector3 closestPoint;

    public List<RaptyStateMachine> members;
    public List<AnkyStateMachine> enemies;
    public RaptyStats S;
    public AnkyStats A;

   // public Wander W;
   // public Pursue P;
  //  public Face F;
    public NavMeshAgent N;
    public GameObject deadModel;
    public GameObject water;
    public GameObject[] Anky;

    public GameObject objective;
    // Start is called before the first frame update
    void Start()
    {
        water = GameObject.FindGameObjectWithTag("Water");
        S = GetComponent<RaptyStats>();
       // W = GetComponent<Wander>();
     //   P = GetComponent<Pursue>();
      //  F = GetComponent<Face>();
        N = GetComponent<NavMeshAgent>();
        members = new List<RaptyStateMachine>();
        InvokeRepeating("loop", 2.0f, 1.0f);
        Anky = GameObject.FindGameObjectsWithTag("Anky");
       
       // W.state = true;
      //  P.state = true;
       // F.state = true;
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
        else if (other.gameObject.tag == "Water")
        {
            while (S.thirst < S.thirstMax)
            {
                S.thirst++;
            }
            State = 1;
        }
        else if (other.gameObject.tag == "Dead")
        {
            while (S.hunger < S.hungerMax)
            {
                S.hunger++;
            }
            State = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        position = transform.position;
        if (S.health <= 0)
        {
            A = null;
            //W.target = null;
           // P.target = null;
            //F.target = null;
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
        if (timer >= wanderTimer && State == 1)
        {

            Vector3 newPos = Random.insideUnitSphere * 20;
            newPos += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(newPos, out hit, Random.Range(0f, 20), 1);
            Vector3 destination = hit.position;
            N.SetDestination(destination);
            timer = 0;
           
        }
       
    }

    void loop()
    {
        S.hunger -= 0.5f;
        S.thirst -= 1f;
        S.energy = S.hunger + S.thirst;
        Wander();
    }

    void Wander()
    {
        State = 1;
        if (S.hunger < (S.hunger/2))
        {
            State = 2;
            Hungry(); 
        }
        else if (S.thirst < (S.thirst/2))
        {
            State = 3;
            Thirsty();
        }
        else if (S.health < S.healthMax)
        {
            Injured();
        }
        else 
        {

        }
        
    }

    void Hungry()
    {
       // P.target = Anky[0];
       // F.target = Anky[0];
        Grouping();
        
    }

    void Thirsty()
    {
   

        Searching();
    }
    //private void OnDrawGizmos()
    //{
    //    var col = water.GetComponent<Collider>();
    //    if(!water)
    //    {
    //        return;
    //    }
    //    closestPoint = col.ClosestPointOnBounds(position);
    //    Gizmos.DrawSphere(position, 10f);
    //    //objective = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //    //objective.transform.position = closestPoint;
    //    Gizmos.DrawWireSphere(closestPoint, 1f);
    
    //}
    void Hunting()
    {

        //search for some anky to attack or scavenge
        Attack();
    }

    //public List<RaptyStateMachine> GetNeighbours(RaptyStateMachine member, float radius)
    //{
    //    List<RaptyStateMachine> neighboursFound = new List<RaptyStateMachine>();

    //    foreach (var otherRapty in members)
    //    {
    //      // if (otherRapty = member)
    //        {
    //            continue;
    //        }
    //        if (Vector3.Distance(member.position, otherRapty.position) <= radius)
    //        {
    //            neighboursFound.Add(otherRapty);
    //        }
    //    }
    //    return neighboursFound;
    //}
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
        while (S.energy > 0 && S.health <= S.healthMax)
        {
            S.health++;
            S.energy--;
        }
    }

    void Searching()
    {
        Vector3 newPos = Random.insideUnitSphere * 50;//RandomNavSphere(transform.position)
        newPos += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(newPos, out hit, Random.Range(0f, 50), 1);
        if (hit.position.y <= 20)
        {
            Vector3 destination = hit.position;
            N.SetDestination(destination);
        }
        //look for water and when found go to it, and when reached go to drinking
        Drinking();
    }

    void Drinking()
    {
        
        
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
