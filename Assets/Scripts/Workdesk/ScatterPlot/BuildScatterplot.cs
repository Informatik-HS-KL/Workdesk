using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse wird genutzt, um eine Scatterplot Matritzen zu erzeugen und in der Welt darzustellen.
/// </summary>
public class BuildScatterplot : MonoBehaviour
{
    private GameObject dataInput;
    private GameObject scatterplots;

    private bool isActive = true;
    // Wird zur Initialisierung genutzt.
    void Start()
    {
        dataInput = GameObject.FindGameObjectWithTag("DataInput");
        scatterplots = GameObject.FindGameObjectWithTag("Scatterplots");
    }

    /// <summary>
    /// Methode dient zum Aktivieren und Anzeigen der Scatterplot Ansicht.
    /// </summary>
    public void activateView()
    {
        if (!isActive)
        {
            isActive = true;
            scatterplots.SetActive(true);
        }
    }

    /// <summary>
    /// Methode dient zum Deaktivieren der Scatterplot Ansicht.
    /// </summary>
    public void deactivateView()
    {
        if (isActive)
        {
            isActive = false;
            scatterplots.SetActive(false);
        }
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {

    }
}
