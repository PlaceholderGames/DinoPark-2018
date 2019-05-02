using System.Collections;
using UnityEngine;

public class Raptor : Dinosaur
{
    protected override void Awake()
    {
        base.Awake();
        group = GameObject.Find("Pack").gameObject;
        group.AddComponent<Agent>();
    }

    protected override void searchForFood()
    {
        wander.enabled = true;
        if (priorities.Find(delegate (Vital v) { return v.name == "Hunger"; }).priority > CONCERN_THRESHOLD && GameObject.Find("Herd").transform.childCount > 0)
        {
            SetBillboardState(1);
            float minDist = 200;
            bool updated = false;
            foreach (Transform dino in FOV.visibleTargets)
            {
                if (dino.tag == "Anky")
                {
                    if (Vector3.Distance(transform.position, dino.transform.position) < minDist || dino.GetComponent<Ankylosaurus>().getHealth() < 0.0f)
                    {
                        Vector3 pos = dino.position;
                        foodTarget.transform.position = pos;
                        updated = true;
                        minDist = Vector3.Distance(transform.position, dino.transform.position);

                        if (dino.GetComponent<Ankylosaurus>().getHealth() < 0.0f && Vector3.Distance(transform.position, dino.transform.position) < 10)
                        {
                            Debug.Log("Switching");
                            wander.enabled = false;
                            brain.popState();
                            brain.pushState(eatFood);
                        }
                    }
                }

            }
            wander.target = foodTarget;
        }
        else
        {
            base.searchForFood();
        }
    }

    public void attackMe()
    {
        StartCoroutine(attackObject());
    }

    IEnumerator attackObject()
    {
        float timetoEnd = Time.timeSinceLevelLoad + 2.0f;
        anim.SetBool("isAttacking", true);
        while (Time.timeSinceLevelLoad <= timetoEnd)
        {
            yield return null;
        }
        anim.SetBool("isAttacking", false);
    }
}
