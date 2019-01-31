/*
    
    Mating State

    Action:
        

    For:
        All

    Starts from:
        Any (temp)

    Exits into:
        Any (temp)

*/

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class FSMMatingState : FSMState
{
    /// <summary>
    /// 
    /// </summary>
    float mutationRate = 0.05f;

    public FSMMatingState(FSMCommon nCOM) : base(nCOM) { com = nCOM; }

    public override void Start()
    {
        if (com.debugging) Debug.Log(com.name + ": entered Mating State");

        //Get reference to all Entities in scene
        Entity[] all = Object.FindObjectsOfType<Entity>();

        //Filter these into the correct species
        List<Entity> potentials = new List<Entity>();
        foreach (Entity ent in all)
            //Base the filter on the first letter of name 
            //Ideally a better method should be found later on
            if (ent.name[0] == com.name[0]) potentials.Add(ent);


        //if there are more than one pontentials, perform roulette selection
        if (potentials.Count > 1)
        {
            //calculate initial fitness sum
            float fitnessSum = 0;
            for (int i = 0; i < potentials.Count; i++)
                fitnessSum += potentials[i].GetDNA().GetFitness();

            //Record fitness
            Debug.Log("Current population: " + fitnessSum);

            //generate number for roulette target
            float rouletteTarget = Random.Range(0, fitnessSum);
            float rouletteSum = 0;

            //work partial sum until selection
            for (int i = 0; i < potentials.Count; i++)
            {
                rouletteSum += potentials[i].GetDNA().GetFitness();
                if (rouletteSum >= rouletteTarget)
                {
                    com.target = potentials[i];
                    break;
                }
            }
        }
        else com.target = potentials[0];

        //ATTRACT MATE HERE
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Transition()
    {
        base.Transition();
    }
}