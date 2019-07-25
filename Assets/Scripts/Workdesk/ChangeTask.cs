using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse dient zum Umschalten der drei verschiedenen Ansichten, 
/// die auf dem Schreibtisch dargestellt werden können.
/// </summary>
public class ChangeTask : MonoBehaviour
{
    private LoadObject loadObjectScript;
    private ReadData readDataScript;
    private CreateMiniworld createMiniworldScript;

    // Use this for initialization
    void Start()
    {
        loadObjectScript = transform.GetComponent<LoadObject>();
        readDataScript = transform.GetComponent<ReadData>();
        createMiniworldScript = transform.GetComponent<CreateMiniworld>();

        activateObjectView();
    }

    /// <summary>
    /// Methode dient zum Aktivieren der Objektansicht.
    /// </summary>
    private void activateObjectView()
    {
        loadObjectScript.activateView();
        readDataScript.deactivateView();
        createMiniworldScript.deactivateView();
    }

    /// <summary>
    /// Methode dient zum Aktivieren der Scatterplotansicht.
    /// </summary>
    private void activateScatterplotView()
    {
        loadObjectScript.deactivateView();
        readDataScript.activateView();
        createMiniworldScript.deactivateView();
    }

    /// <summary>
    /// Methode dient zum Aktivieren der ArchitekturAnsicht.
    /// </summary>
    private void activateArchitectureView()
    {
        loadObjectScript.deactivateView();
        readDataScript.deactivateView();
        createMiniworldScript.activateView();
    }

    // Update is called once per frame
    void Update()
    {

    }
}