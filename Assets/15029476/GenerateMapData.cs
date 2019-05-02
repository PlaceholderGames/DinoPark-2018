using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapData : MonoBehaviour
{
    [SerializeField]
    private bool showDebugInfo = false;

    [Header("Gap between map nodes with world position")]
    [SerializeField]
    private int sampleStep = 8;

    public enum TerrainTypes { GRASS, DIRT, WATER };

    private GameObject terrainGO;
    private Terrain terrain;

    public struct MapNode
    {
        public Vector3 position;
        public int terrainType;

        //variables used for A* search
        public Vector3 ParentPos;
        public float gCost; //cost from the start
        public float hCost; //cost to destination
    }

    //2D list to store all the map nodes
    private List<List<MapNode>> mapNodes = new List<List<MapNode>>();

    void OnDrawGizmos()
    {
        if (showDebugInfo)
        {
            for (int x = 0; x < mapNodes.Count; x++)
            {
                for (int z = 0; z < mapNodes[x].Count; z++)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(mapNodes[x][z].position, new Vector3(mapNodes[x][z].position.x, mapNodes[x][z].position.y + 1, mapNodes[x][z].position.z));
                }
            }
        }
    }

    // Use this for initialization
    private void Start()
    {
        //find the terrain data
        terrainGO = GameObject.Find("Terrain");
        terrain = terrainGO.GetComponent<Terrain>();

        //loop through the entire terrain data, generating
        int xPos = 0, zPos = 0;
        for (int x = 0; x < terrain.terrainData.size.x; x += sampleStep)
        {
            mapNodes.Add(new List<MapNode>());

            for (int z = 0; z < terrain.terrainData.size.z; z += sampleStep)
            {
                MapNode n = new MapNode();
                n.position = new Vector3(x, terrain.SampleHeight(new Vector3(xPos, 0, zPos) * sampleStep), z);
                n.ParentPos = Vector3.zero;

                mapNodes[xPos].Add(n);

                zPos++;
            }
            zPos = 0;
            xPos++;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public int returnSampleStep()
    {
        return sampleStep;
    }

    public List<List<MapNode>> returnMapNodes()
    {
        return mapNodes;
    }

    public void setParentPos(Vector3 currentPos, Vector3 parentPos)
    {
        MapNode n = mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)];
        n.ParentPos = parentPos;

        mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)] = n;
    }

    public void setGCost(Vector3 currentPos, float gCost)
    {
        MapNode n = mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)];
        n.gCost = gCost;

        mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)] = n;
    }

    public void setHCost(Vector3 currentPos, float hCost)
    {
        MapNode n = mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)];
        n.hCost = hCost;

        mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)] = n;
    }

    public float getGCost(Vector3 currentPos)
    {
        return mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)].gCost;
    }

    public float getHCost(Vector3 currentPos)
    {
        return mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)].hCost;
    }

    public float getGHCost(Vector3 currentPos)
    {
        MapNode n = mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)];

        return n.gCost + n.hCost;
    }

    public Vector3 getParentPos(Vector3 currentPos)
    {
        return mapNodes[Mathf.RoundToInt(currentPos.x / sampleStep)][Mathf.RoundToInt(currentPos.z / sampleStep)].ParentPos;
    }

    public Vector3 returnTerrainSize()
    {
        return terrain.terrainData.size;
    }

    public void resetMapNode(List<Vector3> optimalRoute)
    {
        MapNode n = new MapNode();
        for (int i = 0; i < optimalRoute.Count; i++)
        {
            n = mapNodes[Mathf.RoundToInt(optimalRoute[i].x / sampleStep)][Mathf.RoundToInt(optimalRoute[i].z / sampleStep)];
            n.ParentPos = Vector3.zero;
            n.gCost = 0;
            n.hCost = 0;

            mapNodes[Mathf.RoundToInt(optimalRoute[i].x / sampleStep)][Mathf.RoundToInt(optimalRoute[i].z / sampleStep)] = n;
        }
    }
}