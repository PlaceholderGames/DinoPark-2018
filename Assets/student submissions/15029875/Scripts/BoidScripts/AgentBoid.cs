﻿// Perhaps my favourite name of a file ever. I think I'll call each bird that.
// Agent Boid.
// The purpose of this script is to be attached to an agent prefab.
// BoidController.cs will spawn these prefabs and this script will
// set rules and behaviour. Hopefully some fun emergent behaviour
// appears as a result.
// There are three primary tenants to a boid algorithm:
// Separation // Alignment // Cohesion //
// NOTE: I use a lot of "tenants" in this script. "Tenants" also means "rules".
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBoid : MonoBehaviour
{
    public float speed = 0.1f;

    // Use this for initialization
    void Start()
    {
        // Start by setting speed to be a random float. Adds a bit of realism.
        speed = Random.Range(0.1f, 0.15f);
    }
   
    // Update is called once per frame
    void Update()
    {

        // Access the public static variable goal in BoidController.
        // The boid will primarily move towards the goal from the BoidController class which randomly
        // spawns around the map.
        Vector3 boidGoal = BoidController.goal;
        transform.position = Vector3.MoveTowards(transform.position, boidGoal, speed);
    }

    // Take a tenant of the boid algorithm and apply it.
    void ApplyTenant(Vector3 tenant)
    {
        // TO-DO: Make the tenant impact the transform.
    }

    void FlockingUpdate()
    {
        // TO-DO: make an array or list in BoidController so we can pass the game object to the functions.
        // Set Vector3 for each boid algorithm tenant here.
        Vector3 separation = Separation(//boid);
        Vector3 alignment = Alignment(//boid);
        Vector3 cohesion = Cohesion(//boid);

        // Use the ApplyTenant() function to apply the 3 tenants to the boid.
        ApplyTenant(separation);
        ApplyTenant(alignment);
        ApplyTenant(cohesion);
    }

    // Keep a degree of separation amongst the boids.
    Vector3 Separation()
    {
        return new Vector3(0, 0, 0);
    }

    // Calculate the average angle and velocity of all nearby boids.
    Vector3 Alignment()
    {
        return new Vector3(0, 0, 0);
    }

    // Calculate the rotation angle towards the center of the flock.
    Vector3 Cohesion()
    {
        return new Vector3(0, 0, 0);
    }
}