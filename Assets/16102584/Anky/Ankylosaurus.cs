using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ankylosaurus : Dinosaur {

    private CapsuleCollider collider;

    protected override void Awake()
    {
        base.Awake();
        group = GameObject.Find("Herd").gameObject;
        group.AddComponent<Agent>();
        collider = gameObject.transform.Find("Trigger").GetComponent<CapsuleCollider>();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        checkForPredators();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rapty")
        {
            collision.gameObject.GetComponent<Raptor>().attackMe();
            health -= 20;
        }
    }

    public float getHealth() { return health; }

    private void checkForPredators()
    {
        if(brain.getCurrentState() != fleeEnemy)
        {
            foreach (Transform dino in FOV.visibleTargets)
            {
                if (dino.tag == "Rapty")
                {
                    StartCoroutine(flee());
                }
            }
        }
    }
}
