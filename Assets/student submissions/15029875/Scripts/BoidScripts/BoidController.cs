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
    private static int flockSize = 40;

    private float speedModifier = 5;

    // The variables below are weightings for the three
    // tenants of the boid algorithm.
    [SerializeField]
    private float alignmentWeight = 1;

    [SerializeField]
    private float cohesionWeight = 1;

    [SerializeField]
    private float separationWeight = 1;

    [SerializeField]
    private float followWeight = 5;

    // Serialized field for our prefab.
    [SerializeField]
    private AgentBoid prefab;

    // Set up spawn variables here. Start the location at zero.
    [SerializeField]
    private float spawnRadius = 3.0f;
    private Vector3 spawnLocation = Vector3.zero;

    [SerializeField]
    public Transform target;

    // Three vectors for the three tenants of the Reynold algorithm.
    // The fourth is the separation value.
    private Vector3 flockCenter;
    private Vector3 flockDirection;
    private Vector3 targetDirection;
    private Vector3 separation;

    // We need a way to get the speed modified for AgentBoid.
    public float SpeedModifier { get { return speedModifier; } }

    public List<AgentBoid> boidList = new List<AgentBoid>();

    // Essentially spawn the agents and assign them to a list.
    private void Awake()
    {
        // Declare a new list boidList to be the size of our flock.
        boidList = new List<AgentBoid>(flockSize);
        for (int i = 0; i < flockSize; i++)
        {
            // Use Random.insideUnitSphere with other vector variables to 
            // determine how spread out our spawning will be.
            spawnLocation = Random.insideUnitSphere * spawnRadius + transform.position;
            AgentBoid boid = Instantiate(prefab, spawnLocation, transform.rotation) as AgentBoid;
            boid.transform.parent = transform;
            boid.BoidController = this;
            // Finally add the boid to the list.
            boidList.Add(boid);
        }
    }

    public Vector3 Flock(AgentBoid boid, Vector3 boidPosition, Vector3 boidDirection)
    {
        // Set the four vectors used by the Reynold alogirithm to zero for now.
        flockDirection = Vector3.zero;
        flockCenter = Vector3.zero;
        targetDirection = Vector3.zero;
        separation = Vector3.zero;

        // For every boid we're aware of:
        for (int i = 0; i < boidList.Count; i++)
        {
            // Find a neighbouring boid.
            AgentBoid neighbour = boidList[i];
            // Only check against a neighbour.
            if(neighbour != boid)
            {
                // Take the direction of all the boids.
                flockDirection += neighbour.Direction;
                // Take the position of all the boids.
                // Note: use local position for this.
                flockCenter += neighbour.transform.localPosition;
                // Take the delta to all neighbour boids.
                separation += neighbour.transform.localPosition - boidPosition;
                separation *= -1;
            }
        }

        // ALIGNMENT //
        // This is the average direction of all agents.
        // Note: remember to normalize.
        flockDirection /= flockSize;
        flockDirection = flockDirection.normalized * alignmentWeight;

        // COHESION //
        // Take the center of the flock and try to keep averagely (is that a word?)
        // with the group.
        flockCenter /= flockSize;
        flockCenter = flockCenter.normalized * cohesionWeight;

        // SEPARATION //
        // Keep the agents separated with a fair distance.
        separation /= flockSize;
        separation = separation.normalized * separationWeight;

        // Now set a direction vector for the flock to head towards.
        // If this is a bit wonky you can change the followWeight variable.
        targetDirection = target.localPosition - boidPosition;
        targetDirection = targetDirection * followWeight;
        
        // Return the sum of all these vectors and this is where the magic takes place.
        return flockDirection + flockCenter + separation + targetDirection;
    }
}
