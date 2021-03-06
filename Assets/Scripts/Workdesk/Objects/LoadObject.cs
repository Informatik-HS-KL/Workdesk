﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Diese Klasse wird dazu genutzt, ein Objekt zu laden und es in der Welt zu platzieren.
/// </summary>
public class LoadObject : MonoBehaviour
{
    private GameObject grabbableObjectContainer;
    private GameObject trackerObjectContainer;
    private GameObject turningPlateObjectContainer;
    private GameObject objectsContainer;

    private Dropdown objectDropdown;

    private List<GameObject> objectList = new List<GameObject>();

    private int chosenObject;
    private int activatedObjectContainer;

    private bool objectLoaded;
    private bool isActive = false;

    private void Awake()
    {
        grabbableObjectContainer = GameObject.FindGameObjectWithTag("GrabbableObjectContainer");
        trackerObjectContainer = GameObject.FindGameObjectWithTag("TrackerObjectContainer");
        turningPlateObjectContainer = GameObject.FindGameObjectWithTag("TurningPlateObjectContainer");
        objectsContainer = GameObject.FindGameObjectWithTag("Objects");

        objectDropdown = GameObject.FindGameObjectWithTag("ObjectDropdown").GetComponent<Dropdown>();
    }

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        chosenObject = 0;
        objectDropdown.value = chosenObject;
        activatedObjectContainer = 1;
        objectLoaded = false;

        GameObject[] tempObjects = Resources.LoadAll<GameObject>("Objects/ShowObjects");
        foreach (GameObject go in tempObjects) objectList.Add(go);

