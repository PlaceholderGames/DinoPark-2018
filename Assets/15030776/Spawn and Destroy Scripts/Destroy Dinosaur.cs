using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDinosaur : MonoBehaviour
{

    public float lifeTime = 120f;
	
    // Update is called once per frame
    void Update ()
    {
		
        if (lifeTime > 0)
        {

            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)
            {

                Destruction();

            }

        }

        if (this.transform.position.y <= -20)
        {

            Destruction();

        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "destroyer") ;

    }

    void Destruction()
    {

        Destroy(this.gameObject);

    }

}
