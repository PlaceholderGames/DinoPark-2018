using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HerdAgent : MonoBehaviour {
    Collider2D agentCol;
    public Collider2D AgentCollider { get { return agentCol; } }
	// Use this for initialization
	void Start () {
        agentCol = GetComponent<Collider2D>();
	}

    public void Herd(Vector3 speed)
    {
        transform.forward = speed;
        transform.position += speed * Time.deltaTime;
    }
}
