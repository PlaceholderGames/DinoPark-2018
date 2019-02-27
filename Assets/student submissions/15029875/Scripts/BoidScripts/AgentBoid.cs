// Perhaps my favourite name of a file ever. I think I'll call each bird that.
// Agent Boid.
// The purpose of this script is to be attached to an agent prefab.
// BoidController.cs will spawn these prefabs and this script will
// set rules and behaviour. Hopefully some fun emergent behaviour
// appears as a result.
// There are three primary tenants to a boid algorithm:
// Separation // Alignment // Cohesion //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBoid : AgentBase
{
    private BoidController boidController;

    public Rigidbody boidRB { get; private set; }
 
    // Create private variables we want to access from the editor.
    [SerializeField]
    public float neighbourRad;

    [SerializeField]
    public float separationBound;

    private void Awake()
    {
        if(boidController != null)
        {
            Debug.Log("No boid controller attached.");
        }

        boidRB = GetComponent<Rigidbody>();

        boidController = GetComponentInParent<BoidController>();

        // Initialize random velocities.
        boidRB.velocity = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
    }

    public override void Update()
    {
        // Always face forward.
        transform.rotation = Quaternion.LookRotation(boidRB.velocity);
    }

    private void FixedUpdate()
    {
        positionLogic();

    }

    private void positionLogic()
    {
        if ( Vector3.Distance(transform.localPosition, Vector3.zero) > 40f )
        {
            boidRB.velocity += (Vector3.zero - transform.localPosition) * 0.1f * Time.deltaTime;
        }
    }
}
