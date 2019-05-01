using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour
{

    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public Level level;
    public MemberConfig config;

    private Vector3 wanderTarget;

    // Use this for initialization
    void Start ()
    {

        level = FindObjectOfType<Level>();
        config = FindObjectOfType<MemberConfig>();

        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);

    }

    // Update is called once per frame
    void Update ()
    {

        acceleration = Combine();
        acceleration = Vector3.ClampMagnitude(acceleration, config.maxAcceleration);

        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, config.maxVelocity);

        position = position + velocity * Time.deltaTime;

        WrapAround(ref position, -level.bounds, level.bounds);

        transform.position = position;

    }

    protected Vector3 Wander ()
    {

        float jitter = config.wanderJitter * Time.deltaTime;

        wanderTarget += new Vector3(0, RandomBinomial() * jitter, 0);
        wanderTarget = wanderTarget.normalized;
        wanderTarget *= config.wanderRadius;

        Vector3 targetInLocalSpace = wanderTarget + new Vector3(config.wanderDistance, config.wanderDistance, 0);
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);

        targetInWorldSpace -= this.position;

        return targetInWorldSpace.normalized;

    }

    private Vector3 Cohesion ()
    {

        Vector3 cohesionVector = new Vector3();

        int countMembers = 0;

        var neighbours = level.GetNeighbours(this, config.cohesionRadius);

        if (neighbours.Count == 0)
        {

            return cohesionVector;

        }

        foreach (var member in neighbours)
        {

            if (isInFOV(member.position))
            {

                cohesionVector += member.position;
                countMembers++;

            }

        }

        if (countMembers == 0)
        {

            return cohesionVector;

        }

        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.position;
        cohesionVector = Vector3.Normalize(cohesionVector);

        return cohesionVector;

    }

    private Vector3 Alignment ()
    {

        Vector3 alignVector = new Vector3();

        var members = level.GetNeighbours(this, config.alignmentRadius);

        if (members.Count == 0)
        {

            return alignVector;

        }

        foreach (var member in members)
        {

            if (isInFOV(member.position))
            {

                alignVector += member.velocity;

            }

        }

        return alignVector.normalized;

    }

    private Vector3 Seperation ()
    {

        Vector3 seperateVector = new Vector3();

        var members = level.GetNeighbours(this, config.seperationRadius);

        if (members.Count == 0)
        {

            return seperateVector;

        }

        foreach (var member in members)
        {

            if (isInFOV(member.position))
            {

                Vector3 movingTowards = this.position - member.position;

                if (movingTowards.magnitude > 0)
                {

                    seperateVector += movingTowards.normalized / movingTowards.magnitude;

                }

            }

        }

        return seperateVector.normalized;

    }

    private Vector3 Avoidance ()
    {

        Vector3 avoidVector = new Vector3();

        var enemyList = level.GetEnemies(this, config.avoidanceRadius);

        if (enemyList.Count == 0)
        {

            return avoidVector;

        }

        foreach (var enemy in enemyList)
        {

            //avoidVector += RunAway(enemy.position);

        }

        return avoidVector.normalized;

    }

    private Vector3 RunAway (Vector3 target)
    {

        Vector3 neededVelocity = (position - target).normalized * config.maxVelocity;

        return neededVelocity - velocity;

    }

    virtual protected Vector3 Combine ()
    {

        Vector3 finalVec = config.cohesionPriority * Cohesion() + config.wanderPriority * Wander() + config.alignmentPriority * Alignment() + config.seperationPriority * Seperation() + config.avoidancePriority * Avoidance();
        return finalVec;

    }

    void WrapAround (ref Vector3 vector, float min, float max)
    {

        vector.x = WrapAroundFloat(vector.x, min, max);
        vector.y = WrapAroundFloat(vector.y, min, max);
        vector.z = WrapAroundFloat(vector.z, min, max);

    }

    float WrapAroundFloat (float value, float min, float max)
    {

        if (value > max)
        {

            value = min;

        }
        else if (value < min)
        {

            value = max;

        }

        return value;

    }

    float RandomBinomial ()
    {

        return Random.Range(0f, 1f) - Random.Range(0f, 1f);

    }

    bool isInFOV (Vector3 vec)
    {

        return Vector3.Angle(this.velocity, vec - this.position) <= config.maxFOV;

    }

}
