using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour {

    public delegate void StateDelegate();
    private Stack<StateDelegate> stack;

    private void Start() {
        if (stack == null) { stack = new Stack<StateDelegate>(); }
    }

    void Update () {
        StateDelegate currentFunction = getCurrentState();
        if(currentFunction != null) { currentFunction(); }
    }

    public StateDelegate popState() { return stack.Pop(); }

    public void pushState (StateDelegate newState) {
        if(getCurrentState() != newState) { stack.Push(newState); }
    }

    public StateDelegate getCurrentState() {
        if (stack == null) { stack = new Stack<StateDelegate>(); }
        if (stack.Count > 0) { return stack.Peek(); }   
        return null;
    }

    public int Count () { return stack.Count; }
}