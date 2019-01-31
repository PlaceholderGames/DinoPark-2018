using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for allowing easier referencing of genetic data,
/// where each gene corresponds with an element in the genes array
/// in DNA class.
/// </summary>
public enum PossibleGenes { Speed = 0, Sight = 1, Hunger = 2, Aggression = 3, GrowthCap = 4, GrowthRate = 5, Reserved6 = 6, Reserved7 = 7, Reserved8 = 8, Reserved9 = 9 }

/// <summary>
/// Code representation of the genetic material used by all
/// game Entities controlled by the genetic algorithm.
/// </summary>
public class DNA
{
    /// <summary>
    /// Number of genes
    /// </summary>
    private const int geneCount = 10;

    /// <summary>
    /// Stores genetic data used to represent different stats
    /// for the Entity.
    /// </summary>
    private List<float> genes = new List<float>(new float[geneCount]);

    /// <summary>
    /// Default DNA constructor. Initialises (zeroes) the values
    /// in the genes list. This constructor is designed for initialising
    /// DNA objects that are destined to be copied into.
    /// </summary>
    public DNA()
    {
        //Ensures the genes list can faciliate the maximum amount of genes
        genes.Capacity = geneCount;

        //Randomly decide start values for all genes
        for (int i = 0; i < geneCount; i++)
            genes[geneCount] = 0f;
    }

    /// <summary>
    /// Modified DNA constructor. Allows passing of DNA data directly
    /// into the constructor.
    /// </summary>
    /// <param name="desiredGenes">Genes you are wanting to pass data into.</param>
    /// <param name="newGenes">Gene data array whose layout matches that of desiredGenes' order.</param>
    public DNA(PossibleGenes[] desiredGenes, float[] newGenes)
    {
        SetDNA(desiredGenes, newGenes);
    }

    /// <summary>
    /// Allows new genes to be transplanted over the current genes.
    /// </summary>
    /// <param name="desiredGenes">Genes you are wanting to pass data into.</param>
    /// <param name="newGenes">Gene data array whose layout matches that of desiredGenes' order.</param>
    public void SetDNA(PossibleGenes[] desiredGenes, float[] newGenes)
    {
        foreach (PossibleGenes gene in desiredGenes)
            genes[(int)gene] = newGenes[(int)gene];
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