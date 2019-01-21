/// <summary>
/// Base class for all states. Contains the basic mechanics required
/// by FSM to operate properly with integrity.
/// </summary>
public class FSMState
{
    /// <summary>
    /// State's reference to the FSM's common variables class that allows for
    /// universal variables to be accessed by different states without the need
    /// for reinitalization/re-setting for each State class instance.
    /// </summary>
    protected FSMCommon com;

    /// <summary>
    /// Required concluding state that this state will end up as. This will be
    /// initialised in Start().
    /// </summary>
    protected FSMState endState;

    /// <summary>
    /// Default state constructor.
    /// </summary>
    /// <param name="nCOM">Reference to FSMCommon obj belonging to parent FSM.</param>
    public FSMState(FSMCommon nCOM) { com = nCOM; }

    /// <summary>
    /// The State's start-up tasks that are only used once. Virtual allows
    /// subclasses to override this with their own version.
    /// </summary>
    public virtual void Start() { }

    /// <summary>
    /// The State's per-frame update tasks. Virtual allows subclasses to override
    /// this with their own version.
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// The State's transition-to-another-state tasks. Virtual allows subclasses to 
    /// override this with their own version, although there should be no need to.
    /// </summary>
    public virtual void Transition() { if (endState != null) com.fsm.ChangeState(endState); }
}