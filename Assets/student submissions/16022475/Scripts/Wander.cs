using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Wander : Face {

    public float offset;
    public float radius;
    public float rate;
    public bool state = false;
    public NavMeshAgent nav;
    public override void Awake()
    {
        target = new GameObject();
       // target.transform.position = transform.position;
       // target = GameObject.FindGameObjectWithTag("Dead");
        nav = GetComponent<NavMeshAgent>();
        base.Awake();
        nav.SetDestination(target.transform.position);
    }

    public override Steering GetSteering()
    {
        //I can now change wander public float state to allow the dino to wander OR stay still if it were eating, sleeping, resting etc
        if (state == true)
        {
            Steering steering = new Steering();
            float wanderOrientation = Random.Range(-1.0f, 1.0f) * rate;
            float targetOrientation = wanderOrientation + agent.orientation;
            Vector3 orientationVec = OriToVec(agent.orientation);
            Vector3 targetPosition = (offset * orientationVec) + transform.position;
            targetPosition = targetPosition + (OriToVec(targetOrientation) * radius);
            targetAux.transform.position = targetPosition;
            steering = base.GetSteering();
            steering.linear = targetAux.transform.position - transform.position;
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
            return steering;
        }
        else
        {
            Steering steering = new Steering();
            steering.linear = transform.position - transform.position;
            //steering.linear.Normalize();
            return steering;
        }
      
    }
}
