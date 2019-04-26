using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for the tiny animations for attacking and being alert
public class DinoAnimation : MonoBehaviour
{
    private Animator anim;
    public GameObject dino;
    public GameObject opponent;
    public float distance;

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
            anim.SetBool("isAttacking", true);
        }
	}
}