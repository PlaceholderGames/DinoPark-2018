using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

//this class is for the rapty hunting state
public class RaptyHunting : State<MyRapty>
{
    //hunting instance
    private static RaptyHunting rapty_instance;

    private RaptyHunting()
    {
        if (rapty_instance != null)
            return;

        rapty_instance = this;
    }

    public static RaptyHunting RaptyInstance
    {
        get
        {
            if (rapty_instance == null)
                new RaptyHunting();
            return rapty_instance;
        }
    }

    public override void OnStateEnter(MyRapty parent)
    {
        parent.anim.SetBool("isHunting", true);
    }

    public override void OnStateExit(MyRapty parent)
    {
        parent.anim.SetBool("isHunting", false);
    }

    // Update is called once per frame
    public override void OnStateUpdate(MyRapty parent)
    {
        // if dead dinosaurs are more than zero
        if (parent.DeadDinoMeat.Count > 0)
        {
            //then set to null/false
            parent.wandering.target = null;
            parent.wandering.enabled = false;

            //if the raptor is in distance of meat then it will change its position
            if (Vector3.Distance(parent.transform.position, parent.getDinoMeat().transform.position) < 15.0f)
            {
                parent.arriving.enabled = false;
                parent.arriving.target = null;
                //and enter hunger state and eat
                parent.State.Change(RaptyHunger.RaptyInstance);
            }
            else
            {
                //if dino isnt arriving then set to true and get the target dino meat
                if (!parent.arriving.enabled)
                {
                    parent.arriving.target = parent.getDinoMeat().gameObject;
                    parent.arriving.enabled = true;
                }
                
            }
        }
        //if the food count is over zero then set the state to alert
        else if (parent.Food.Count > 0)
        {
            parent.wandering.target = null;
            parent.wandering.enabled = false;
            //enter alert state
            parent.State.Change(RaptyIsAlert.RaptyInstance);
        }
        //if the raptors water gets below 55 then he will try and find water
        //the path finders are used here to find the nearest water available to the raptor
        else if (parent.rapty_water < 55.0f)
        {
            if (parent.transform.position.y > 35.0f)
            {
                if (parent.following.path.nodes.Count == 0)
                {
                    parent.aStar.enabled = true;
                    parent.following.enabled = true;
                    parent.agent.enabled = true;              

                    //list created drinking water available
                    List<Vector2> drinkingWater = new List<Vector2>();
                    Vector2 nearestWater = new Vector2();

                    var gridWorldSize = 2000;
                    var tileSize = 25;
                    var position = 1000;

                    //x and y coordinates for 
                    for (var x = 0; x < gridWorldSize; x = x + tileSize)
                    {
                        for (var y = 0; y < gridWorldSize; y = y + tileSize)
                        {
                            if (parent.dinoPark.GetComponent<Terrain>().SampleHeight(new Vector3(x, 0, y)) < 35.0f)
                                drinkingWater.Add(new Vector2(x, y));
                        }
                    }

                    foreach (var water in drinkingWater)
                    {
                        var distance_from_water = Vector2.Distance(new Vector2(parent.transform.position.x, parent.transform.position.z), water);

                        if (nearestWater == new Vector2(position, position))
                            nearestWater = water;
                        else if (distance_from_water < Vector2.Distance(new Vector2(parent.transform.position.x, parent.transform.position.z), nearestWater))
                            nearestWater = water;
                    }
                    parent.aStar.target = Object.Instantiate(new GameObject(), new Vector3(nearestWater.x, 30.0f, nearestWater.y), Quaternion.identity);
                    parent.following.path = parent.aStar.path;
                    parent.aStar.mapGrid.seaLevel = 20.0f;
                }
                else
                {
                    parent.RaptyMovement(parent.following.getDirectionVector());
                }
            }
            else
            {
                //turning the scripts off/null and changing raptors state
                //to thirsty
                parent.aStar.enabled = false;
                parent.aStar.target = null;
                parent.following.enabled = false;
                parent.following.path = null;
                parent.wandering.enabled = false;
                parent.wandering.target = null;
                parent.agent.enabled = false;
                parent.State.Change(RaptyThirsty.RaptyInstance);
            }
        }
        //if wandering isnt enabled then turn it on
        else
        {
            if (!parent.wandering.enabled)
                parent.wandering.enabled = true;
        }
    }
}
