using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herd : MonoBehaviour {

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    int size = 15;

    [SerializeField]
    float spawnRadius = 5;

    private List<GameObject> HerdMembers;
    private Vector3 position;

	// Use this for initialization
	void Start () {

        HerdMembers = new List<GameObject>();
        position = transform.position;
        gameObject.AddComponent<Agent>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.position = position;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            position += Random.onUnitSphere * spawnRadius;
            position.y = transform.position.y;

            rb.AddForce(Random.onUnitSphere * 5, ForceMode.Impulse);
            HerdMembers.Add(obj);
        }
	}
}
