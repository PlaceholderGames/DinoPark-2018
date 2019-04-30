using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public Agent agent;

    protected void start()
    {
        agent = GetComponent<Agent>();

    }
	
}
