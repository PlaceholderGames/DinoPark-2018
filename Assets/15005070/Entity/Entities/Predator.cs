using UnityEngine;

/// <summary>
/// 
/// </summary>
class Predator : Entity
{
    public override void derivedStart()
    {
        //Initialise Predator's DNA structure
        dna = new DNA(new[] {
            PossibleGenes.Speed,
            PossibleGenes.Sight,
            PossibleGenes.Hunger,
            PossibleGenes.Aggression,
            PossibleGenes.GrowthCap,
            PossibleGenes.GrowthRate },
            new[] {
                1f,
                1f,
                1f,
                1f,
                1f,
                1f,
            });

        //Create this Entity's instance of common state variables
        FSMCommon com = new FSMCommon(transform.name, true, fsm, this);

        //Start FSM with the Roaming State
        fsm.ChangeState(new FSMRoamingState(com));
    }
}