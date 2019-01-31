// Script which acts as a controller for a group of boid agents.
// The purpose of this script is to set the initial population of
// agents and spawn them in random locations around the controller.
// Because of the scale of the map we're working in, we'll probably
// have two or three of these controllers around the map
// to spawn flocks. I am so glad I chose fish for my first AI coursework -
// this is nearly identical, but better!
// Note: the lack of consistency is my fault - for the nonce agent and boid are the same thing.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    // Serialized field for our prefab.
    [SerializeField]
    private AgentBoid prefab;

    private static int flockSize = 30;

    // The variables below are weightings for the three
    // tenants of the boid algorithm.
    [SerializeField]
    private float alignmentWeight = 0.5f;

    [SerializeField]
    private float cohesionWeight = 0.9f;

    [SerializeField]
    private float separationWeight = 0.1f;

    // Set up spawn variables here. Start the location at zero.
    [SerializeField]
    private float spawnRadius = 3.0f;
    private Vector3 spawnLocation = Vector3.zero;

    private AgentBoid[] boids;

    // Essentially spawn the agents and assign them to a list.
    private void Awake()
    {
        // Declare a new list boidList to be the size of our flock.
        boids = new AgentBoid[flockSize];

        for (int i = 0; i < flockSize; i++)
        {
            // Use Random.insideUnitSphere with other vector variables to 
            // determine how spread out our spawning will be.
            spawnLocation = Random.insideUnitSphere * spawnRadius + transform.position;

            boids[i] = Instantiate(prefab, spawnLocation, Quaternion.identity) as AgentBoid;
            boids[i].transform.parent = transform;
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < flockSize; i++)
        {
            // Select an individual boid.
            AgentBoid boid = boids[i];
            // If the boid is not equal to null and the rigidbody is not equal to null:
            if (boid != null && boid.boidRB != null)
            {
                // Set the rules of the Reynolds algorithm by accessing the corresponding functions.
                Vector3 parsedAlignment = Alignment(boid) * alignmentWeight * Time.deltaTime;
                Vector3 parsedCohesion = Cohesion(boid) * cohesionWeight * Time.deltaTime;
                Vector3 parsedSeparation = Separation(boid) * separationWeight * Time.deltaTime;

                // Where the magic happens. Sum the vectors together.
                boid.boidRB.velocity += (parsedAlignment + parsedCohesion + parsedSeparation);
            }
        }
    }

    private Vector3 Alignment(AgentBoid boid)
    {
        // Set velocity to zero.
        Vector3 alignment = Vector3.zero;
        int iterator = 0;
        for(int i = 0; i < flockSize; i++)
        {
            // Returns the distance between a and b.
            // Use this to find the distance between a boid and it's neighbour.
            float distance = Vector3.Distance(boids[i].transform.localPosition, boid.transform.localPosition);
            // If the distance between the two is greater than zero and less than the radius
            // defined in the AgentBoid script, then:
            if (distance > 0 && distance < boid.neighbourRad)
            {
                // Set the velocity.
                alignment += boids[i].boidRB.velocity;
                iterator++;
            }
        }
        // Return the alignment. Do not forget to normalize this!
        if (iterator > 0)
        { return (alignment / (flockSize - 1)).normalized; }
        else
        { return Vector3.zero; }
    }

    private Vector3 Cohesion(AgentBoid boid)
    {
        Vector3 cohesion = Vector3.zero;
        int iterator = 0;
        for(int i = 0; i < flockSize; i++)
        {
            // Returns the distance between a and b.
            // Use this to find the distance between a boid and it's neighbour.
            float distance = Vector3.Distance(boids[i].transform.localPosition, boid.transform.localPosition);
            if (distance > 0 && distance < boid.neighbourRad)
            {
                // Set the velocity.
                cohesion += boids[i].transform.localPosition;
                iterator++;
            }
        }
        // Return the cohesion vector. Do not forget to normalize this!
        if (iterator > 0)
        { return ((cohesion / (flockSize - 1)) - boid.transform.localPosition).normalized; }
        else
        { return Vector3.zero; }
    }

    private Vector3 Separation(AgentBoid boid)
    {
        Vector3 separation = Vector3.zero;
        int iterator = 0;
        for (int i = 0; i < flockSize; i++)
        {
            // Returns the distance between a and b.
            // Use this to find the distance between a boid and it's neighbour.
            float distance = Vector3.Distance(boids[i].transform.localPosition, boid.transform.localPosition);
            if(distance > 0 && distance < boid.separationBound)
            {
                separation -= (boids[i].transform.localPosition - boid.transform.localPosition).normalized / distance;
                iterator++;
            }
        }
        // Return the separation vector. Do not forget to normalize this!
        if (iterator > 0)
        { return (separation / (flockSize - 1)).normalized; }
        else
        { return Vector3.zero; }
    }
}
