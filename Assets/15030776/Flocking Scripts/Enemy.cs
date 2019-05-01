using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Member
{

    override protected Vector3 Combine()
    {

        return config.wanderPriority * Wander();

    }

}
