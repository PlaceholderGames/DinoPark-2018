using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

    [SerializeField]
    Transform camera;

    private void Awake()
    {
        camera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>().transform;
    }

    // Update is called once per frame
    void Update () {
        Vector3 v = camera.transform.position - transform.position;
        v.x = 0;
        v.z = 0;
        transform.LookAt(camera.position - v);
	}
}
