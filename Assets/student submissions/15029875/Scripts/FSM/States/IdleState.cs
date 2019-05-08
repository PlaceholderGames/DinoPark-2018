using System.Collections;
using System.Collections.Generic;
using StateMachineInternals;
using UnityEngine;

// Class called when the agent is idling. This may need to be inherited from to
// call behaviour for different agents, but this basic state
// is essentially going to wander in different directions.
public class IdleState : IState
{
    AgentBase agent;

    float rotationSpeed = 100.0f;

    private bool isWandering, isRotatingLeft, isRotatingRight, isWalking = false;

    public IdleState(AgentBase parsedAgent)
    {
        agent = parsedAgent;
    }

    public void BeginState()
    {
    }

    public void EndState()
    {
    }

    public void UpdateState()
    {
        if (isWandering == false)
        {
            agent.StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            agent.transform.Rotate(agent.transform.up * Time.deltaTime * rotationSpeed);
        }
        if (isRotatingLeft == true)
        {
            agent.transform.Rotate(agent.transform.up * Time.deltaTime * -rotationSpeed);
        }
        if (isWalking == true)
        {
            agent.transform.position += agent.transform.forward * agent.speed * Time.deltaTime;
        }

        
        if(agent.hunger >= 45 && agent.hunger <= 100 && agent.hungry == false)
        {
            Debug.Log(agent.name + "Hunger is true.");
            agent.hungry = true;
            agent.stateMachine.SwitchState(new HungerState(agent, agent.FOV.visibleTargets));
        }
    }

    // Enumerator to essentially feed booleans back to the coroutine.
    // So if any of these conditions are true then the agent decides
    // whether it moves left, right and at what speeds.
    // This is finnicky - a lot of the variables below need fine-tuning.
    // It might also be beneficial in the future to make the base agent class
    // handle some of these - for example, does a velociraptor rotate faster than an anky?
    IEnumerator Wander()
    {
        int rotationTime = Random.Range(1, 3); // Speed it takes to do a full rotation.
        int rotateWait = Random.Range(1, 4); // How long will the agent wait before rotating?
        int rotateLeftRight = Random.Range(1, 2); // Nifty way of picking left or right, right?
        int walkWait = Random.Range(1, 4); // How long will the agent wait before walking again?
        int walkTime = Random.Range(1, 13); // How long will we be walking?

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);

        if(rotateLeftRight == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }
        else
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }

        isWandering = false;
    }
    
}