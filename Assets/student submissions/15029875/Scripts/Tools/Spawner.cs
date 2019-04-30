using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class exists to spawn an agent at a random location specified by the
// controller it's attached to.
public class Spawner : MonoBehaviour {

    [SerializeField]
    private float x, z;

    [SerializeField]
    private float x1, z1;

    [SerializeField]
    private int amount;

    [SerializeField]
    private GameObject[] prefabs;

    [SerializeField]
    private Terrain terrainData;

    void Start () {
        Spawn(amount);
	}

    // Spawn prefab with an amount x.
    void Spawn(int amount)
    {
        for (int i = 0; i < amount; i ++)
        {
            int prefabIndex = UnityEngine.Random.Range(0, prefabs.Length);

            // Set a random position between clamped variables.
            var xPosition = Random.Range(x, x1);
            var zPosition = Random.Range(z, z1);

            // By using SampleHeight we can snap the agent to the y axis of the terrain.
            Vector3 prefabPosition = new Vector3(xPosition, 0, zPosition);
            prefabPosition.y = Terrain.activeTerrain.SampleHeight(prefabPosition);

            // Randomize the rotation across the x axis.
            var yRotation = Random.Range(0, 180);

            GameObject prefab = Instantiate(prefabs[prefabIndex], prefabPosition, Quaternion.Euler(0.0f, yRotation, 0.0f));
        }
    }
}
