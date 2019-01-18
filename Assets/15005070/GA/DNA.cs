using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for allowing easier referencing of genetic data,
/// where each gene corresponds with an element in the genes array
/// in DNA class.
/// </summary>
public enum PossibleGenes { Speed = 0, Sight = 1, Hunger = 2 }

/// <summary>
/// Code representation of the genetic material used by all
/// game Entities controlled by the genetic algorithm.
/// </summary>
class DNA
{
    /// <summary>
    /// Number of genes
    /// </summary>
    private int geneCount = 3;

    /// <summary>
    /// Stores genetic data used to represent different stats
    /// for the Entity.
    /// </summary>
    private List<float> genes = new List<float>();

    /// <summary>
    /// Default DNA constructor that initialises all gene values.
    /// </summary>
    public DNA()
    {
        //Ensures the genes list can faciliate the maximum amount of genes
        genes.Capacity = geneCount;

        //Randomly decide start values for all genes
        for (int i = 0; i < geneCount; i++)
            genes[geneCount] = Random.Range(0f, 0f);
    }

    /// <summary>
    /// Specific DNA constructor that initalises only desired gene values.
    /// Useful for Entities with more limited gene sets.
    /// </summary>
    /// <param name="desiredGenes">Array of genes to be initalised.</param>
    public DNA(PossibleGenes[] desiredGenes)
    {
        //Ensures the genes list can faciliate the maximum amount of genes
        genes.Capacity = geneCount;

        //Randomly decide start values for desired genes
        foreach (PossibleGenes gene in desiredGenes)
            genes[(int)gene] = Random.Range(0f, 0f);
    }

    /// <summary>
    /// Allows new genes to be transplanted over the current genes.
    /// </summary>
    /// <param name="newGenes">New genes to be set in to this DNA object.
    /// The index must correspond with current gene format (PossibleGenes)
    /// for it to work properly.</param>
    public void SetDNA(float[] newGenes)
    {
        //Randomly decide start values for all genes
        for (int i = 0; i < newGenes.Length; i++)
            genes[i] = newGenes[i];
    }

    /// <summary>
    /// Returns specific gene data.
    /// </summary>
    /// <param name="gene">Specific gene to request data from.</param>
    public float GetGene(PossibleGenes gene) { return genes[(int)gene]; }

    /// <summary>
    /// Returns fitness value based on averaged genetic data.
    /// </summary>
    public float GetFitness()
    {
        float sum = 0f;
        foreach (float gene in genes)
        {
            sum += gene;
        }
        return (sum / genes.Capacity);
    }
}