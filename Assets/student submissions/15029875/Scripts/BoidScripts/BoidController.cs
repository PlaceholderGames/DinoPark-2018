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

    // Set our boid GameObject to public, allows for faster debugging.
    // Can just plug our agent in the Unity viewer like this.
    public GameObject agent;
    public static int flockSize = 10;   

    // Declare an empty vector 3 to be the agent's current goal.
    public static Vector3 goal = Vector3.zero;
    public GameObject goalPrefab;

    // In the start function we want to set a starting position for each
    // agent and instantiate them in these spots.
    void Start()
    {
        for (int i = 0; i < flockSize; i++)
        {
            // In hindsight I probably should have named this Spawn
            // and the other function something else. It'll do for now.
            SpawnParse();
        }

        // In the start function call setGoal. In the setGoal function,
        // we call Invoke again every random seconds.
        Invoke("setGoal", 1.0f);
    }

    // Parse a Vector3 to another function. SpawnParse essentially uses insideUnitSphere
    // to set a location around the radius of the controller.
    public GameObject SpawnParse()
    {
        return Spawn(transform.position + Random.insideUnitSphere * 5);
    }

    // Take the parsed position, which should be inside a sphere radius (if not - something's gone
    // terribly wrong...) and instantiate the agnet prefab with a random rotation.
    public GameObject Spawn(Vector3 parsedPosition)
    {
        var rotation = Quaternion.Slerp(transform.rotation, Random.rotation, 0.2f);
        var boid = Instantiate(agent, parsedPosition, rotation) as GameObject;
        return boid;
    }

    // Every random seconds, create a goal for the boid to head for.
    public void setGoal()
    {

        // Remember the map is very large. Try this to begin with, tweak later.
        goal = new Vector3(Random.Range(0, 1500),    // x
                           Random.Range(200, 300),   // y
                           Random.Range(0, 1500));   // z

        goalPrefab.transform.position = goal;

        // Repeatedly call this function every x seconds.
        Invoke("setGoal", Random.Range(1.0f, 10.0f));
    }
}
