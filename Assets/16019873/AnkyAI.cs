using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkyAI : MonoBehaviour
{
    //there is one Alpha male raptor that will control the rest when in the hunting stage
    //the rest will follow his commands
    //creating a variable to be able to access the conditions set in the Unity Animator Controller
    Animator animator;
    //linked to the dino
    public GameObject thisDino;
    public GameObject grass;
    public GameObject enemy;
    public GameObject claws;
    public GameObject alpha;
    public GameObject Cube3;   //rapty's body
    public GameObject waterLocation;
    //there is one Alpha male raptor that will control the rest when in the hunting stage
    //the rest will follow his commands
    public ASAgentInstance ASagent;
    public ASPathFollower followingDino;
    //A star path finding
    public AStarSearch aStarVar;
    //alpha male variables
    //current hunger/thirst values
    public float currentHunger = 100;
    public float currentThirst = 100;
    public float currentHealth = 100;
    //values for enabling and disabling scripts in run time
    public FieldOfView fov;
    public Wander wander;
    public Pursue pursue;
    public Face face;
    public Dead deadD;
    public bool isAlpha;

    // Use this for initialization
    void Start () {
        fov = thisDino.GetComponent<FieldOfView>();
        pursue = thisDino.GetComponent<Pursue>();
        wander = thisDino.GetComponent<Wander>();
        face = thisDino.GetComponent<Face>();
        ASagent = thisDino.GetComponent<ASAgentInstance>();
        deadD = thisDino.GetComponent<Dead>();
        //setting it to get data from the FSM created in Unity Animator
        animator = GetComponent<Animator>();
        //make other raptys follow the alpha dino
        aStarVar = GetComponent<AStarSearch>();
        followingDino = GetComponent<ASPathFollower>();

        //at the start of the game, dino got full health/hunger/thirst bar
        currentHealth = DinoBaseClass.maxHealth;
        currentHunger = DinoBaseClass.maxHunger;
        currentThirst = DinoBaseClass.maxThirst;
    }
	
	// Update is called once per frame
	void Update () {

        //Decreases hunger, thrist and stores height to decide when to go into the drinking state
        animator.SetFloat("hunger", animator.GetFloat("hunger") - DinoBaseClass.decreaseHunger * Time.deltaTime);
        animator.SetFloat("thirst", animator.GetFloat("thirst") - DinoBaseClass.decreaseThirst * Time.deltaTime);
        animator.SetFloat("yAxis", animator.gameObject.transform.position.y);

        //Search and store the gameObject of the last grass and rapty seen
        foreach (Transform i in fov.visibleTargets)
        {
            //Reset if there is no rapty
            animator.SetBool("nobodyAround", true);
            animator.SetBool("isAlert", false);

            if (i.tag == "grassFood")
            {
                grass = i.gameObject;
                pursue.target = i.gameObject;

            }
            else if (i.tag == "Rapty")
            {
                //Flee
                animator.SetBool("nobodyAround", false);
                animator.SetBool("isAlert", true);
                //Debug.Log("Found Anky!");
                enemy = i.gameObject;
            }
        }

        if(!isAlpha)
        {
            //go to alpha anky if too far away, but go for water if very thirst
            if (Vector3.Distance(thisDino.transform.position, alpha.transform.position) >= 20 && animator.GetFloat("thirst") > 20)
            {
                ASagent.enabled = true;
                aStarVar.target = alpha.gameObject;
            }
            else if (Vector3.Distance(thisDino.transform.position, alpha.transform.position) <= 10)
            {
                ASagent.enabled = false;
            }

        }
    }
}