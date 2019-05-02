using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D tCrosshair;

    public float size = 4.0f;

    private void OnGUI()
    {
        float xMin = (Screen.width / 2) - (tCrosshair.width / 2);
        float yMin = (Screen.height / 2) - (tCrosshair.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, size, size), tCrosshair);
    }
}