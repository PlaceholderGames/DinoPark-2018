using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script will be attahed to all dinos so that if they are eaten or killed
//this script will be enabled
public class Dead : MonoBehaviour
{
    //creating a variable to be able to access the conditions set in the Unity Animator Controller
    Animator animator;
    //linked to the dino
    public GameObject thisDino;
    public Dead deadD;


    // Use this for initialization
    void Start ()
    {
        deadD = thisDino.GetComponent<Dead>();
    }


    //work out the distance between one dino to another
    public GameObject dieDino()
    {
        //https://learn.unity.com/tutorial/destroy-i?projectId=5c8920b4edbc2a113b6bc26a#5c8a6146edbc2a001f47d5c6 - to destroy within 3 sec
        //will stop rendering which means the thing will be removed from the scene only in runtime
        //not from the list of objects
        if (animator.GetBool("deadDino") == true)
        {
            Destroy(GetComponent<MeshRenderer>(), 3f);
            animator.SetBool("deadDino", true);
        }
        return thisDino;
    }


    // Update is called once per frame
    void Update ()
    {
		if(deadD.enabled == true)
        {
            dieDino();
            //transform location to "lay down"
        }
	}
}