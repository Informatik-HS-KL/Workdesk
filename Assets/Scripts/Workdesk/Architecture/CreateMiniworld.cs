using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Diese Klasse dient dazu eine Miniaturszene zu erzeugen und sie auf dem Schreibtisch anzuzeigen.
/// </summary>
public class CreateMiniworld : MonoBehaviour
{
    private GameObject architecture = null;
    private GameObject miniPlayer;
    private GameObject vrOrigin;
    private GameObject clipboardArchitecture;
    private GameObject resetButton;
    private FillDesktop fillDesktopScript;
    private Maze mazeScript;

    private bool isActive = true;
    private Vector3 startPos;

    private bool inMaze;

    private void Awake()
    {        
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Grip, ButtonEventType.Click, teleportToMaze);
        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Grip, ButtonEventType.Click, teleportToDesk);
        findArchitectureObject();
        miniPlayer = GameObject.FindGameObjectWithTag("MiniPlayer");
        vrOrigin = GameObject.FindGameObjectWithTag("VROrigin");
        mazeScript = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<Maze>();
        clipboardArchitecture = GameObject.FindGameObjectWithTag("ClipboardArchitecture");
        resetButton = GameObject.FindGameObjectWithTag("ResetButton");
    }

    private void OnDestroy()
    {
        if (isActive)
        {
            ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.Grip, ButtonEventType.Click, teleportToMaze);
            ViveInput.RemoveListenerEx(HandRole.RightHand, ControllerButton.Grip, ButtonEventType.Click, teleportToDesk);
        }
    }

    public void activateListener()
    {
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Grip, ButtonEventType.Click, teleportToMaze);
        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Grip, ButtonEventType.Click, teleportToDesk);
    }

    private void deactivateListener()
    {
        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.Grip, ButtonEventType.Click, teleportToMaze);
        ViveInput.RemoveListenerEx(HandRole.RightHand, ControllerButton.Grip, ButtonEventType.Click, teleportToDesk);
    }

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        inMaze = false;
        startPos = vrOrigin.transform.position;        
    }

    public void activateClipboard()
    {
        clipboardArchitecture.SetActive(true);
        resetButton.SetActive(false);
    }

    public void deactivateClipboard()
    {
        clipboardArchitecture.SetActive(false);
        resetButton.SetActive(true);
    }

    private void findArchitectureObject()
    {
        architecture = GameObject.FindGameObjectWithTag("Architecture");
        fillDesktopScript = transform.GetComponent<FillDesktop>();
    }

    /// <summary>
    /// Methode dient zur Aktivierung der Architekturansicht.
    /// </summary>
    public void activateView()
    {
        if (!isActive)
        {
            activateListener();
            activateClipboard();
            isActive = true;
            if (architecture == null) findArchitectureObject();
            architecture.SetActive(true);
            fillDesktopScript.openCamera();
            mazeScript.buildMaze();
        }
    }

    /// <summary>
    /// Methode dient zur Deaktivierung der Architekturansicht.
    /// </summary>
    public void deactivateView()
    {
        if (isActive)
        {
            teleportToDesk();            
            mazeScript.reset();
            deactivateListener();
            isActive = false;
            if (architecture == null) findArchitectureObject();
            architecture.SetActive(false);
            fillDesktopScript.closeCamera();            
        }
    }

    /// <summary>
    /// Methode zum Versetzen der VR-Kamera in das Labyrinth, an die Stelle, an der das Männchen stand.
    /// Zusätzlich wird die Drehung angepasst.
    /// </summary>
    public void teleportToMaze()    
    {
        if (!inMaze)
        {
            inMaze = true;
            float scaleXZ = GameObject.FindGameObjectWithTag("MiniMaze").transform.GetChild(1).transform.localScale.x;
            float scaleY = GameObject.FindGameObjectWithTag("MiniMaze").transform.GetChild(1).transform.localScale.y;
            Vector3 tempVector = new Vector3((miniPlayer.transform.localPosition.x / scaleXZ), (miniPlayer.transform.localPosition.y / scaleY), (miniPlayer.transform.localPosition.z / scaleXZ));

            vrOrigin.transform.eulerAngles = new Vector3(vrOrigin.transform.eulerAngles.x, GameObject.FindGameObjectWithTag("MiniPlayer").transform.eulerAngles.y - 
                GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.y, vrOrigin.transform.eulerAngles.z);

            vrOrigin.transform.position = GameObject.FindGameObjectWithTag("Maze").transform.position + tempVector;
        }
    }

    /// <summary>
    /// Methode zum Zurücksetzen der VR-Kamera in den Hauptraum.
    /// Zusätzlich wird die Drehung zurückgesetzt.
    /// </summary>
    public void teleportToDesk()
    {
        if (inMaze)
        {
            inMaze = false;
            float scaleXZ = GameObject.FindGameObjectWithTag("MiniMaze").transform.GetChild(1).transform.localScale.x;
            float scaleY = GameObject.FindGameObjectWithTag("MiniMaze").transform.GetChild(1).transform.localScale.y;

            Debug.Log("Änderung Prüfen");
            //Änderung + MainCamera überprüfen!
            //float localXPos = vrOrigin.transform.localPosition.x - GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition.x - GameObject.FindGameObjectWithTag("Maze").transform.position.x;
            //float localXPos = vrOrigin.transform.localPosition.x - GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
            float localXPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x - GameObject.FindGameObjectWithTag("Maze").transform.position.x;
            float localYPos = vrOrigin.transform.localPosition.y - GameObject.FindGameObjectWithTag("Maze").transform.position.y;
            float localZPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z - GameObject.FindGameObjectWithTag("Maze").transform.position.z;
            //float localZPos = vrOrigin.transform.localPosition.z - GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition.z - GameObject.FindGameObjectWithTag("Maze").transform.position.z;
            //float localZPos = vrOrigin.transform.localPosition.z - GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;

            Vector3 tempVector = new Vector3((localXPos * scaleXZ), (localYPos * scaleY), (localZPos * scaleXZ));
            
            miniPlayer.transform.localPosition = tempVector;
            miniPlayer.transform.eulerAngles = new Vector3(miniPlayer.transform.eulerAngles.x, GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.y, miniPlayer.transform.eulerAngles.z);
            vrOrigin.transform.eulerAngles = new Vector3(vrOrigin.transform.eulerAngles.x, 0.0f, vrOrigin.transform.eulerAngles.z);

            vrOrigin.transform.position = startPos;
        }
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {

    }
}