// Base class for all agent classes to inherit.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For now keep inheriting from MonoBheaviour - can't attach an agent
// to an object without some MonoBehaviour reference.
public abstract class AgentBase : MonoBehaviour {

    protected int HP;

    // Make the MonoBehaviour Update a public abstract function
    // so it can be overridden in agents down the line.
    public abstract void Update();
}
