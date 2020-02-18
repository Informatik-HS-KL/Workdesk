using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

public class ObjectController : MonoBehaviour
{
    private GameObject grabbableObjectContainer;
    private GameObject trackerObjectContainer;
    private GameObject turningPlateObjectContainer;
    private List<GameObject> objectList = new List<GameObject>();
    private ClipBoardController clipBoardController;
    private int chosenObject;
    private int activatedObjectContainer;
    private Dropdown dropdown;
    private bool objectLoaded;

    // Start is called before the first frame update
    void Start()
    {
        grabbableObjectContainer = GameObject.FindGameObjectWithTag("GrabbableObjectContainer");
        trackerObjectContainer = GameObject.FindGameObjectWithTag("TrackerObjectContainer");
        turningPlateObjectContainer = GameObject.FindGameObjectWithTag("TurningPlateObjectContainer");

        clipBoardController = (new GameObject("ClipboardController")).AddComponent<ClipBoardController>();
        dropdown = GameObject.FindGameObjectWithTag("ObjectDropdown").GetComponent<Dropdown>();

        objectLoaded = false;
        chosenObject = 0;
        dropdown.value = chosenObject;
        activatedObjectContainer = 1;
        activateTurningPlateObjectContainer();

        saveObjectsFromFolder();
        clipBoardController.populateDropdownList(dropdown, objectList);
        loadObjectInContainer(activatedObjectContainer);
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

    // Update is called once per frame
    void Update()
    {

    }

    void saveObjectsFromFolder()
    {
        GameObject[] tempObjects = Resources.LoadAll<GameObject>("Objects/ShowObjects");
        foreach (GameObject gameObject in tempObjects)
        {
            objectList.Add(gameObject);
        }

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
                Vector3 turningPlatePos = turningPlateObjectContainer.transform.position;
                GameObject clonedObject = Instantiate(tempObj, turningPlatePos, Quaternion.identity) as GameObject;
                clonedObject.transform.parent = turningPlateObjectContainer.transform;
                clonedObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else if (objContainer == 2) //Tracker
            {
                Vector3 trackerPos = trackerObjectContainer.transform.position;
                GameObject clonedObject = Instantiate(tempObj, trackerPos, Quaternion.identity) as GameObject;
                clonedObject.transform.parent = trackerObjectContainer.transform;
            }
            else if (objContainer == 3) //Grab
            {
                Vector3 containerPos = grabbableObjectContainer.transform.position;
                GameObject clonedObject = Instantiate(tempObj, containerPos, Quaternion.identity) as GameObject;
                clonedObject.AddComponent<BasicGrabbable>();
                clonedObject.transform.parent = grabbableObjectContainer.transform;
                clonedObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else Debug.Log("Fehler bei der Auswahl des ObjectContainers!");
            objectLoaded = true;
        }
    }

    public void switchObject()
    {
        chosenObject = dropdown.value;
        Debug.Log("Reload Object" + chosenObject);
        unloadObjects();
        loadObjectInContainer(activatedObjectContainer);
    }

    /// <summary>
    /// Methode zur Aktivierung der verschiedenen Interaktionsmöglichkeiten.
    /// </summary>
    /// <param name="buttonName">Name des betätigten Knopfes.</param>
    public void activateContainer(string buttonName)
    {
        switch (buttonName)
        {
            case "TurnButton":
                Debug.Log("TurnButton");
                activateTurningPlateObjectContainer();
                if (turningPlateObjectContainer.transform.parent.gameObject.activeSelf == true)
                {
                    turningPlateObjectContainer.transform.parent.gameObject.GetComponent<TurningPlate>().toggle3D();
                }
                unloadObjects();
                loadObjectInContainer(1);
                break;
            case "TrackButton":
                Debug.Log("TrackButton");
                activateTrackerObjectContainer();
                unloadObjects();
                loadObjectInContainer(2);
                break;
            case "GrabButton":
                Debug.Log("GrabButton");
                activateGrabbableObjectContainer();
                unloadObjects();
                loadObjectInContainer(3);
                break;
            case "TeleportButton":
                unloadObjects();
                transform.GetComponent<ChangeScene>().loadScene(chosenObject);
                break;
        }
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
