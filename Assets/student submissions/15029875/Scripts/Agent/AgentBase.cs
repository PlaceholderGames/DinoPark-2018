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
    // For now, due to the Horology() function using time.deltaTime, these variables have to be floats.
    // Will see if this presents a problem in the future.
    public float health, speed, hunger, thirst;
    public bool hungry, dead;

    public StateMachine stateMachine;
    public FieldOfView FOV;

    // Make the MonoBehaviour Update a public abstract function
    // so it can be overridden in agents down the line.
    public abstract void Update();
    public abstract void OnTriggerEnter(Collider other);
    public abstract void Horology(); // Govern the laws of time. (Essentially what happens per tick.)
}
