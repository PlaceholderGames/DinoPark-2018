using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyHealth : MonoBehaviour
{

    public int startingHealth = 100;    // Starting Health Pool for the Ankylosaurus.
    public int currentHealth;           // Current Health of the Ankylosaurus.

    bool isDead;                        // Whether the Ankylosaurus is Dead.
    bool damaged;                       // True when the Ankylosaurus gets Damaged.

    // Use this for initialization
    void Start ()
    {

        // Set the initial health of the Ankylosaurus.
        currentHealth = startingHealth;

    }
	
    // Update is called once per frame
    void Update ()
    {
		
        

    }

    public void TakeDamage (int amount)
    {

        // Reduce the current health of the Ankylosaurus by the damage amount.
        currentHealth -= amount;

        // If the Ankylosaurus lost all its health and hasn't "died" yet.
        if (currentHealth <= 0 && !isDead)
        {

            // It dies.
            Death();

        }

    }

    void Death ()
    {

        // Set the death flag so this function won't be called again.
        isDead = true;



    }

}
