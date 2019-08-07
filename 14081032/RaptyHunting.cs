using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptyHunting : StateMachineBehaviour
{
    Raptylocation;
    public Transform foodSource;
    public float thirst = 0;
    public float hunger = 0;
    public float speed;
    private Vector3 Raptylocation;
    private GameObject projectilePrefab;
    private object projectileSpawnTransform;
    private object projectileDamage;
    private readonly object HashAttack;

    public object Get { get; private set; }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Rapty has entered Hunting State");

        animator.SetBool("isHunting", true);
        animator.SetBool("isIdle", false);

        hunger = animator.GetFloat("hungerValue");
        thirst = animator.GetFloat("thirstValue");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NewMethod();
        //simple ticker that increases the hunger and thirst of the Rapty
        thirst += 0.3f;
        hunger += 0.8f;
        animator.SetFloat("thirstValue", thirst);
        animator.SetFloat("hungerValue", hunger);

        if (hunger >= 5)
        {
            //locate closet Anky and move towards it
            foodSource = GameObject.FindGameObjectWithTag("Anky").transform;
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, foodSource.position, speed * Time.deltaTime);
            //Decreases the hunger per tick when at foodsource
            if (foodSource.position == Raptylocation)
            {
                hunger -= 5;
            }
        }
    }

    private void NewMethod()
    {
        Raptylocation = GetComponent<Transform>();
    }

    public void TriggerAttack()
    {
        CharacterController.Animator.setTrigger(HashAttack);
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnTransform, Quaternion.identity);

        DealDamageOnCollsion dealDamageOnCollsion = projectile.GetComponent<DealDamageOnCollsion>();

        if (dealDamageOnCollsion == null)
        {
            dealDamageOnCollsion = projectile.AddComponent<DealDamageOnCollsion>();
        }

        dealDamageOnCollsion.Initialise(projectileDamage, Transform.position, true, DamageTypes.Arcane);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = CalculateLaunchVelocity();
    }

    private GameObject Instantiate(GameObject projectilePrefab, object position, Quaternion identity)
    {
        throw new NotImplementedException();
    }

    private T GetComponent<T>()
    {
        throw new NotImplementedException();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private class Raptylocation
    {
    }

    private class DealDamageOnCollsion
    {
        internal void Initialise(object projectileDamage, Vector3 position, bool v, object arcane)
        {
            throw new NotImplementedException();
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}