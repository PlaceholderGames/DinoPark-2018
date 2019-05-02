using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyPack : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        var CentreOfRaptorPack = new Vector3();
        var CurrentCount = 0;

        foreach (var MyRapty in FindObjectsOfType<GameObject>())
        {
            if (MyRapty.gameObject.tag == "Rapty")
            {
                CentreOfRaptorPack = CentreOfRaptorPack + MyRapty.transform.position;
                CurrentCount++;
            }
        }

        transform.position = CentreOfRaptorPack / CurrentCount;
    }
}
