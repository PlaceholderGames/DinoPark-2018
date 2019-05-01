using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyDeathScript : MonoBehaviour {

    private Animator anim;
    private Transform raptyLoc;


	// Use this for initialization
	void Start () {
        anim.GetComponent<Animator>();
        raptyLoc.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (anim.GetBool("isDead") == true)
        {
            GetComponent<MyRapty>().enabled = false;
            GetComponent<Agent>().enabled = false;
            GetComponent<Wander>().enabled = false;
            GetComponent<Pursue>().enabled = false;
            raptyLoc.transform.Rotate(0.0f, 0.0f, 90.0f);
        }
    }
}
