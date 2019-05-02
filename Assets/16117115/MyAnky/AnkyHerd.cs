using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyHerd : MonoBehaviour  // This script is used to give the Anky's a herding characteristic
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        var CentreOfHerdingCircle = new Vector3(); // Updates the centre of the herding circle by creating a new vector 3
        var CurrentCount = 0;

        foreach (var MyAnky in FindObjectsOfType<GameObject>())   // If MyAnky is found within the game objects with the tag Anky then transform the position of the herding circle
        {
            if (MyAnky.gameObject.tag == "Anky")
            {
                CentreOfHerdingCircle = CentreOfHerdingCircle + MyAnky.transform.position;
                CurrentCount++;
            }
        }

        transform.position = CentreOfHerdingCircle / CurrentCount;   // Decides the movement by dividing the centre by the current count
    }
}
