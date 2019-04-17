using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinoAI : MonoBehaviour
{
    Animator animator;
    //linked to the dino
    public GameObject otherDino;
    //work out the distance between one dino to another
    public GameObject getPlayer()
    {
        return otherDino;
    }

    //use this for initialisation
    private void Start()
    {
        //setting it to get data from the FSM created in Unity Animator
        animator = GetComponent<Animator>();
    }

    //update is called once per frame
    private void Update()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, otherDino.transform.position));
    }

}
