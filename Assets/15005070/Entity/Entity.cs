using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for allowing easier referencing to entity stats,
/// where each stat corresponds with an element in the stats array
/// in Entity class
/// </summary>
public enum EntityStats { Hunger = 0, Tiredness = 1 }

/// <summary>
/// Base component class for all diversified species-specific AI agents.
/// Contains common functionality and variables used by all.
/// </summary>
[RequireComponent(typeof(FSM))]
public class Entity : MonoBehaviour
{
    /// <summary>
    /// Number of stats
    /// </summary>
    protected int statCount = 2;

    /// <summary>
    /// Stores stats data
    /// </summary>
    protected List<float> stats = new List<float>();

    /// <summary>
    /// Entity's genetic information. Used for both self-reference
    /// and (mainly) for supplying genetic material when
    /// </summary>
    protected DNA dna;

    /// <summary>
    /// Entity's reference to the finite-state machine. The FSM is
    /// what controls the Entity's behaviour with the stuff this class
    /// provides.
    /// </summary>
    protected FSM fsm;

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
    public virtual void derivedStart() { }

    /// <summary>
    /// Allows subclasses to pass their own Update functionality
    /// without having to override Update() belonging to Entity.
    /// Update is called once per frame.
    /// </summary>
    public virtual void derivedUpdate() { }

    /// <summary>
    /// Allows subclasses to pass their own Update functionality
    /// without having to override Update() belonging to Entity.
    /// FixedUpdate is used for physics.
    /// </summary>
    public virtual void derivedFixedUpdate() {}

    private void Start()
    {
        fsm = GetComponent<FSM>();
        derivedStart();
    }
    private void Update() { derivedUpdate(); }
    private void FixedUpdate() { derivedFixedUpdate(); }

    /// <summary>
    /// Move towards position. Moves Entity towards a set position until
    /// termination range is reached.
    /// </summary>
    /// <param name="target">Target position the Entity needs to reach.</param>
    /// <param name="terminateRange">Range in which the Entity needs to be relative to target to consider itself "arrived".</param>
    /// <param name="modifier">Additional modifier for changing translation & rotation speeds.</param>
    public bool MoveToPos(Vector3 target, float terminateRange = 0.1f, float modifier = 1f)
    {
        if (Vector3.Distance(target, transform.position) >= terminateRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, (dna.GetGene(PossibleGenes.Speed) * modifier) * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, target - transform.position, ((dna.GetGene(PossibleGenes.Speed) * modifier) * 2) * Time.deltaTime, 0.0f));
            if (anim) anim.enabled = true;
            return false;
        }
        else
        {
            if (anim) anim.enabled = false;
            return true;
        }

        //IMPLEMENT HEIGHT, WATER, AND BLOCKAGE DETECTION IN HERE
    }

    /// <summary>
    /// Move at direction. Moves Entity continously in a set
    /// direction.
    /// </summary>
    /// <param name="direction">Target rotation the Entity needs to achieve.</param>
    /// <param name="modifier">Additional modifier for changing translation & rotation speeds.</param>
    public bool MoveAtDir(Quaternion direction, float modifier = 1f)
    {
        transform.position += Vector3.forward * (dna.GetGene(PossibleGenes.Speed) * modifier) * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, (dna.GetGene(PossibleGenes.Speed) * modifier) * Time.deltaTime);
        if (anim) anim.enabled = true;
        return false;
        
        //IMPLEMENT HEIGHT, WATER, AND BLOCKAGE DETECTION IN HERE
    }

    /// <summary>
    /// Returns specific state data.
    /// </summary>
    /// <param name="stat">Specific stat to request data from.</param>
    public float GetStat(EntityStats stat) { return stats[(int)stat]; }

    /// <summary>
    /// Allows other scripts to access the Entity's DNA
    /// to evaulate or modify at will. Ideally this is 
    /// limited to future GA class (comment will need 
    /// updating in the future).
    /// </summary>
    public DNA GetDNA() { return dna; }
}