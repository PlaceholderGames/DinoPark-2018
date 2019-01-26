// Perhaps my favourite name of a file ever. I think I'll call each bird that.
// Agent Boid.
// The purpose of this script is to be attached to an agent prefab.
// BoidController.cs will spawn these prefabs and this script will
// set rules and behaviour. Hopefully some fun emergent behaviour
// appears as a result.
// There are three primary tenants to a boid algorithm:
// Separation // Alignment // Cohesion //
// NOTE: I use a lot of "tenants" in this script. "Tenants" also means "rules".
// This script has been adapted from the third edition of
// Unity 2017 Game AI Programming written by Barrera, Kyaw and Swe.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBoid : MonoBehaviour
{
    // Create private variables we want to access from the editor.
    [SerializeField]
    private BoidController boidController;
    // Set two directions: the current and target direction.
    private Vector3 targetDirection;
    private Vector3 direction;

    // Standard set/get for the controller.
   public BoidController BoidController
    {
        get { return boidController;  }
        set { boidController = value;  }
    }

    // Get the direction.
    public Vector3 Direction { get { return direction; } }

    private void Awake()
    {
        // Initialize with a standard forward direction.
        direction = transform.forward.normalized;
        // A bit of validation.
        if(boidController != null)
        {
            Debug.Log("No boid controller attached.");
        }
    }

    private void Update()
    {
        targetDirection = BoidController.Flock(this, transform.localPosition, direction);
        // If there is no target direction:
        if(targetDirection == Vector3.zero)
        {
            // Do nothing.
            return;
        }
        // Else set the direction of the agent to a direction with a speed 
        // taken from the controller and move towards it.
        direction = targetDirection.normalized;
        direction *= boidController.SpeedModifier;
        transform.Translate(direction * Time.deltaTime);
    }
}
