using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidGoal : MonoBehaviour {

    [SerializeField]
    public Terrain terrainData;

    private void Start()
    {
        InvokeRepeating("RandomPosition", 1, Random.Range(7, 15));
    }

    void RandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(200, 1800), 0, Random.Range(200, 1800));
       randomPosition.y = Terrain.activeTerrain.SampleHeight(randomPosition);

        gameObject.transform.position = randomPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dinos")
        {
            RandomPosition();
        }
    }
}
