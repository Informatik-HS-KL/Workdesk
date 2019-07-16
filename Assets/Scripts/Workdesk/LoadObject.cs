using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse wird dazu genutzt, ein Objekt zu laden und es in der Welt zu platzieren.
/// </summary>
public class LoadObject : MonoBehaviour
{
    private GameObject grabbableObjectContainer;
    private GameObject trackerObjectContainer;
    private GameObject turningPlateObjectContainer;

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        grabbableObjectContainer = GameObject.FindGameObjectWithTag("GrabbableObjectContainer");
        trackerObjectContainer = GameObject.FindGameObjectWithTag("TrackerObjectContainer");
        turningPlateObjectContainer = GameObject.FindGameObjectWithTag("TurningPlateObjectContainer");
    }

    /// <summary>
    /// Diese Methode aktiviert den GrabbableObjectContainer und zeigt das Objekt darin an.
    /// Zusätzlich werden die beiden anderen Container deaktiviert.
    /// </summary>
    public void activateGrabbableObjectContainer()
    {
        grabbableObjectContainer.gameObject.SetActive(true);
        trackerObjectContainer.gameObject.SetActive(false);
        turningPlateObjectContainer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Diese Methode aktiviert den TrackerObjectContainer und zeigt das Objekt darin an.
    /// Zusätzlich werden die beiden anderen Container deaktiviert.
    /// </summary>
    public void activateTrackerObjectContainer()
    {
        grabbableObjectContainer.gameObject.SetActive(false);
        trackerObjectContainer.gameObject.SetActive(true);
        turningPlateObjectContainer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Diese Methode aktiviert den TurningPlateObjectContainer und zeigt das Objekt darin an.
    /// Zusätzlich werden die beiden anderen Container deaktiviert.
    /// </summary>
    public void activateTurningPlateObjectContainer()
    {
        grabbableObjectContainer.gameObject.SetActive(false);
        trackerObjectContainer.gameObject.SetActive(false);
        turningPlateObjectContainer.gameObject.SetActive(true);
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {

    }
}
