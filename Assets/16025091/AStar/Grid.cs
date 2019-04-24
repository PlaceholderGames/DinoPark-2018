using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public LayerMask unwalkableMask;
    Node[,] grid;

    public Vector2 gridWorldSize;
    public float nodeRadius;

    Terrain terrain;

    float nodeDiameter;
    Vector2Int gridSize;

    Vector3 terrainCenter;
    float seaLevel = 20.0f;

    private void Start()
    {
        gridWorldSize = new Vector3(Terrain.activeTerrain.terrainData.size.x, Terrain.activeTerrain.terrainData.size.z);
        terrainCenter = new Vector3(transform.position.x + Terrain.activeTerrain.terrainData.size.x / 2, 1.0f, transform.position.z + Terrain.activeTerrain.terrainData.size.z / 2);
        nodeDiameter = nodeRadius * 2;
        gridSize = new Vector2Int(Mathf.RoundToInt(gridWorldSize.x / nodeDiameter), Mathf.RoundToInt(gridWorldSize.y / nodeDiameter));
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSize.x, gridSize.y];

        Vector3 worldBottomLeft = terrainCenter - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                worldPoint.y = Terrain.activeTerrain.SampleHeight(worldPoint);
                bool walkable = (worldPoint.y > seaLevel);
                // = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);
                grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(terrainCenter, new Vector3(gridWorldSize.x, 1.0f, gridWorldSize.y));

        if (grid !=null)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = n.walkable ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }

}
