using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurHealth : MonoBehaviour
{

    GameObject Anky;

    public int maxHealth = 100;
    public int currentHealth;

    public float lifeTime = 10f;

    bool isDead;

    // Use this for initialization
    void Start ()
    {

        Anky = GameObject.FindGameObjectWithTag("Anky");

        currentHealth = maxHealth;
        Debug.Log("Current Health = " + currentHealth);

    }
	
    // Update is called once per frame
    void Update ()
    {
		
        if (lifeTime > 0)
        {

            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)
            {

                Death();

            }

        }

    }

    void TakeDamage (int amount)
    {

        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {

            Death();

        }

    }

    void Death ()
    {

        if (Anky != null)
        {

            GameObject.Destroy(Anky);
            Debug.Log("Dinosaur is dead.");

            isDead = true;

        }
        else
        {

            return;

        }

    }

}
