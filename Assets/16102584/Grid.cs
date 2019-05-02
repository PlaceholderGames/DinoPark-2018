using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField]
    private Terrain terrain;

    private float maxHeight = 151.0236f;

    // Use this for initialization
    void Start () {
        //terrainData = terrain.terrainData;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        for (int y = 0; y < terrain.terrainData.size.x; y+=8)
        {
            for (int x = 0; x < terrain.terrainData.size.z; x+=8)
            {
                Vector3 vec = new Vector3(x, 0, y);
                Vector3 tempVec = vec - terrain.transform.position;
                tempVec.y = terrain.SampleHeight(tempVec);
                float xval = x / terrain.terrainData.size.x;
                float yval = y / terrain.terrainData.size.z;
                float gradient = terrain.terrainData.GetSteepness(xval, yval);
                if (tempVec.y > maxHeight)
                    maxHeight = tempVec.y;
                Gizmos.color = Color.Lerp(Color.yellow, Color.magenta, (tempVec.y / maxHeight));
                if (tempVec.y <= 34.75)
                    Gizmos.color = Color.cyan;
                if (gradient >= 28) Gizmos.color = Color.red;
                Gizmos.DrawWireCube(tempVec, new Vector3(8, 3, 8));
            }
        }
    }
}
