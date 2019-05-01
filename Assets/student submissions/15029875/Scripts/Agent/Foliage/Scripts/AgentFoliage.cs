using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// The Bison agent is special in that it has the capacity to herd
// as well as idle.
public class AgentFoliage : AgentBase
{
    float calories;

    // Constructor.
    private void Start()
    {
        float initialSize = Random.Range(0.0f, 1.4f);
        calories = Random.Range(30, 40) * initialSize; // Multiply by initial size: this gives juicier plants a better start.
        this.gameObject.transform.localScale += new Vector3(initialSize, initialSize, initialSize); // Scale the objects initially.
    }

    public override void Update()
    {
        Horology();
    }

    public override void Horology()
    {
        // Increase in calories.
        if (calories <= 200)
        {
            calories += Time.deltaTime;
        }
    }
}