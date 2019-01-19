/// <summary>
/// Base class for all states. Contains the basic
/// mechanics required by FSM to operate properly with
/// integrity.
/// </summary>
public class State
{
    /// <summary>
    /// Internal reference to the Entity's finite-state machine.
    /// </summary>
    protected FSM fsm;

    /// <summary>
    /// Required concluding state that this state will end up as.
    /// </summary>
    protected State endState;

    /// <summary>
    /// Internal reference to the parent Entity whose's being runned
    /// by this state.
    /// </summary>
    protected Entity parent;

    /// <summary>
    /// Internal reference to any target Entity that this state should
    /// know about.
    /// </summary>
    protected Entity target;

    /// <summary>
    /// Default state constructor.
    /// </summary>
    /// <param name="nFSM">Reference to parent FSM component.</param>
    /// <param name="nParent">Reference to parent Entity component.</param>
    public State(FSM nFSM, Entity nParent)
    {
        fsm = nFSM;
        parent = nParent;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Start() { }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Transition() { if (endState != null) fsm.ChangeState(endState); }
}