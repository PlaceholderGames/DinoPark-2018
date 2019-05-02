using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyAnky : Dinosaur
{
    //public enum ankyState
    //{
    //    IDLE,       // The default state on creation.
    //    EATING,     // This is for eating depending on y value of the object to denote grass level
    //    DRINKING,   // This is for Drinking, depending on y value of the object to denote water level
    //    ALERTED,      // This is for hightened awareness, such as looking around
    //    GRAZING,    // Moving with the intent to find food (will happen after a random period)
    //    ATTACKING,  // Causing damage to a specific target
    //    FLEEING,     // Running away from a specific target
    //    DEAD
    //};

    // Use this for initialization
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        // Assert default animation booleans and floats
        anim.SetBool("isIdle", true);
        anim.SetBool("isEating", false);
        anim.SetBool("isDrinking", false);
        anim.SetBool("isAlerted", false);
        anim.SetBool("isGrazing", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isFleeing", false);
        anim.SetBool("isDead", false);
        anim.SetFloat("speedMod", 1.0f);
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller
        base.Start();
    }

    protected override void Update()
    {
        // Idle - should only be used at startup

        // Eating - requires a box collision with a dead dino

        // Drinking - requires y value to be below 32 (?)

        // Alerted - up to the student what you do here

        // Hunting - up to the student what you do here

        // Fleeing - up to the student what you do here

        // Dead - If the animal is being eaten, reduce its 'health' until it is consumed

        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    protected override void seekFood()
    {
        //to seek food, the search will attempt to find grass
        //weight of each node is its y value

        if (optimalRoute.Count == 0)
        {
            //starting node is added to the queue (the node nearest to the dino)
            startPos = findNearestMapNode(transform.position);
            currentPos = startPos;

            openQueue.Add(findNearestMapNode(currentPos));
            float gCost = Mathf.Abs(currentPos.y - startPos.y);
            float hCost = Mathf.Abs(currentPos.y - waterLevel);
            mapData.setGCost(currentPos, gCost);
            mapData.setHCost(currentPos, hCost);
            mapData.setParentPos(currentPos, Vector3.zero);

            do
            {
                if (openQueue.Count == 0)
                    break;

                currentPos = openQueue[0];

                //searches for the 4 nearest nodes
                for (int i = 0; i < 4; i++)
                {
                    //stores the new position that we're looking at
                    Vector3 tmpPos = getNewNodePos(currentPos, i);

                    if (tmpPos.x > 0 && tmpPos.x < (mapData.returnTerrainSize().x) && tmpPos.z > 0 && tmpPos.z < (mapData.returnTerrainSize().z))
                    {

                        //check whether the node is already in the closed list
                        if (!closedQueue.Contains(tmpPos))
                        {
                            float newGCost = Mathf.Abs(tmpPos.y - startPos.y);
                            float newHCost = Mathf.Abs(tmpPos.y - waterLevel);
                            float movementCost = newGCost + Mathf.Abs(tmpPos.y - currentPos.y) + newHCost;

                            if (movementCost < newGCost || !openQueue.Contains(tmpPos))
                            {
                                if (!openQueue.Contains(tmpPos))
                                {
                                    openQueue.Add(tmpPos);
                                    mapData.setGCost(tmpPos, movementCost);
                                    mapData.setHCost(tmpPos, newHCost);
                                    mapData.setParentPos(tmpPos, currentPos);
                                }
                                else
                                {
                                    mapData.setParentPos(tmpPos, currentPos);
                                    mapData.setGCost(tmpPos, movementCost);
                                }
                            }
                        }
                    }
                }

                //openQueue.Sort((a, b) => mapData.getGHCost(a).CompareTo(mapData.getGHCost(b)));
                //openQueue = openQueue.OrderBy(e => e.y).ToList();


                closedQueue.Add(openQueue[0]);
                openQueue.RemoveAt(0);

            } while (openQueue.Count > 0 && currentPos.y > waterLevel);
            optimalRoute.Add(currentPos);

            //mapData.setParentPos(openQueue[0], currentPos);
            closedQueue.Add(openQueue[0]);

            //Vector3 newCurPos = currentPos;

            while (currentPos != startPos)
            {
                optimalRoute.Add(currentPos);

                int i = 0;
                for (i = 0; i < closedQueue.Count; i++)
                    if (closedQueue[i] == currentPos)
                        break;

                currentPos = mapData.getParentPos(closedQueue[i]);
            }

            openQueue.Clear();
            closedQueue.Clear();
        }

        //generate the optimal route for the dinosaur to move
        if (optimalRoute.Count > 0)
        {
            targetObj.transform.position = optimalRoute[optimalRoute.Count - 1];

            float tolerance = 3.0f;
            if (Mathf.Abs(Mathf.Round(transform.position.x) - targetObj.transform.position.x) < tolerance)
            {
                if (Mathf.Abs(Mathf.Round(transform.position.z) - targetObj.transform.position.z) < tolerance)
                {
                    //mapData.resetMapNode(optimalRoute);

                    optimalRoute.RemoveAt(optimalRoute.Count - 1);
                    //optimalRoute.Clear();
                }
            }
        }

        //float levelTolerance = 1.0f;
        //if (transform.position.y <= waterLevel || Mathf.Abs(transform.position.y - waterLevel) < levelTolerance)
        //{
        //    currentState = dinoState.DRINKING;
        //    stateChangeOccured = true;
        //}
    }

    protected override void eat()
    {

    }
}
