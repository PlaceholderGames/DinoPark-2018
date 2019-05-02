#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class EditorSettings : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!EditorApplication.isPlaying)
        {
            SceneView.currentDrawingSceneView.camera.farClipPlane = 10000000;
            SceneView.currentDrawingSceneView.camera.nearClipPlane = 0;
        }
    }
}

#endif