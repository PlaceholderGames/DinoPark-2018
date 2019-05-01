using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyAttack : MonoBehaviour
{

    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.

    GameObject Anky;                            // Reference to the Ankylosaurus GameObject.
    AnkyHealth ankyHealth;                      // Reference to the Ankylosaurus' health.
    RaptyHealth raptyHealth;                    // Reference to the Rapty's health.

    bool ankyInRange;                           // Whether the Ankylosaurus is within the trigger collider and can be attacked.

    float timer;                                // Timer for counting up to the next attack.

    // Use this for initialization
    void Start ()
    {

        // Setting up the references.
        Anky = GameObject.FindGameObjectWithTag("Anky");
        ankyHealth = Anky.GetComponent<AnkyHealth>();
        raptyHealth = GetComponent<RaptyHealth>();

    }

    private void OnTriggerEnter(Collider other)
    {
        
        // If the entering collider is the Ankylosaurus.
        if (other.gameObject == Anky)
        {

            // The Ankylosaurus is in range.
            ankyInRange = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        
        // If the exiting collider is the Ankylosaurus.
        if (other.gameObject == Anky)
        {

            // The Ankylosaurus is no longer in range.
            ankyInRange = false;

        }

    }

    // Update is called once per frame
    void Update ()
    {

        // Add the time since the Update was last called to the Timer.
        timer += Time.deltaTime;

        // If the Timer exceeds the time between attacks, the Ankylosaurus is in range and this Raptor is alive.
        if (timer >= timeBetweenAttacks && ankyInRange && raptyHealth.currentHealth > 0)
        {

            // Attack.
            Attack ();

        }

        // If the pAnkylosaurus has zero or less health.
        if (ankyHealth.currentHealth <= 0)
        {



        }

    }

    void Attack ()
    {

        // Reset the Timer.
        timer = 0f;

        // If the Ankylosaurus has health to lose.
        if (ankyHealth.currentHealth > 0)
        {

            ankyHealth.TakeDamage(attackDamage);

        }

    }

}
