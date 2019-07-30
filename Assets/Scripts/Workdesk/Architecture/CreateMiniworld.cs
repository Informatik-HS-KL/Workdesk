using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse dient dazu eine Miniaturszene zu erzeugen und sie auf dem Schreibtisch anzuzeigen.
/// </summary>
public class CreateMiniworld : MonoBehaviour
{

    private GameObject architecture = null;
    private FillDesktop fillDesktopScript;

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        findArchitectureObject();
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
        if (architecture == null) findArchitectureObject();
        architecture.SetActive(true);
        fillDesktopScript.openCamera();
    }

    /// <summary>
    /// Methode dient zur Deaktivierung der Architekturansicht.
    /// </summary>
    public void deactivateView()
    {
        if (architecture == null) findArchitectureObject();
        architecture.SetActive(false);
        fillDesktopScript.closeCamera();
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {

    }
}