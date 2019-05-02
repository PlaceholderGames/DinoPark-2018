using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FSM;

public class MyAnky : Agent
{
    public Animator anim;
    //so can set dino to the dino park area and he will
    //stay within the boundaries
    public GameObject dinoPark;
    //used in the AnkyAttack class
    public Seek seek;
    //used for UpdateEnemies function
    public FieldOfView Sight;
    //used ion the AnkyMovement function
    public ASAgent agent;
    //used in AnkyDead class
    public Wander wandering;
    public Face faceTowards;
    //used in the AnkyRunAway class
    public Flee runAway;

    public FSM<MyAnky> State { get; private set; }

    public enum ankyState
    {
        IDLE,       // The default state on creation.
        EATING,     // This is for eating depending on y value of the object to denote grass level
        DRINKING,   // This is for Drinking, depending on y value of the object to denote water level
        ALERTED,      // This is for hightened awareness, such as looking around
        GRAZING,    // Moving with the intent to find food (will happen after a random period)
        ATTACKING,  // Causing damage to a specific target
        FLEEING,     // Running away from a specific target
        DEAD
    };

    //used to find the Ankys enemies (raptors)
    public List<Transform> Enemies;

    //setting anky as alive
    public bool anky_dead = false;
    //setting the ankys stats
    public float anky_health = 100.0f;
    public float anky_water = 100.0f;
    public float anky_attack = 30.0f;
    public float anky_hunger = 100.0f;
    public float anky_meat = 100.0f;

    //timer and counter for the raptors health
    public float health_timer;
    public float health_counter;
    //timer and counter for the raptor getting hungrier
    public float hunger_timer;
    public float hunger_counter;
    //timer and counter for the raptor getting thirstier
    public float water_timer;
    public float water_counter;
    //timer and counter for the raptor colliding with objects
    public float collision_counter;
    public float collision_timer;


    // Use this for initialization
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        dinoPark = GameObject.Find("dinoPark");
        //changing state to idle
        seek = GetComponent<Seek>();

        State = new FSM<MyAnky>(this);
        State.Change(AnkyIdle.AnkyInstance);

        // Assert default animation booleans and floats
        anim.SetBool("isIdle", true);
        anim.SetBool("isEating", false);
        anim.SetBool("isDrinking", false);
        anim.SetBool("isAlerted", false);
        anim.SetBool("isGrazing", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isFleeing", false);
        anim.SetBool("isDead", false);
        anim.SetFloat("speedMod", 1.0f);
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller

        health_timer = 0.0f;
        hunger_timer = 0.0f;
        water_timer = 0.0f;

        base.Start();

    }

    //OnCollsionStay will be called once per frame
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Rapty" && State.current_state is AnkyAttack && collision_timer > collision_counter && !(collision.gameObject.GetComponent<MyRapty>().State.current_state is RaptyDead))
        {
            collision_timer = 0.0f;
            collision.gameObject.GetComponent<MyRapty>().rapty_health = collision.gameObject.GetComponent<MyRapty>().rapty_health - anky_attack;
        }
    }

    //this is so the anky can track where the raptors are
    //if the raptor is within distance of the anky then i will return it as
    //an enemy
    public Transform getEnemies()
    {
        Transform enemy = null;

        foreach (var raptor in Enemies)
        {
            var distance_from_enemy = Vector3.Distance(transform.position, raptor.position);

            if (enemy == null)
                enemy = raptor;
            else if (distance_from_enemy < Vector3.Distance(transform.position, enemy.position))
                enemy = raptor;
        }
        return enemy;
    }

    //this uses the sight script
    //it will update game object raptor as an enemy and will add it
    //to the list
    private void UpdateEnemies()
    {
        Enemies = new List<Transform>();

        foreach (var enemy in Sight.visibleTargets)
            {
            if (enemy.gameObject.tag == "Rapty")
                Enemies.Add(enemy);
            }
    }

    //this is for the movement of the anky
    //it is getting the speed of the anky from the agent script
    //it will then transform its postion and movement around the map
    public void AnkyMovement(Vector3 movement)
    {
        float ankySpeed = agent.maxSpeed;

        movement = movement * (ankySpeed * Time.deltaTime);
        transform.LookAt(transform.position, movement);
        transform.Translate(movement, Space.World);
    }

    protected override void Update()
    {
        //updating the enemies
        UpdateEnemies();
        //updating ankys state
        State.Update();

        hunger_timer = hunger_timer + Time.deltaTime;
        //if the hunger timer is more than the counter
        //then it will minus the dying amount from the ankys hunger bar
        if (hunger_timer > hunger_counter)
        {
            float dying = 0.80f;
            hunger_timer = 0.0f;
            anky_hunger = anky_hunger - dying;
        }

        water_timer = water_timer + Time.deltaTime;
        //if the water timer is more than the counter
        //then it will minus the dying amount from the ankys water bar
        if (water_timer > water_counter)
        {
            float dying = 0.40f;
            water_timer = 0.0f;
            anky_water = anky_water - dying;
        }

        health_timer = health_timer + Time.deltaTime;
        //if the anky health timer is more than the health counter
        //then health up is calculated by dividing the water and hunger by 3 and adding
        //them together and multiplying by 0.09
        //if the ankys health is below 100 then it will add health_up to the ankys health
        if (health_timer > health_counter)
        {
            float health_up = (anky_water / 3 + anky_hunger / 3) * 0.09f;

            health_timer = 0.0f;
            if (anky_health < 100.0f)
                anky_health = anky_health + health_up;
            else
                anky_health = 100.0f;
        }
        // Idle - should only be used at startup

        // Eating - requires a box collision with a dead dino

        // Drinking - requires y value to be below 32 (?)

        // Alerted - up to the student what you do here

        // Hunting - up to the student what you do here

        // Fleeing - up to the student what you do here

        // Dead - If the animal is being eaten, reduce its 'health' until it is consumed

        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    //if the anky is dead then the game object will be removed
    public void Dead()
    {
        Destroy(gameObject);
    }
}
