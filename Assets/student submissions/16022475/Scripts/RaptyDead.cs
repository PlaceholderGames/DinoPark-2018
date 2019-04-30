using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyDead : MonoBehaviour
{ 
    public float food;
// Start is called before the first frame update
void Start()
{
    food = 500;
}

// Update is called once per frame
void Update()
{
    food -= 0.1f;
    if (food <= 0)
    {
        Destroy(gameObject);
    }
}

}
