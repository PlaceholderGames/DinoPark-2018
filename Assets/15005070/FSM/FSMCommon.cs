using UnityEngine;
/// <summary>
/// Contains variables that are common & universally needed by all
/// States. The use of this will prevent the need for reinitializing
/// the same variables each time a State object is initalized. 
/// </summary>
public class FSMCommon
{
    /// <summary>
    /// Reference to State's parent Entity's name. Used for self-identification.
    /// </summary>
    public string name;

    /// <summary>
    /// Flags whether the State should be debugging mode. If this is enabled,
    /// the State should log when it starts and what major actions it has completed.
    /// </summary>
    public bool debugging;

    /// <summary>
    /// Reference to State's parent FSM. Access to this will be needed
    /// for changing the FSM's current state during Transition().
    /// </summary>
    public FSM fsm;

    /// <summary>
    /// Reference to FSM's parent Entity. Access to this will be needed
    /// for when States need to interface with the parent Entity component
    /// for getting stats, DNA, position/rotation, etc.
    /// by this state.
    /// </summary>
    public Entity parent;

    /// <summary>
    /// Default FSMCommon constructor.
    /// </summary>
    public FSMCommon() { }

    /// <summary>
    /// Modified FSMCommon constructor. Allows initialisation of most FSMCommon
    /// variables in one go.
    /// </summary>
    /// <param name="nName"></param>
    /// <param name="nDebugging"></param>
    /// <param name="nFSM"></param>
    /// <param name="nParent"></param>
    public FSMCommon(string nName, bool nDebugging, FSM nFSM, Entity nParent)
    {
        name = nName;
        debugging = nDebugging;
        fsm = nFSM;
        parent = nParent;
    }

    /// <summary>
    /// Reference to Sate's target Entity. Making this universal might be helpful
    /// for other states knowing what the last State's focus is. For example, both
    /// Attacking and Eating States will need to know the same target.
    /// know about.
    /// </summary>
    public Entity target;
}