using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : Member
{

    override protected Vector3 Combine()
    {

        return config.wanderPriority * Wander();

    }

}
