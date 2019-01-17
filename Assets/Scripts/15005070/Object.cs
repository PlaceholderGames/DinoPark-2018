using UnityEngine;

/// <summary>
/// Base class for all entities (AI agents)
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>
    /// Reference to entities' self prefab and variables
    /// </summary>
    public GameObject[] selfPrefab;

    /// <summary>
    /// Internal reference to Entity's Animator script
    /// </summary>
    protected Animator anim;

    /// <summary>
    /// Allows subclasses to pass their own Start-requiring functionality
    /// </summary>
    public virtual void derivedStart()
    {

    }

    /// <summary>
    /// Allows subclasses to pass their own Update-requiriing functionality
    /// </summary>
    public virtual void derivedUpdate()
    {

    }

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        derivedStart();
    }

    private void Update()
    {
        derivedUpdate();
    }

    /// <summary>
    /// Universal entitiy movement function
    /// </summary>
    /// <param name="targetPos">Position to transit to</param>
    /// <param name="targetRange">Range in which transit is considered complete</param>
    /// <param name="speed">Speed of both movement and movement animation</param>
    /// <param name="modifier">A specified speed modifier for when needed</param>
    /// <returns>Returns true when entity has reached target position</returns>
    public bool MoveTo(Vector3 targetPos, float targetRange = 0.1f, float speed = 1f, float modifier = 1f)
    {
        if (Vector3.Distance(targetPos, transform.position) > targetRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, (speed * modifier) * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetPos - transform.position, (speed * 2) * Time.deltaTime, 0.0f));
            if (anim) anim.enabled = true;
            return false;
        }
        else
        {
            if (anim) anim.enabled = false;
            return true;
        }
    }
}
