using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainReset : MonoBehaviour {

    public int[,] terrainDetail;
    private Terrain t;

	// Use this for initialization
	void Start () {
        t = GameObject.Find("Terrain").GetComponent<Terrain>();
        terrainDetail = t.terrainData.GetDetailLayer(0, 0, t.terrainData.detailWidth, t.terrainData.detailHeight, 0);
    }

    void OnDestroy()
    {
        resetTerrain();
    }

    void resetTerrain()
    {
        t.terrainData.SetDetailLayer(0, 0, 0, terrainDetail);
    }
}
