using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    private Camera cam;
    private GameObject statsCanvas;

    private bool panelIsHidden = true;

    //stores for the text objects
    private Text txtName = null;
    private Text txtHealth = null;
    private Text txtHunger = null;
    private Text txtThirst = null;
    private Text txtEnergy = null;
    private Text txtStatus = null;

    // Use this for initialization
    private void Start ()
    {
        statsCanvas = GameObject.Find("StatsCanvas") as GameObject;
        cam = GameObject.Find("First Person Controller Prefab/Main Camera").GetComponent<Camera>();

        //initialise the panel as hidden
        statsCanvas.SetActive(false);
        panelIsHidden = true;

        //if text objects are null then setup the objects
        if (txtName == null) txtName = statsCanvas.transform.Find("Panel/txtName").GetComponent<Text>();
        if (txtHealth == null) txtHealth = statsCanvas.transform.Find("Panel/txtHealth").GetComponent<Text>();
        if (txtHunger == null) txtHunger = statsCanvas.transform.Find("Panel/txtHunger").GetComponent<Text>();
        if (txtThirst == null) txtThirst = statsCanvas.transform.Find("Panel/txtThirst").GetComponent<Text>();
        if (txtEnergy == null) txtEnergy = statsCanvas.transform.Find("Panel/txtEnergy").GetComponent<Text>();
        if (txtStatus == null) txtStatus = statsCanvas.transform.Find("Panel/txtStatus").GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //check whether we're looking at a dino
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Dinos"))
            {
                if (statsCanvas.activeSelf == false)
                {
                    statsCanvas.SetActive(true);
                    panelIsHidden = false;
                }

                //if the panel is being shown then update the GUI
                updateStatsGUI(hit);
            }
            else
            {
                if (statsCanvas.activeSelf == true)
                {
                    statsCanvas.SetActive(false);
                    panelIsHidden = true;
                }
            }
        }
    }

    private void updateStatsGUI(RaycastHit hit)
    {
        Dinosaur dino = hit.transform.GetComponent<Dinosaur>();
        txtName.text = dino.returnName();
        txtHealth.text = "Health: " + dino.returnHealth().ToString() + "%";
        txtHunger.text = "Hunger: " + dino.returnHunger().ToString() + "%";
        txtThirst.text = "Thirst: " + dino.returnThirst().ToString() + "%";
        txtEnergy.text = "Energy: " + dino.returnEnergy().ToString() + "%";
        txtStatus.text = dino.returnTextStatus();
    }
}