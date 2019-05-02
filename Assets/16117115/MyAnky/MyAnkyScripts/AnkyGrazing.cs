using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteSM;

public class AnkyGrazing : State<MyAnky>    // This state is for the Anky to graze
{
    private static AnkyGrazing _ankyInstance;  // Graze instance

    private AnkyGrazing()
    {
        if (_ankyInstance != null)
            return;

        _ankyInstance = this;
    }

    public static AnkyGrazing AnkyInstance  // Graze instance
    {
        get
        {
            if (_ankyInstance == null)
                new AnkyGrazing();

            return _ankyInstance;
        }
    }

    public override void EnterState(MyAnky CurrentParentOf)   // Enters grazing state
    {
        CurrentParentOf.Animator.SetBool("isGrazing", true);
    }

    public override void UpdateState(MyAnky CurrentParentOf)  // Updates grazing state
    {
        if (CurrentParentOf.AnkyPredators.Count > 0)
        {
            CurrentParentOf.AStar.enabled = false;
            CurrentParentOf.AS.enabled = false;
            CurrentParentOf.ASAgent.enabled = false;
            CurrentParentOf.AStar.target = null;
            CurrentParentOf.Wandering.enabled = false;
            CurrentParentOf.Wandering.target = null;
            CurrentParentOf.State.ChangeState(AnkyAlerted.AnkyInstance);   //If predator count is greater than 0 then change all these script parameters
        }
        else if (CurrentParentOf.Anky_Thirst < 65.0f)        // If thirst less than 65.0 then search for water tiles to drink
        {
            if (CurrentParentOf.transform.position.y > 35.0f)
            {
                if (CurrentParentOf.AS.path.nodes.Count == 0)
                {
                    CurrentParentOf.AStar.enabled = true;
                    CurrentParentOf.AS.enabled = true;
                    CurrentParentOf.ASAgent.enabled = true;

                    List<Vector2> BodyOfWater = new List<Vector2>();

                    for (var XAxis = 0; XAxis < 2000; XAxis = XAxis + 25)
                    {
                        for (var ZAxis = 0; ZAxis < 2000; ZAxis = ZAxis + 25)
                        {
                            if (CurrentParentOf.Terrain.GetComponent<Terrain>().SampleHeight(new Vector3(ZAxis, 0, XAxis)) < 35.0f)    // Grabs the coordinates and position of the water tiles and adds new vector
                                BodyOfWater.Add(new Vector2(ZAxis, XAxis));
                        }
                    }

                    Vector2 closestWater = new Vector2();    // Returns closest water

                    foreach (var PatchOfWater in BodyOfWater)    // For each water tile within vector, transform closest water to the water tiles
                    {
                        var DistanceFromWater = Vector2.Distance(new Vector2(CurrentParentOf.transform.position.x, CurrentParentOf.transform.position.z), PatchOfWater);

                        if (closestWater == new Vector2(1000, 1000))
                            closestWater = PatchOfWater;
                        else if (DistanceFromWater < Vector2.Distance(
                                     new Vector2(CurrentParentOf.transform.position.x, CurrentParentOf.transform.position.z),
                                     closestWater))
                            closestWater = PatchOfWater;
                    }

                    CurrentParentOf.AStar.mapGrid.seaLevel = 20.0f;
                    CurrentParentOf.AStar.target = Object.Instantiate(new GameObject(),
                        new Vector3(closestWater.x, 30.0f, closestWater.y), Quaternion.identity);    // Output the sea level and grab the identity of the closest water and water tiles using AS
                    CurrentParentOf.AS.path = CurrentParentOf.AStar.path;
                }
                else
                {
                    CurrentParentOf.MoveAnky(CurrentParentOf.AS.getDirectionVector());    // Move Anky towards water
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
                CurrentParentOf.State.ChangeState(AnkyDrinking.AnkyInstance);   // Else switch all these script permissions and change back to Anky drinking
            }
        }
        else if (CurrentParentOf.Anky_Hunger < 55.0f)  // If Anky hunger goes below 55.0 then shoot a raycast to look for grass to eat
        {
            RaycastHit RaycastOutput;
            var rayLength = 2.0f;

            var mouth = CurrentParentOf.transform
                .Find("Armature/Chest/Neck/HeadRig/DownMouthStart/DownMouthRig/DownMouthRig_end").position;   // Makes the mouth move when eating

            Debug.DrawRay(mouth, -CurrentParentOf.transform.up * rayLength, Color.red);   // Draws the raycast

            if (Physics.Raycast(mouth, -CurrentParentOf.transform.up, out RaycastOutput, rayLength))
            {
                if (CurrentParentOf.Terrain.GetComponent<Grass>().Details[(int)RaycastOutput.point.z, (int)RaycastOutput.point.x] != 0) // This shoots the raycast and then grabs the Grass component and returns the details of the Grass component
                {
                    CurrentParentOf.Wandering.enabled = false;
                    CurrentParentOf.Wandering.target = null;
                    CurrentParentOf.State.ChangeState(AnkyEating.AnkyInstance);   // When Grass component has been grabbed change state to Anky eating
                }
            }
        }
        else if (Vector3.Distance(CurrentParentOf.transform.position, CurrentParentOf.AnkyHerd.transform.position) > 40.0f &&
                 CurrentParentOf.AS.path.nodes.Count == 0)    // If herd position is greater than 40.0 and node count is 0 then enable wandering
        {
            CurrentParentOf.Wandering.enabled = false;

            if (!CurrentParentOf.AStar.enabled)    // If AStar is disabled then enable AStar, AS and SearchAgent 
            {
                CurrentParentOf.AStar.enabled = true;
                CurrentParentOf.AS.enabled = true;
                CurrentParentOf.ASAgent.enabled = true;

                CurrentParentOf.AStar.target = CurrentParentOf.AnkyHerd;    // Set AStar target to Anky herd

                CurrentParentOf.AS.path = CurrentParentOf.AStar.path;  // Equals AS path to AStar path
            }
        }
        else if (Vector3.Distance(CurrentParentOf.transform.position, CurrentParentOf.AnkyHerd.transform.position) > 15.0f &&
                 CurrentParentOf.AS.path.nodes.Count > 0)   // If herd is greater than 15.0 and has a node greater than 0 then move Anky using direction vector
        {
            CurrentParentOf.MoveAnky(CurrentParentOf.AS.getDirectionVector());
        }
        else
        {
            if (CurrentParentOf.AStar.enabled)   // If AStar enabled then disable other scripts
            {
                CurrentParentOf.AStar.enabled = false;
                CurrentParentOf.AS.enabled = false;
                CurrentParentOf.ASAgent.enabled = false;
                CurrentParentOf.AStar.target = null;
                CurrentParentOf.AS.path = null;
            }
            else
            {
                if (!CurrentParentOf.Wandering.enabled)
                    CurrentParentOf.Wandering.enabled = true;   // If not wandering, enable wandering
            }
        }
    }

    public override void ExitState(MyAnky CurrentParentOf)   // Exits grazing state
    {
        CurrentParentOf.Animator.SetBool("isGrazing", false);
    }
}