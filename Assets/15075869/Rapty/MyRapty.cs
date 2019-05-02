using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FSM;

public class MyRapty : Agent
{
    public Animator anim;
    //so can set dino to the dino park area and he will
    //stay within the boundaries
    public GameObject dinoPark;
    //used in the RaptyAttack class
    public Seek seeking;
    //used in the eat function
    public FieldOfView Sight;
    //used in the RaptyMovement function
    public ASAgent agent;
    //used in the RaptyHunting and RaptyDead class
    public Wander wandering;
    //used in the RaptyHunting class
    public Arrive arriving;
    //used in the RaptyRunAway class
    public Flee runAway;
    public ASPathFollower following;
    public AStarSearch aStar;

    public FSM<MyRapty> State { get; private set; }

    public enum raptyState
    {
        IDLE,       // The default state on creation.
        RAPTOR,     // This is for eating depending on location of a target object (killed prey)
        DRINKING,   // This is for Drinking, depending on y value of the object to denote water level
        ALERTED,      // This is for hightened awareness, such as looking around
        HUNTING,    // Moving with the intent to hunt
        ATTACKING,  // Causing damage to a specific target
        FLEEING,     // Running away from a specific target
        DEAD
    };

    //setting raptor as alive
    public bool rapty_dead = false;
    //setting health/hunger/and need for water to 100
    public float rapty_health = 100.0f;
    public float rapty_hunger = 100.0f;
    public float rapty_water = 100.0f;
    public float rapty_attack = 20.0f;
    public float rapty_meat = 100.0f;

    //this is for the raptors food
    public List<Transform> Food;//alive
    public List<Transform> DeadDinoMeat;//dead

    //timer and counter for the raptor getting hungrier
    public float hunger_timer;
    public float hunger_counter;
    //timer and counter for the raptors health
    public float health_timer;
    public float health_counter;
    //timer and counter for the raptor getting thirstier
    public float water_timer;
    public float water_counter;
    //timer and counter for the raptor colliding with objects
    public float collision_counter;
    public float collision_timer;


    // Use this for initialization
    protected override void Start()
    {
        //making the variables equal to the components
        anim = GetComponent<Animator>();
        dinoPark = GameObject.Find("Terrain");
        seeking = GetComponent<Seek>();
        Sight = GetComponent<FieldOfView>();
        wandering = GetComponent<Wander>();
        agent = GetComponent<ASAgent>();
        arriving = GetComponent<Arrive>();
        runAway = GetComponent<Flee>();
        following = GetComponent<ASPathFollower>();
        aStar = GetComponent<AStarSearch>();

        //this will enable the path follower
        //find the new path and then disable it
        following.enabled = true;
        following.path = new ASPath();
        following.enabled = false;

        State = new FSM<MyRapty> (this);
        //changing state to idle
        State.Change(RaptyIdle.RaptyInstance);

        // Assert default animation booleans and floats
        anim.SetBool("isIdle", true);
        anim.SetBool("isEating", false);
        anim.SetBool("isDrinking", false);
        anim.SetBool("isAlerted", false);
        anim.SetBool("isHunting", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isFleeing", false);
        anim.SetBool("isDead", false);
        // This with GetBool and GetFloat allows 
        // you to see how to change the flag parameters in the animation controller

        //setting timer and counters to zero
        water_timer = 0.0f;
        health_timer = 0.0f;
        hunger_timer = 0.0f;
        collision_counter = 0.0f;
        base.Start();
    }

    //This is for when the raptor needs to get food (anky dinosaurs)
    //for each food available for the raptor it will find how far away it is
    //and move its position towards the food if within distance
    public Transform getFood()
    {
        Transform FindFood = null;

        foreach (var food in Food)
        {
            var distance_from_food = Vector3.Distance(transform.position, food.position);

            if (FindFood == null)
                FindFood = food;
            else if (distance_from_food < Vector3.Distance(transform.position, FindFood.position))
                FindFood = food;
        }

        return FindFood;
    }

    //this is for when the raptor is in distance of any dead dinosaurs they can eat as food
    //for each dinosaur meat available for the raptor it will find how far away it is
    //and move its position towards the food if within distance
    public Transform getDinoMeat()
    {
        Transform FindDinoMeat = null;

        foreach (var food in DeadDinoMeat)
        {
            var distance_from_food = Vector3.Distance(transform.position, food.position);

            if (FindDinoMeat == null)
                FindDinoMeat = food;
            else if (distance_from_food < Vector3.Distance(transform.position, FindDinoMeat.position))
                FindDinoMeat = food;
        }
        return FindDinoMeat;
    }

    //if the raptor is dead then the game object will be removed
    public void Dead()
    {
        Destroy(gameObject);
    }

    //this is for the movement of the raptor
    //it is getting the speed of the raptor from the agent script
    //it will then transform its postion and movement around the map
    public void RaptyMovement(Vector3 movement)
    {
        float raptySpeed = agent.maxSpeed;
        movement = movement * (raptySpeed * Time.deltaTime);
        transform.LookAt(transform.position, movement);
        transform.Translate(movement, Space.World);
    }

    //OnCollisionEnter  is called when the game object collider starts
    //colliding with another game object
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Anky" && State.current_state is RaptyAttack && collision_timer > collision_counter && !(collision.gameObject.GetComponent<MyAnky>().State.current_state is AnkyDead))
        {
            collision_timer = 0.0f;
            collision.gameObject.GetComponent<MyAnky>().anky_health -= rapty_attack;
        }
    }

    //function for raptors available food
    //using the lists of food available for the raptor, Food (Anky) and DeadDinoMeat (dead dinosaurs)
    //I use the sight script to find visible targets (food)
    private void Eat()
    {
        Food = new List<Transform>();
        DeadDinoMeat = new List<Transform>();

        foreach (var food in Sight.visibleTargets)
            if (food.gameObject.tag == "Anky")
            {
                if (food.gameObject.GetComponent<MyAnky>().State.current_state is AnkyDead)
                    DeadDinoMeat.Add(food);
                else
                    Food.Add(food);
            }
    }

    protected override void Update()
    {
        //get dead dino meat function
        getDinoMeat();
        //Eat();
        //update the raptors state
        State.Update();

        hunger_timer = hunger_counter + Time.deltaTime;
        //if the hunger timer is more than the counter
        //then it will minus the dying amount from the raptors hunger bar
        if (hunger_timer > hunger_counter)
        {
            var dying = 0.80f;
            hunger_timer = 0.0f;
            rapty_hunger = rapty_hunger - dying;
        }

        water_timer = water_timer + Time.deltaTime;
        //if the water timer is more than the counter
        //then it will minus the dying amount from the raptors water bar
        if (water_timer > water_counter)
        {
            var dying = 0.40f;
            water_timer = 0.0f;
            rapty_water = rapty_water - dying;
        }

        health_timer = health_timer + Time.deltaTime;
        //if the raptor health timer is more than the health counter
        //then health up is calculated by dividing the water and hunger by 3 and adding
        //them together and multiplying by 0.09
        //if the raptors health is below 100 then it will add health_up to the raptors health
        if (health_timer > health_counter)
        {
            var health_up = (rapty_water / 3 + rapty_hunger / 3) * 0.09f;

            health_timer = 0.0f;
            if (rapty_health < 100.0f)
                rapty_health = rapty_health + health_up;
            else
                rapty_health = 100.0f;
        }

        collision_timer = collision_timer + Time.deltaTime;
        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }
}
