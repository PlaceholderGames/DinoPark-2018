using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;

public class AI : MonoBehaviour
{
    public bool switchState = false;
    public float gameTimer;
    public int seconds = 0;

    public Animator animator;

    public StateMachine<AI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(AI_Idle.Instance);
        gameTimer = Time.time;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
        }

        if (seconds == 5)
        {
            seconds = 0;
            switchState = !switchState;
        }

        stateMachine.Update();
    }
}