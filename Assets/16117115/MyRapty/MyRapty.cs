using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FiniteSM; // Finite State Machine as a namespace
using UnityEditor;

/*###############################################################################################################################################*/
                                                      /* This is where all of the public variables are setup and also where all of the scripts within 
                                                       * this project are set to a public name which then allow to be called within any script which inherit
                                                       * this script which effects the entire states for the Rapty. */
public class MyRapty : Agent
{
    public float Rapty_Attack = 15.0f;
    public float Rapty_Health = 100.0f;
    public float Rapty_Hunger = 100.0f;
    public float Rapty_MeatOnRapty = 100.0f;
    public float Rapty_Thirst = 100.0f;
    public bool IsDead = false;

    public FiniteSM<MyRapty> State { get; private set; }

    public Animator Animator;
    public ASPathFollower AS;
    public ASAgent ASAgent;
    public AStarSearch AStar;
    public Face Facing;
    public Flee Fleeing;
    public Agent MainAgent;
    public Arrive GoTo;
    public Seek Seeking;
    public GameObject Terrain;
    public FieldOfView Vision;
    public Wander Wandering;

    public List<Transform> NearestLiveAnky;
    public List<Transform> NearestDeadAnky;

    /*###############################################################################################################################################*/

    private float CollisionTimer;
    private float HealthTimer;
    private float HungerTimer;
    private float ThirstTimer;
                                                             /*This is where the private variables are made to be used within this script mainly for the timers and ticks
                                                              *for the Rapty's health and hunger/thirst levels */

    private float CollisionTicks = 1.55f;
    private float HealthTicks = 4.25f;
    private float HungerTicks = 2.45f;
    private float ThirstTicks = 3.25f;

    /*###############################################################################################################################################*/
                                                                             /* This section is all of the scripts and components getting called a variable name
                                                                              * and then getting the component which completes the setup for all of these components
                                                                              * to be used within this program */
    // Use this for initialization
    protected override void Start()
    {
        Animator = GetComponent<Animator>();
        AS = GetComponent<ASPathFollower>();
        AStar = GetComponent<AStarSearch>();
        Facing = GetComponent<Face>();
        Fleeing = GetComponent<Flee>();
        GoTo = GetComponent<Arrive>();
        ASAgent = GetComponent<ASAgent>();
        MainAgent = GetComponent<Agent>();
        Seeking = GetComponent<Seek>();
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
        State = new FiniteSM<MyRapty>(this);
        State.ChangeState(RaptyIdle.RaptyInstance);

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
        // Updates the nearest dead Anky
        RetrieveNearestDeadAnky();
        //RetrieveAnky();

        State.UpdateCurrentState(); // Updates the current state

       /*###############################################################################################################################################*/
        /* This section is creating the hunger health and tick. As the game goes on it will remove 0.75 health each tick */

        HungerTimer = HungerTimer + Time.deltaTime;
        if (HungerTimer > HungerTicks)
        {
            var HealthRemoval = 0.90f;

            HungerTimer = 0.0f;
            Rapty_Hunger = Rapty_Hunger - HealthRemoval;
        }

        /*###############################################################################################################################################*/
                                                 /* This section is creating the thirst timer and tick. As the game goes on it will remove 0.50 health each tick */
        ThirstTimer = ThirstTimer + Time.deltaTime;
        if (ThirstTimer > ThirstTicks)
        {
            var HealthRemoval = 1.5f;

            ThirstTimer = 0.0f;
            Rapty_Thirst = Rapty_Thirst - HealthRemoval;
        }

        /*###############################################################################################################################################*/
                                                 /* This section is setting up the health tick and timer and some parameters such as if the Rapty health is 
                                                  * less than 100, then regenerate health which is calculated using the Rapty health and thirst, dividing
                                                  * them both by 2 then added together which is then multiplied by 0.03 */
        HealthTimer = HealthTimer + Time.deltaTime;
        if (HealthTimer > HealthTicks)
        {
            var regeneration = (Rapty_Hunger / 2 + Rapty_Thirst / 2) * 0.05f;

            HealthTimer = 0.0f;
            if (Rapty_Health < 100.0)
                Rapty_Health = Rapty_Health + regeneration;
            else
                Rapty_Health = 100.0f;
        }

        /*###############################################################################################################################################*/

        CollisionTimer = CollisionTimer + Time.deltaTime;

        base.Update();  // Updates the base after all parameter checks
    }

