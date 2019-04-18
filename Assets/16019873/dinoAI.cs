using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinoAI : MonoBehaviour
{
    Animator animator;
    //linked to the dino
    public GameObject otherDino;
    public GameObject bite;
    public GameObject Cube;
    //work out the distance between one dino to another
    public GameObject getDino()
    {
        return otherDino;
    }

    //this function is going to instantiate a 'bite' object from a prefab
    //from the mouth of the dino (a cube)
    //and pushes it in the rotation that has been chosen and the strength of the attack
    void scratching()
    {
        GameObject b = Instantiate(bite, Cube.transform.position, Cube.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(Cube.transform.forward * 500);
    }


    //cancelling the invoke of the attaching method
    public void stopScratching()
    {
        CancelInvoke("Bite");
    }


    //this gets called every half-second
    //make the double(float) less to get it faster
    public void startScratching()
    {
        InvokeRepeating("Bite", 0.3f, 0.3f);
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
