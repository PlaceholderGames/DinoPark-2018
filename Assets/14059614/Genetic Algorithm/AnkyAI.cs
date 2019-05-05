using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;

public class AnkyAI : DinoAI {


    public StateMachine<AnkyAI> stateMachine { get; set; }
    // Use this for initialization
    void Start () {
        Statistics(2);
        stateMachine = new StateMachine<AnkyAI>(this);
        stateMachine.ChangeState(AnkyIdle.Instance);
    }
	
	// Update is called once per frame
	void Update () {
        Timer();
        stateMachine.Update();
	}

}
