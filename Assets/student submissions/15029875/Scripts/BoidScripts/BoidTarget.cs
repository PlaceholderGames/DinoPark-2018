// This script provides a target for our agent to move towards.
// It a random point in space within bounds.
// If the agent reaches a certain threshold distance to the target,
// a new target is calculated.
// This will replicate the unpredictable movement common in flocks
// of birds.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidTarget : MonoBehaviour {

    // NOTE:
    // I've been playing with the bounds a bit, and if it's too large (the entire map)
    // it doesn't really capture the algorithm very well.
    // A well around this might be to have many boid controller objects
    // with shorter bounds?
    [SerializeField]
    private Vector3 bounds;

    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private float turnSpeed = 3.0f;

    [SerializeField]
    private float targetPointTolerance = 5.0f;

    private Vector3 initialPosition;
    private Vector3 nextMovementPoint;
    private Vector3 targetPosition;

    private void Awake()
    {
        initialPosition = transform.position;
        CalculateNextMovementPoint();
    }

    private void CalculateNextMovementPoint()
    {
        // Set a random target position within the bounds specification in the vector 3 variable.
        float posX = Random.Range(initialPosition.x - bounds.x, initialPosition.x + bounds.x);
        float posY = Random.Range(initialPosition.y - bounds.y, initialPosition.y + bounds.y);
        float posZ = Random.Range(initialPosition.z - bounds.z, initialPosition.z + bounds.z);

        targetPosition.x = posX;
        targetPosition.y = posY;
        targetPosition.z = posZ;

        // Set the new point to be the sum of the agent's target position and initial position.
        nextMovementPoint = initialPosition + targetPosition;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        // Good ol' slerp rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, 
                             Quaternion.LookRotation(nextMovementPoint - transform.position),
                             turnSpeed * Time.deltaTime);

        // Compare the current position to the movement point, determing the agent's distance.
        if (Vector3.Distance(nextMovementPoint, transform.position) <= targetPointTolerance)
        {
            // If we're within the tolerance level then calculate a new random position.
            CalculateNextMovementPoint(); 
        }
    }
}
