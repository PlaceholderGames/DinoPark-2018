// Perhaps my favourite name of a file ever. I think I'll call each bird that.
// Agent Boid.
// The purpose of this script is to be attached to an agent prefab.
// BoidController.cs will spawn these prefabs and this script will
// set rules and behaviour. Hopefully some fun emergent behaviour
// appears as a result.
// There are three primary tenants to a boid algorithm:
// Separation // Alignment // Cohesion //
// Hopefully these can be summarized in one function each
// and unleashed in the Update function, free for Agent Boid
// to go where he or she pleases.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBoid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Each boid must maintain a reasonable amount of distance between
    // itself and neighbours, otherwise the flock would become overcrowded.
    void Separation ()
    {
    }

    // The boids should have an average alignment. If a boid deviates from this
    // alignment then there could be some fun emergent behaviour. For the most part
    // try to keep them heading towards a point with an average heading.
    void Alignmnent ()
    {

    }

    // Every boid should try to move towards the average position of all other
    // boids. As opposed to separation, this dictates how close roughly a boid
    // should be.
    void Cohesion()
    {

    }   
	
	// Update is called once per frame
	void Update () {
        // All three functions return velocities. These are used to determine the transform.position.
        // TEST CODE. ATTEMPTING TO GET SEAGULL ROTATION CORRECT.
        transform.position = transform.position + transform.forward * 0.1f;
	}
}
