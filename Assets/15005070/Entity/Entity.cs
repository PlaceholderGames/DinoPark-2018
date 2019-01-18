using UnityEngine;


/// <summary>
/// Base class for all diversified species-specific AI agents.
/// Contains common functionality and variables used by all.
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>
    /// Entity's genetic information. Used for both self-reference
    /// and (mainly) for supplying genetic material when
    /// </summary>
    private DNA dna;

    /// <summary>
    /// Self-reference to Entity's prefab so that
    /// it can replicate itself in it's image.
    /// </summary>
    public GameObject[] selfPrefab;

    /// <summary>
    /// Self-reference to Entity's Animator script so that movement
    /// animations can be used if this is populated.
    /// </summary>
    protected Animator anim;

    /// <summary>
    /// Allows subclasses to pass their own Start functionality
    /// without having to override the Start() belonging to Entity.
    /// Start is called when first frame update.
    /// </summary>
    protected virtual void derivedStart() { }

    /// <summary>
    /// Allows subclasses to pass their own Update functionality
    /// without having to override Update() belonging to Entity.
    /// Update is called once per frame.
    /// </summary>
    protected virtual void derivedUpdate() { }

    /// <summary>
    /// Allows subclasses to pass their own Update functionality
    /// without having to override Update() belonging to Entity.
    /// FixedUpdate is used for physics.
    /// </summary>
    protected virtual void derivedFixedUpdate() {}

    private void Start() { derivedStart(); }
    private void Update() { derivedUpdate(); }
    private void FixedUpdate() { derivedFixedUpdate(); }

    /// <summary>
    /// Universal Entity movement function for making sure
    /// all Entities move in a similar manner with given parametre set.
    /// </summary>
    /// <param name="targetPos">Position to transit to.</param>
    /// <param name="targetRange">Range in which transit is considered complete.</param>
    /// <param name="speed">Speed of both movement and movement animation<./param>
    /// <param name="modifier">A specified speed modifier for when needed.</param>
    /// <returns>Returns true when entity has reached target position.</returns>
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