// State base class.
// All states inherit this class and methods will be overridden from here.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// As this is a base class for inherited states
// it's by best design to make this an abstract class
// allowing us to initialise functions in their own inherited classes.
public abstract class StateBase {

    public abstract void BeginS(StateMachine SM);
    public abstract void UpdateS(StateMachine SM);
    public abstract void EndS(StateMachine SM);
}
