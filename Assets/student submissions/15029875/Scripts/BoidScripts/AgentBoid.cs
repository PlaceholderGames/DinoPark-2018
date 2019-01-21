// Perhaps my favourite name of a file ever. I think I'll call each bird that.
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

    // neighbourRange is used in SeparationVec to determine how close to get.
    // In the future it might be good to put this in a random range,
    // but for now this should be fine.
    float neighourRange = 10.0f;
    public float speed = 0.1f;

    // Use this for initialization
    void Start()
    {
        // Start by setting speed to be a random float. Adds a bit of realism.
        //speed = Random.Range(0.1f, 0.2f);
    }
    
    // Vector3 function to return the separation vector. We'll grab targets with an overlap sphere.
    Vector3 SeparationVec(Transform target)
    {
        // Work out the difference between this position and the target position.
        var difference = transform.position - target.transform.position;
        var differenceLen = difference.magnitude;
        // By using Clamp01 we can clamp between 0 and 1.
        var scaler = Mathf.Clamp01(1.0f - differenceLen / neighourRange);
        return difference * (scaler / differenceLen);
    }

    // Update is called once per frame
    void Update()
    {
        // Find agents near the current boid. Using the neighbourRange this
        // draws a sphere around the boid and detects any other voids inside of it.
        // This is how we'll clump the boids together to form a flock.
        var detectedBoids = Physics.OverlapSphere(transform.position, neighourRange);

        // Start by declaring empty vectors for the three tenants.
        // These 3 culminate at the bottom of the update loop in a simple transform.position change.
        var separation = Vector3.zero;
        var cohesion   = transform.forward;
        var alignment  = transform.position;

        // Separation //
        // Because we're in the update loop this foreach will constantly detect
        // our detetced boids, allowing us to keep track of them.
        foreach (var boid in detectedBoids)
        {
            var t = boid.transform;
            separation += SeparationVec(t);
            alignment  += t.forward;
            cohesion   += t.position;
        }

        // Cohesion //
        // Average the boids nearby.
        var boidsAveraged = 1.0f / detectedBoids.Length;
        // Now that we have the average we can apply them to the relevant tenants.
        cohesion  *= boidsAveraged;
        alignment *= boidsAveraged;
        cohesion   = (cohesion - transform.position).normalized;

        // Alignment //
        // Set a var alignedDirection which is the sum of all three tenants.
        // Using a quaternion we can make a smooth rotation into this direction [from
        // vector3.forward to the alignedDirection] and later, hopefully, apply it using a Slerp.
        var alignedDirection = separation + alignment + cohesion;
        var alignedRotation = Quaternion.FromToRotation(Vector3.forward, alignedDirection.normalized);

        // If the agent isn't already aligned:
        if (alignedRotation != transform.rotation)
        {
            // Use a Slerp to gradually rotation towards the desired alignment.
            transform.rotation = Quaternion.Slerp(alignedRotation, transform.rotation, 0.01f * 10);
        }

        // Access the public static variable goal in BoidController.
        Vector3 boidGoal = BoidController.goal;
        Debug.Log("Controller goal: ");
        Debug.Log(boidGoal.ToString());
        transform.position = Vector3.MoveTowards(transform.position, boidGoal, speed);
        var goalRotation = Quaternion.FromToRotation(Vector3.forward, boidGoal.normalized);
        transform.rotation = Quaternion.Slerp(goalRotation, transform.rotation, 0.01f * 10);

    }
}
