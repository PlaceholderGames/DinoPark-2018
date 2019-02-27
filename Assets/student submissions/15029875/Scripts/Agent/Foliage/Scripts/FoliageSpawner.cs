using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class exists to spawn foliage at a random location specified by the
// controller it's attached to.
public class FoliageSpawner : MonoBehaviour {

    [SerializeField]
    private float x, z;

    [SerializeField]
    private float x1, z1;

    [SerializeField]
    private int amount;

    [SerializeField]
    private GameObject foliage0;
    [SerializeField]
    private GameObject foliage1;

    [SerializeField]
    private Terrain terrainData;

    List<GameObject> foliageSelection = new List<GameObject>();


    // Use this for initialization
    void Start () {
        SpawnFoliage(amount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Spawn foliage with an amount x.
    void SpawnFoliage(int amount)
    {
        // Spawn the foliage around a certain radius.
        foliageSelection.Add(foliage0);
        foliageSelection.Add(foliage1);

        for (int i = 0; i < amount; i ++)
        {
            int foliagePrefabIndex = UnityEngine.Random.Range(0, foliageSelection.Count);

            // Set a random position between clamped variables.
            var xPosition = Random.Range(x, x1);
            var zPosition = Random.Range(z, z1);

            // By using SampleHeight we can snap the foliage to the y axis of the terrain.
            Vector3 foliagePosition = new Vector3(xPosition, 0, zPosition);
            foliagePosition.y = Terrain.activeTerrain.SampleHeight(foliagePosition);

            GameObject foliage = Instantiate(foliageSelection[foliagePrefabIndex], foliagePosition, Quaternion.identity);
        }
    }
}
