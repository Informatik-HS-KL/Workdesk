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
    private BuildScatterplot buildScatterplotScript;
    private CreateMiniworld createMiniworldScript;

    // Use this for initialization
    void Start()
    {
        loadObjectScript = transform.GetComponent<LoadObject>();
        buildScatterplotScript = transform.GetComponent<BuildScatterplot>();
        createMiniworldScript = transform.GetComponent<CreateMiniworld>();

        activateScatterplotView();
    }

    /// <summary>
    /// Methode dient zum Aktivieren der Objektansicht.
    /// </summary>
    private void activateObjectView()
    {
        loadObjectScript.activateView();
        buildScatterplotScript.deactivateView();
        createMiniworldScript.deactivateView();
    }

    /// <summary>
    /// Methode dient zum Aktivieren der Scatterplotansicht.
    /// </summary>
    private void activateScatterplotView()
    {
        loadObjectScript.deactivateView();
        buildScatterplotScript.activateView();
        createMiniworldScript.deactivateView();
    }

    /// <summary>
    /// Methode dient zum Aktivieren der ArchitekturAnsicht.
    /// </summary>
    private void activateArchitectureView()
    {
        loadObjectScript.deactivateView();
        buildScatterplotScript.deactivateView();
        createMiniworldScript.activateView();
    }

    // Update is called once per frame
    void Update()
    {

    }
}