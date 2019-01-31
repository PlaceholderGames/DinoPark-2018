using UnityEngine;

/// <summary>
/// 
/// </summary>
class Velociraptor : Entity
{
    public override void derivedStart()
    {
        //Initialise Predator's DNA structure
        PossibleGenes[] genesRequested = new PossibleGenes[6] {
            PossibleGenes.Speed,
            PossibleGenes.Sight,
            PossibleGenes.Hunger,
            PossibleGenes.Aggression,
            PossibleGenes.GrowthCap,
            PossibleGenes.GrowthRate
        };
        float[] geneValues = new float[6] {
            1f,
            1f,
            1f,
            1f,
            1f,
            1f
        };
        dna = new DNA(genesRequested, geneValues);

        //Create this Entity's instance of common state variables
        FSMCommon com = new FSMCommon(transform.name, true, fsm, this);

        //Start FSM with the Roaming State
        fsm.ChangeState(new FSMRoamingState(com));
    }
}