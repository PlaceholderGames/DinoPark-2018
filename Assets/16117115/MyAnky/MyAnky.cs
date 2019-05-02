using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FiniteSM; // Finite State Machine as a namespace

/*###############################################################################################################################################*/

public class MyAnky : Agent                           /* This is where all of the public variables are setup and also where all of the scripts within 
                                                         this project are set to a public name which then allow to be called within any script which inherit
                                                         this script which effects the entire states for the Anky. */
{
    public float Anky_Attack = 25.0f;
    public float Anky_Health = 100.0f;
    public float Anky_Hunger = 100.0f;
    public float Anky_Meat = 100.0f;
    public float Anky_Thirst = 100.0f;
    public bool IsDead = false;

    public FiniteSM<MyAnky> State { get; private set; }

    public Animator Animator;
    public GameObject AnkyHerd;
    public List<Transform> AnkyPredators;
    public ASPathFollower AS;
    public AStarSearch AStar;
    public Face Facing;
    public Flee Fleeing;
    public ASAgent ASAgent;
    public Agent MainAgent;
    public GameObject Terrain;
    public FieldOfView Vision;
    public Wander Wandering;
  /*###############################################################################################################################################*/

    private float CollisionTimer;
    private float HealthTimer;
    private float HungerTimer;
    private float ThirstTimer;
                                                             /*This is where the private variables are made to be used within this script mainly for the timers and ticks
                                                              *for the Anky's health and hunger/thirst levels */ 
    private float CollisionTicks = 5.0f;
    private float HealthTicks = 2.0f;
    private float HungerTicks = 5.0f;
    private float ThirstTicks = 4.5f;

    /*###############################################################################################################################################*/
                                                                             /* This section is all of the scripts and components getting called a variable name
                                                                              * and then getting the component which completes the setup for all of these components
                                                                              * to be used within this program */
    // Use this for initialization
    protected override void Start()
    {
        Animator = GetComponent<Animator>();
        AnkyHerd = GameObject.Find("AnkyHerd").gameObject;
        AS = GetComponent<ASPathFollower>();
        AStar = GetComponent<AStarSearch>();
        Facing = GetComponent<Face>();
        Fleeing = GetComponent<Flee>();
        ASAgent = GetComponent<ASAgent>();
        MainAgent = GetComponent<Agent>();
        Terrain = GameObject.Find("Terrain");
        Vision = GetComponent<FieldOfView>();
        Wandering = GetComponent<Wander>();

        /*###############################################################################################################################################*/
                                                                /* This section is enabling and setting the A star path finding when starting the script
                                                                 * and once it finds a new path it will disable the path finder */
        AS.enabled = true;
        AS.path = new ASPath();
        AS.enabled = false;

        /*###############################################################################################################################################*/
                                                                /* This section sets the starting state which is idle */
        State = new FiniteSM<MyAnky>(this);
        State.ChangeState(AnkyIdle.AnkyInstance);

        /*###############################################################################################################################################*/
                                                               /* This section sets the timers to 0 at startup, as the game goes on these will increase */
        CollisionTicks = 0.0f;
        HealthTimer = 0.0f;
        HungerTimer = 0.0f;
        ThirstTimer = 0.0f;

        /*###############################################################################################################################################*/

        Animator.SetBool("isIdle", true);
        Animator.SetBool("isEating", false);
        Animator.SetBool("isDrinking", false);
        Animator.SetBool("isAlerted", false);
        Animator.SetBool("isHunting", false);
        Animator.SetBool("isAttacking", false);
        Animator.SetBool("isFleeing", false);
        Animator.SetBool("isDead", false);

        base.Start(); // Starts base

    }

    protected override void Update()
    {
        UpdateAnkyPredators(); // Updates the Anky Predators

        State.UpdateCurrentState(); // Updates the current state

        /*###############################################################################################################################################*/
                                                 /* This section is setting up the health tick and timer and some parameters such as if the Anky health is 
                                                  * less than 100, then regenerate health which is calculated using the Anky health and thirst, dividing
                                                  * them both by 2 then added together which is then multiplied by 0.03 */
        HealthTimer = HealthTimer + Time.deltaTime;
        if (HealthTimer > HealthTicks)
        {
            float HealthRegeneration = (Anky_Hunger / 2 + Anky_Thirst / 2) * 0.05f;

            HealthTimer = 0.0f;
            if (Anky_Health < 100.0)
                Anky_Health = Anky_Health + HealthRegeneration;
            else
                Anky_Health = 100.0f;
        }

        /*###############################################################################################################################################*/
                                              /* This section is creating the thirst timer and tick. As the game goes on it will remove 0.50 health each tick */
        ThirstTimer = ThirstTimer + Time.deltaTime;
        if (ThirstTimer > ThirstTicks)
        {
            float HealthRemoval = 1.5f;

            ThirstTimer = 0.0f;
            Anky_Thirst = Anky_Thirst - HealthRemoval;
        }

        /*###############################################################################################################################################*/
                                              /* This section is creating the hunger health and tick. As the game goes on it will remove 0.75 health each tick */
        HungerTimer = HungerTimer + Time.deltaTime;
        if (HungerTimer > HungerTicks)
        {
            float HealthRemoval = 1.0f;

            HungerTimer = 0.0f;
            Anky_Hunger = Anky_Hunger - HealthRemoval;
        }
        CollisionTimer = CollisionTimer + Time.deltaTime;

        /*###############################################################################################################################################*/


        base.Update(); // Updates the base
    }

    protected override void LateUpdate()
    {
        base.LateUpdate(); // Late update for the base
    }
    /*###############################################################################################################################################*/
                                                  /* This function is where the Anky movement is created. This uses the search agent to collect the max speed
                                                   * which is then used for direction and works hand in hand with delta time. The Anky will translate the direction
                                                   * and position and then will face the desired way */
    public void MoveAnky(Vector3 AnkyMovement)
    {
        var MovementSpeed = ASAgent.maxSpeed;

        AnkyMovement = AnkyMovement * MovementSpeed * Time.deltaTime;
        transform.Translate(AnkyMovement, Space.World);
        transform.LookAt(transform.position, AnkyMovement);
    }
    /*###############################################################################################################################################*/
                                                 /* This function is regarding collision between Anky and Rapty. If Rapty comes in contact with Anky when
                                                  * Anky is in attacking state then it will deal damage to the Rapty. This is done by retrieving the position
                                                  * of each Anky and Rapty and using the tags to determine who is who. This will then be broadcasted within the
                                                  * run log. */
    private void OnCollisionStay(Collision AnkyCollision)
    {
        if (AnkyCollision.gameObject.tag == "Rapty" && State.ActiveState is AnkyAttack && CollisionTimer > CollisionTicks &&
            !(AnkyCollision.gameObject.GetComponent<MyRapty>().State.ActiveState is RaptyDead))
        {
            CollisionTimer = 0.0f;
            AnkyCollision.gameObject.GetComponent<MyRapty>().Rapty_Health = AnkyCollision.gameObject.GetComponent<MyRapty>().Rapty_Health - Anky_Attack;
            Debug.Log(gameObject.name + " has dealt " + Anky_Attack + " Some damage to " + AnkyCollision.gameObject + ".");
        }
    }
    /*###############################################################################################################################################*/
                                               /* This function is regarding the tracking of the nearest predator. This is done by retrieving the position
                                                * of any Rapty in the overworld in the near vicinity. If there is no predators nearby then it will return
                                                * Rapty as it is the only predator of the Anky. If there is a nearby predator then it will also return Rapty */
    public Transform TrackNearestPredator()
    {
        Transform NearestRapty = null;

        foreach (var rapty in AnkyPredators)
        {
            var distance = Vector3.Distance(transform.position, rapty.position);

            if (NearestRapty == null)
                NearestRapty = rapty;
            else if (distance < Vector3.Distance(transform.position, NearestRapty.position))
                NearestRapty = rapty;
        }

        return NearestRapty;
    }
    /*###############################################################################################################################################*/
                                            /* This function is to update the nearby predators. As it updates the nearby predators, it uses the Vision
                                             * component which then compares the tag and if the tag is Rapty then it will add Rapty to the list of 
                                             * predators. */
    private void UpdateAnkyPredators()
    {
        AnkyPredators = new List<Transform>();

        foreach (var AnkyTarget in Vision.visibleTargets)
        {
            if (AnkyTarget.gameObject.CompareTag("Rapty"))
                AnkyPredators.Add(AnkyTarget);
        }
    }
    /*###############################################################################################################################################*/
                                            /* This function is simply to kill the Anky, if called within any script and it meets certain parameters in so
                                             * script then Anky is dead. */
    public void Dead()
    {
        Destroy(gameObject);
    }

}
