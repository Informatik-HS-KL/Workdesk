using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Diese Klasse wird genutzt, um eine Scatterplot Matritzen zu erzeugen und in der Welt darzustellen.
/// Zusätzlich dazu werden die erzeugten Scatterplot beim deaktivieren der Ansicht wieder gelöscht.
/// </summary>
public class BuildScatterplot : MonoBehaviour
{
    private GameObject dataInput;
    private GameObject scatterplots;
    private GameObject vrOrigin;

    private Dropdown scatterplotDropdown;
    private Visualizer visualizerScript;
    private Visualizer bigVisualizerScript;
    private TurningPlate turningPlateScript;

    private GameObject bigScatterplotHolder;
    private GameObject smallScatterplotHolder;

    private bool isActive = true;
    private bool inScatterplotRoom;

    private Vector3 startPos;
    private Vector3 scatterplotRoomPos;

    private bool init = false;

    private void Awake()
    {
        vrOrigin = GameObject.FindGameObjectWithTag("VROrigin");
        scatterplots = GameObject.FindGameObjectWithTag("Scatterplots");
        bigScatterplotHolder = GameObject.FindGameObjectWithTag("bigScatterplot");
        smallScatterplotHolder = GameObject.FindGameObjectWithTag("Visualizer");
        scatterplotDropdown = GameObject.FindGameObjectWithTag("ScatterplotDropdown").GetComponent<Dropdown>();
        visualizerScript = GameObject.FindGameObjectWithTag("Visualizer").GetComponent<Visualizer>();
        bigVisualizerScript = GameObject.FindGameObjectWithTag("bigScatterplot").GetComponent<Visualizer>();
        turningPlateScript = GameObject.FindGameObjectWithTag("ScatterplotPlate").GetComponent<TurningPlate>();
    }

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        inScatterplotRoom = false;
        scatterplotRoomPos = new Vector3(-8.0f, 0.0f, -10.0f);
        startPos = vrOrigin.transform.position;
        activateListener();

        init = true;
        visualizerScript.create(scatterplotDropdown.value);
        bigVisualizerScript.create(scatterplotDropdown.value);
    }

    /// <summary>
    /// Methode dient zum Aktivieren von zwei Listenern.
    /// </summary>
    public void activateListener()
    {
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Grip, ButtonEventType.Click, teleportToScatterplotRoom);
        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Grip, ButtonEventType.Click, teleportToDesk);
    }

    /// <summary>
    /// Methode zum Setzen der VR-Kamera in den Scatterplotraum.
    /// </summary>
    private void teleportToScatterplotRoom()
    {
        Debug.Log("TeleportToRoom:  " + inScatterplotRoom);
        if (!inScatterplotRoom)
        {
            inScatterplotRoom = true;
            vrOrigin.transform.position = scatterplotRoomPos;
        }
    }

    /// <summary>
    /// Methode zum Zurücksetzen der VR-Kamera in den Hauptraum.
    /// </summary>
    private void teleportToDesk()
    {
        Debug.Log("TeleportToDesk:  " + inScatterplotRoom);
        if (inScatterplotRoom)
        {
            inScatterplotRoom = false;
            vrOrigin.transform.position = startPos;
        }
    }

    /// <summary>
    /// Methode zum Anzeigen des ausgewählten Scatterplots.
    /// </summary>
    public void switchScatterplot()
    {
        if (init)
        {
            Debug.Log("switchScatterplot():  " + scatterplotDropdown.value);
            visualizerScript.create(scatterplotDropdown.value);
            bigVisualizerScript.create(scatterplotDropdown.value);
        }
    }

    /// <summary>
    /// Methode dient zum zerstören der angezeigten Scatterplots.
    /// Wird beim deaktivieren der Ansicht aufgerufen.
    /// </summary>
    private void destroyScatterplots()
    {
        foreach (Transform child in bigScatterplotHolder.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in smallScatterplotHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }
}