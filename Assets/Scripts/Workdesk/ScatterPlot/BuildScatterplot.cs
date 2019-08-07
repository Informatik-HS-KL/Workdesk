using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Diese Klasse wird genutzt, um eine Scatterplot Matritzen zu erzeugen und in der Welt darzustellen.
/// </summary>
public class BuildScatterplot : MonoBehaviour
{
    private GameObject dataInput;
    private GameObject scatterplots;
    private GameObject screen;

    private Dropdown scatterplotDropdown;
    private Visualizer visualizerScript;
    private TurningPlate turningPlateScript;

    private bool isActive = true;

    private void Awake()
    {
        screen = GameObject.FindGameObjectWithTag("Screen");
        scatterplots = GameObject.FindGameObjectWithTag("Scatterplots");
        screen.SetActive(false);
        scatterplotDropdown = GameObject.FindGameObjectWithTag("ScatterplotDropdown").GetComponent<Dropdown>();
        visualizerScript = GameObject.FindGameObjectWithTag("Visualizer").GetComponent<Visualizer>();
        turningPlateScript = GameObject.FindGameObjectWithTag("ScatterplotPlate").GetComponent<TurningPlate>();
    }

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        
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
            screen.SetActive(false);
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
            screen.SetActive(true);
        }
    }

    public void switchScatterplot()
    {
        visualizerScript.loadOtherScatterplot(scatterplotDropdown.value);
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {

    }
}
