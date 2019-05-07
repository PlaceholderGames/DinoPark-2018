using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    public GameObject raptor;
    public GameObject Ankylosaurus;

    public int raptorPopulation;
    public int ankylosaurusPopulation;

    Terrain terrain;
    void Start()
    {
        SpawnRaptors();
        SpawnAnkies();
    }

    void SpawnRaptors()
    {
        float x = 0.0f, y = 0.0f, z = 0.0f;        
        for (int i = 0; i <raptorPopulation; i++)
        {
            Vector3 spawnPos = new Vector3(x, y, z);
            spawnPos.y += Terrain.activeTerrain.SampleHeight(spawnPos) + 200.0f;
            spawnPos.x = Random.Range(200, 1500);
            spawnPos.z = Random.Range(200, 1500);
            Instantiate(raptor, spawnPos, Quaternion.identity);
        }
    }
    void SpawnAnkies()
    {
        float x = 0.0f, y = 0.0f, z = 0.0f;
        for (int i = 0; i < ankylosaurusPopulation; i++)
        {
            Vector3 spawnPos = new Vector3(x, y, z);
            spawnPos.y += Terrain.activeTerrain.SampleHeight(spawnPos) + 200.0f;
            spawnPos.x = Random.Range(200, 1500);
            spawnPos.z = Random.Range(200, 1500);
            Instantiate(Ankylosaurus, spawnPos, Quaternion.identity);
        }
    }
}
