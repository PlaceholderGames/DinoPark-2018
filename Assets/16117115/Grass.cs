using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour    // Class for grass to spwan within map
{
    public Terrain GrassTerrain;
    public int[,] Details;

    private float GrassTimer;
    private float GrassTicks = 1.0f;   // Local variables for grass

    // Use this for initialization
    void Start()
    {
        GrassTerrain = GetComponent<Terrain>();

        Details = GrassTerrain.terrainData.GetDetailLayer(0, 0, GrassTerrain.terrainData.detailWidth,   // Gets the terrain component and then grabs the width and height detail
            GrassTerrain.terrainData.detailHeight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GrassTimer > GrassTicks)
        {
            GrassGrowth();
            GrassTerrain.terrainData.SetDetailLayer(0, 0, 0, Details);   // Grows the grass
        }

        GrassTimer += Time.deltaTime;
    }

    void GrassGrowth()
    {
        var ZAxis = Random.Range(0, 2000);    // Random array range for Z and X for the grass spawn
        var XAxis = Random.Range(0, 2000);

        if (GrassTerrain.GetComponent<Terrain>().SampleHeight(new Vector3(XAxis, 0, ZAxis)) > 35.0f && Details[ZAxis, XAxis] < 16)      // If height is greater than 35.0 and Z and X is less than 16 and one to X and Z. Else grow grass
            Details[ZAxis, XAxis] += 1;
        else
            GrassGrowth();
    }
}