    protected override void LateUpdate()
    {
        base.LateUpdate(); // Late base update
    }
    /*###############################################################################################################################################*/
                                                /* This function is where the Rapty movement is created. This uses the search agent to collect the max speed
                                                 * which is then used for direction and works hand in hand with delta time. The Rapty will translate the direction
                                                 * and position and then will face the desired way */
    public void MoveRapty(Vector3 RaptyMovement)
    {
        var MovementSpeed = ASAgent.maxSpeed;

        RaptyMovement = RaptyMovement * MovementSpeed * Time.deltaTime;
        transform.Translate(RaptyMovement, Space.World);
        transform.LookAt(transform.position, RaptyMovement);
    }
    /*###############################################################################################################################################*/
                                                /* This function is regarding collision between Anky and Rapty. If Anky comes in contact with Rapty when
                                                 * Rapty is in attacking state then it will deal damage to the Anky. This is done by retrieving the position
                                                 * of each Anky and Rapty and using the tags to determine who is who. This will then be broadcasted within the
                                                 * run log. */
    private void OnCollisionEnter(Collision RaptyCollision)
    {
        if (RaptyCollision.gameObject.tag == "Anky" && State.ActiveState is RaptyAttack && CollisionTimer > CollisionTicks &&
            !(RaptyCollision.gameObject.GetComponent<MyAnky>().State.ActiveState is AnkyDead))
        {
            CollisionTimer = 0.0f;
            RaptyCollision.gameObject.GetComponent<MyAnky>().Anky_Health -= Rapty_Attack;
            Debug.Log(gameObject.name + " has dealt " + Rapty_Attack + " Some damage to " + RaptyCollision.gameObject + ".");
        }
    }
    /*###############################################################################################################################################*/
                                               /* This function is regarding the tracking of the nearest dead prey. This is done by retrieving the position
                                                * of any dead Anky in the overworld in the near vicinity. If there is no predators nearby then it will return
                                                * RaptyPrey as it is the only prey for the Rapty. If there is a nearby dead prey then it will also return RaptyPrey */
    public Transform RetrieveNearestDeadAnky()
    {
        Transform NearestAnkyDead = null;

        foreach (var RaptyPrey in NearestDeadAnky)
        {
            var distance = Vector3.Distance(transform.position, RaptyPrey.position);

            if (NearestAnkyDead == null)
                NearestAnkyDead = RaptyPrey;
            else if (distance < Vector3.Distance(transform.position, NearestAnkyDead.position))
                NearestAnkyDead = RaptyPrey;
        }

        return NearestAnkyDead;
    }
    /*###############################################################################################################################################*/
                                            /* This function is regarding the tracking of the nearest dead prey. This is done by retrieving the position
                                             * of any dead Anky in the overworld in the near vicinity. If there is no predators nearby then it will return
                                             * RaptyPrey as it is the only prey for the Rapty. If there is a nearby dead prey then it will also return RaptyPrey */

    public Transform RetrieveNearestLiveAnky()
    {
        Transform NearestLivingAnky = null;

        foreach (var RaptyPrey in NearestLiveAnky)
        {
            var distance = Vector3.Distance(transform.position, RaptyPrey.position);

            if (NearestLivingAnky == null)
                NearestLivingAnky = RaptyPrey;
            else if (distance < Vector3.Distance(transform.position, NearestLivingAnky.position))
                NearestLivingAnky = RaptyPrey;
        }

        return NearestLivingAnky;
    }
    /*###############################################################################################################################################*/
    /* This function is to update the nearby Anky. As it updates the nearby Anky, it uses the Vision
     * component which then compares the tag and if the tag is Anky then it will add Anky to the list of 
     * prey. */
    private void RetrieveAnky()
    {
        NearestLiveAnky = new List<Transform>();
        NearestDeadAnky = new List<Transform>();

        foreach (var target in Vision.visibleTargets)
            if (target.gameObject.tag == "Anky")
            {
                if (target.gameObject.GetComponent<MyAnky>().State.ActiveState is AnkyDead)
                    NearestDeadAnky.Add(target);
                else
                    NearestLiveAnky.Add(target);
            }
    }
    /*###############################################################################################################################################*/
                                            /* This function is simply to kill the Rapty, if called within any script and it meets certain parameters in so
                                             * script then Rapty is dead. */
    public void Dead()
    {
        Destroy(gameObject);
    }
}
