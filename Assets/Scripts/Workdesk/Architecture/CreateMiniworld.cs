using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse dient dazu eine Miniaturszene zu erzeugen und sie auf dem Schreibtisch anzuzeigen.
/// </summary>
public class CreateMiniworld : MonoBehaviour {

    private GameObject architecture;

    // Wird zur Initialisierung genutzt.
    void Start () {
        architecture = GameObject.FindGameObjectWithTag("Architecture");
	}

    /// <summary>
    /// Methode dient zur Aktivierung der Architekturansicht.
    /// </summary>
    public void activateView()
    {
        architecture.SetActive(true);
    }

    /// <summary>
    /// Methode dient zur Deaktivierung der Architekturansicht.
    /// </summary>
    public void deactivateView()
    {
        architecture.SetActive(false);
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update () {
		
	}
}