        GameObject.FindGameObjectWithTag("GameController").GetComponent<FillDesktop>().setObject(objectList[chosenObject].name);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<FillDesktop>().setMode("Turn");
    }

    /// <summary>
    /// Methode dient zur Aktiverung der Objectansicht.
    /// </summary>
    public void activateView()
    {
        if (!isActive)
        {
            isActive = true;
            objectsContainer.SetActive(true);
            loadObjectInContainer(1);
            activateTurningPlateObjectContainer();
        }
    }

    /// <summary>
    /// Methode dient zur Deaktivierung der Objectansicht.
    /// </summary>
    public void deactivateView()
    {
        if (isActive)
        {
            isActive = false;
            activateContainer("TurnButton");
            deactivateAllContainers();
            chosenObject = 0;
            objectDropdown.value = chosenObject;
            activatedObjectContainer = 1;
            objectsContainer.SetActive(false);
        }
    }

    /// <summary>
    /// Diese Methode aktiviert den GrabbableObjectContainer und zeigt das Objekt darin an.
    /// Zusätzlich werden die beiden anderen Container deaktiviert.
    /// </summary>
    public void activateGrabbableObjectContainer()
    {
        grabbableObjectContainer.gameObject.SetActive(true);
        trackerObjectContainer.gameObject.SetActive(false);
        turningPlateObjectContainer.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// Diese Methode aktiviert den TrackerObjectContainer und zeigt das Objekt darin an.
    /// Zusätzlich werden die beiden anderen Container deaktiviert.
    /// </summary>
    public void activateTrackerObjectContainer()
    {
        grabbableObjectContainer.gameObject.SetActive(false);
        trackerObjectContainer.gameObject.SetActive(true);
        turningPlateObjectContainer.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// Diese Methode aktiviert den TurningPlateObjectContainer und zeigt das Objekt darin an.
    /// Zusätzlich werden die beiden anderen Container deaktiviert.
    /// </summary>
    public void activateTurningPlateObjectContainer()
    {
        grabbableObjectContainer.gameObject.SetActive(false);
        trackerObjectContainer.gameObject.SetActive(false);
        turningPlateObjectContainer.transform.parent.gameObject.SetActive(true);
    }

    /// <summary>
    /// Methode um alle Container zu deaktivieren, sobald ein anderer Task ausgewählt wird.
    /// </summary>
    public void deactivateAllContainers()
    {
        grabbableObjectContainer.gameObject.SetActive(false);
        trackerObjectContainer.gameObject.SetActive(false);
        turningPlateObjectContainer.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// Methode zum Ändern des ausgewählten Objektes.
    /// Wird aus dem GameController aufgerufen.
    /// </summary>
    /// <param name=""></param>
    public void switchChosenObject()
    {
        chosenObject = objectDropdown.value;
        reloadObject();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<FillDesktop>().setObject(objectList[chosenObject].name);
    }

    /// <summary>
    /// Methode zur Aktivierung der verschiedenen Interaktionsmöglichkeiten.
    /// </summary>
    /// <param name="buttonName">Name des betätigten Knopfes.</param>
    public void activateContainer(string buttonName)
    {
        string mode = "";
        switch (buttonName)
        {
            case "TurnButton":
                activateTurningPlateObjectContainer();
                if (turningPlateObjectContainer.transform.parent.gameObject.activeSelf == true)
                {
                    turningPlateObjectContainer.transform.parent.gameObject.GetComponent<TurningPlate>().toggle3D();
                }
                unloadObjects();
                loadObjectInContainer(1);
                mode = "Turn";
                break;
            case "TrackButton":
                activateTrackerObjectContainer();
                unloadObjects();
                loadObjectInContainer(2);
                mode = "Tracker";
                break;
            case "GrabButton":
                activateGrabbableObjectContainer();
                unloadObjects();
                loadObjectInContainer(3);
                mode = "Grab";
                break;
            case "TeleportButton":
                unloadObjects();
                transform.GetComponent<ChangeScene>().loadScene(chosenObject);
                break;
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<FillDesktop>().setMode(mode);
    }

    /// <summary>
    /// Methode zum Laden des ausgewählten Objektes und Einfügen an den richtigen Objekt Container.
    /// </summary>
    /// <param name="objContainer"></param>
    private void loadObjectInContainer(int objContainer)
    {
        activatedObjectContainer = objContainer;
        if (!objectLoaded)
        {
            GameObject tempObj = objectList[chosenObject].transform.gameObject;

            if (objContainer == 1)  //Turning Table
            {
                Vector3 turningPlatePos = turningPlateObjectContainer.transform.parent.transform.parent.transform.position;
                turningPlatePos = turningPlatePos + tempObj.transform.position;
                GameObject clonedObject = Instantiate(tempObj, turningPlatePos, Quaternion.identity) as GameObject;                
                clonedObject.transform.parent = turningPlateObjectContainer.transform;
                clonedObject.transform.rotation = new Quaternion(0f,0f,0f,0f);
            }
            else if (objContainer == 2) //Tracker
            {
                Vector3 trackerPos = trackerObjectContainer.transform.parent.transform.position;
                GameObject clonedObject = Instantiate(tempObj, trackerPos, Quaternion.identity) as GameObject;
                clonedObject.transform.parent = trackerObjectContainer.transform;
            }
            else if (objContainer == 3) //Grab
            {
                GameObject clonedObject = Instantiate(tempObj, new Vector3(0.2f, 0.8f, 0.2f), Quaternion.identity) as GameObject;
                clonedObject.transform.parent = grabbableObjectContainer.transform;
                clonedObject.AddComponent<BasicGrabbable>();
            }
            else Debug.Log("Fehler bei der Auswahl des ObjectContainers!");
            objectLoaded = true;
        }
    }

    /// <summary>
    /// Methode dient zum erneuten Laden eines Objektes, nachdem der User ein anderes ausgewählt hat.
    /// </summary>
    private void reloadObject()
    {
        unloadObjects();
        loadObjectInContainer(activatedObjectContainer);
    }

    /// <summary>
    /// Methode zum Zerstören der geladenen Objekte um Platz für ein nächstes zu machen.
    /// </summary>
    private void unloadObjects()
    {
        objectLoaded = false;
        if (activatedObjectContainer == 1) Destroy(turningPlateObjectContainer.transform.GetChild(0).gameObject);
        else if (activatedObjectContainer == 2) Destroy(trackerObjectContainer.transform.GetChild(0).gameObject);
        else if (activatedObjectContainer == 3) Destroy(grabbableObjectContainer.transform.GetChild(0).gameObject);
    }
}