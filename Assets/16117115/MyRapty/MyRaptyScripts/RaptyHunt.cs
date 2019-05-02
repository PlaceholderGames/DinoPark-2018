using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class RaptyHunt : State<MyRapty>  // This is a sate for the Rapty hunting
{
    private static RaptyHunt _raptyInstance;  // Hunting Instance

    private RaptyHunt()
    {
        if (_raptyInstance != null)
            return;

        _raptyInstance = this;
    }

    public static RaptyHunt RaptyInstance   // Hunting Instance
    {
        get
        {
            if (_raptyInstance == null)
                new RaptyHunt();

            return _raptyInstance;
        }
    }

    public override void EnterState(MyRapty CurrentParentOf)  // Enters hunting state
    {
        CurrentParentOf.Animator.SetBool("isHunting", true);
    }

    public override void UpdateState(MyRapty CurrentParentOf)   // Updates hunting state
    {
        if (CurrentParentOf.NearestDeadAnky.Count > 0)
        {
            CurrentParentOf.Wandering.enabled = false;
            CurrentParentOf.Wandering.target = null;

            if (Vector3.Distance(CurrentParentOf.transform.position, CurrentParentOf.RetrieveNearestDeadAnky().transform.position) < 10.0f)
            {
                CurrentParentOf.GoTo.target = null;
                CurrentParentOf.GoTo.enabled = false;
                CurrentParentOf.State.ChangeState(RaptyEating.RaptyInstance);   // If Anky is less than 10.0 then go to Anky and eat
            }
            else
            {
                if (!CurrentParentOf.GoTo.enabled)
                {
                    CurrentParentOf.GoTo.target = CurrentParentOf.RetrieveNearestDeadAnky().gameObject;   // If GoTo not enabled then enable and track nearest Anky
                    CurrentParentOf.GoTo.enabled = true;
                }
            }
        }
        else if (CurrentParentOf.NearestLiveAnky.Count > 0)   // If living dino is greater than 0 then enable wandering and switch state to Rapty alert
        {
            CurrentParentOf.Wandering.enabled = false;
            CurrentParentOf.Wandering.target = null;
            CurrentParentOf.State.ChangeState(RaptyAlerted.RaptyInstance);
        }
        else if (CurrentParentOf.Rapty_Thirst < 50.0f)   // If thirst less than 65.0 then enable AStar
        {
            if (CurrentParentOf.transform.position.y > 35.0f)
            {
                if (CurrentParentOf.AS.path.nodes.Count == 0)
                {
                    CurrentParentOf.AStar.enabled = true;
                    CurrentParentOf.AS.enabled = true;
                    CurrentParentOf.ASAgent.enabled = true;

                    List<Vector2> NearestWater = new List<Vector2>();

                    for (var XAxis = 0; XAxis < 2000; XAxis = XAxis + 25)
                    {
                        for (var ZAxis = 0; ZAxis < 2000; ZAxis = ZAxis + 25)
                        {
                            if (CurrentParentOf.Terrain.GetComponent<Terrain>().SampleHeight(new Vector3(ZAxis, 0, XAxis)) < 35.0f)   // Collects water tiles for the Rapty to drink
                                NearestWater.Add(new Vector2(ZAxis, XAxis));
                        }
                    }

                    Vector2 closestWater = new Vector2();

                    foreach (var ClosestWater in NearestWater)
                    {
                        var DistanceFromWater =
                            Vector2.Distance(new Vector2(CurrentParentOf.transform.position.x, CurrentParentOf.transform.position.z),   // Finds the closes water tiles and sends them into the vector
                                ClosestWater);

                        if (closestWater == new Vector2(1000, 1000))
                            closestWater = ClosestWater;
                        else if (DistanceFromWater < Vector2.Distance(
                                     new Vector2(CurrentParentOf.transform.position.x, CurrentParentOf.transform.position.z),   // Equals the closest tiles to the water tiles so Rapty can access them
                                     closestWater))
                            closestWater = ClosestWater;
                    }

                    CurrentParentOf.AStar.mapGrid.seaLevel = 35.0f;
                    CurrentParentOf.AStar.target = Object.Instantiate(new GameObject(),
                        new Vector3(closestWater.x, 30.0f, closestWater.y), Quaternion.identity);   // New vector with the closest water and sets the AS path to the AStar path
                    CurrentParentOf.AS.path = CurrentParentOf.AStar.path;
                }
                else
                {
                    CurrentParentOf.MoveRapty(CurrentParentOf.AS.getDirectionVector());   // Moves Rapty to closest water
                }
            }
            else
            {
                CurrentParentOf.AStar.enabled = false;
                CurrentParentOf.AS.enabled = false;
                CurrentParentOf.ASAgent.enabled = false;
                CurrentParentOf.AStar.target = null;
                CurrentParentOf.AS.path = null;
                CurrentParentOf.Wandering.enabled = false;
                CurrentParentOf.Wandering.target = null;
                CurrentParentOf.State.ChangeState(RaptyDrinking.RaptyInstance);    // When reaches water disables all above scripts and switches state to Rapty drinking
            }
        }
        else
        {
            if (!CurrentParentOf.Wandering.enabled)
                CurrentParentOf.Wandering.enabled = true;    // If wandering not enabled, enable it
        }
    }

    public override void ExitState(MyRapty CurrentParentOf)   // Exits hunting state
    {
        CurrentParentOf.Animator.SetBool("isHunting", false);
    }
}