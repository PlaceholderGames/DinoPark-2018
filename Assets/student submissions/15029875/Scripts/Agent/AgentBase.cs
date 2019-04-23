// Base class for all agent classes to inherit.
using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// For now keep inheriting from MonoBheaviour - can't attach an agent
// to an object without some MonoBehaviour reference.
public abstract class AgentBase : MonoBehaviour {
    // For the time being make these placeholders. Could instantiate them here but
    // some agents may have a greater base speed or health than other agents.
    public int health, speed, hunder, thirst;

    // Make the MonoBehaviour Update a public abstract function
    // so it can be overridden in agents down the line.
    public abstract void Update();
}
