using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CLAWS 3D MODEL: https://www.turbosquid.com/FullPreview/Index.cfm/ID/411026
//EXCL MARK 3D MODEL: Mariya Hristozova, 3Ds Max 2018

//script for the tiny animations for attacking and being alert
public class DinoAnimation : MonoBehaviour
{
    private Animator anim;
    public GameObject dino;
    public GameObject opponent;
    public GameObject claws;
    public GameObject exMark;
    public float distance;


    //work out the distance between one dino to another -  same function as the one in RaptyAI
    public GameObject getCurrentDino()
    {
        return dino;
    }


    // Use this for initialization
    void Start ()
    {
        //getting hold of the objects attached to the dino
        anim = GetComponent<Animator>();
        //anim.SetFloat("distance", distance);
    }
	

	// Update is called once per frame
	void Update ()
    {
        //if the dino is in an attacking state,
        //do the attacking animation
		if (anim.GetFloat("distance") < 7)
        {
            //gets the current position of the dino
            //dino = gameObject.transform.position;
            anim.SetBool("isAttacking", true);

            //Method for adding claws, both work
            claws.GetComponent<MeshRenderer>().enabled = true;
            claws.SetActive(true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
            
            //Method for removing claws, both work
            claws.gameObject.GetComponent<MeshRenderer>().enabled = false;
            claws.SetActive(false);
            
        }

        if (anim.GetFloat("distance") < 7)
        {
            //gets the current position of the dino
            //dino = gameObject.transform.position;
            anim.SetBool("isAlert", true);

            //Method for adding claws, both work
            exMark.GetComponent<MeshRenderer>().enabled = true;
            exMark.SetActive(true);
        }
        else
        {
            anim.SetBool("isAlert", false);

            //Method for removing claws, both work
            exMark.gameObject.GetComponent<MeshRenderer>().enabled = false;
            exMark.SetActive(false);

        }

    }
}