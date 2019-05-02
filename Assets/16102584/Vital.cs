using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vital
{
    public string name;
    public float value;
    public FSM.StateDelegate startState;
    public float priority = 0;

    public Vital(string aName, float aStartValue, FSM.StateDelegate aState)
    {
        name = aName;
        value = aStartValue;
        startState = aState;
    }
};
