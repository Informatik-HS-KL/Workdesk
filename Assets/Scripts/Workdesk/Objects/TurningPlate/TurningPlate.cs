using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse dient zur Interaktion mit der Platte. Zusätzlich wird das Skript "TurningPlateCollider" benötigt.
/// </summary>
public class TurningPlate : MonoBehaviour
{
    [SerializeField]
    new TurningPlateCollider collider;

    [SerializeField]
    float xAngle = 0f;

    [SerializeField]
    float yAngle = 0f;

    [SerializeField]
    float zAngle = 0f;

    public bool activated3D;
    private bool firstTime;

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        firstTime = true;
        activated3D = false;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<FillDesktop>().writeOnScreen(activated3D);
    }

    public void resetPosition()
    {
        Debug.Log("RESET POSITION");
        xAngle = 0f;
        yAngle = 0f;
        zAngle = 0f;
    }

    /// <summary>
    /// Diese Methode dient zum Umschalten der TurningPlate zwischen 2D und 3D Modus.
    /// </summary>
    public void toggle3D()
    {
        if (firstTime)
        {
            firstTime = false;
        }
        else
        {
            if (activated3D) activated3D = false;
            else activated3D = true;
        }

        GameObject.FindGameObjectWithTag("GameController").GetComponent<FillDesktop>().writeOnScreen(activated3D);
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {
        if (collider.getXAngle() != 0f) xAngle += collider.getXAngle();
        if (collider.getYAngle() != 0f) yAngle += collider.getYAngle();
        if (collider.getZAngle() != 0f) zAngle += collider.getZAngle();

        if (!activated3D)
        {
            xAngle = 0f;
            //yAngle = 0f;
            zAngle = 0f;
        }

        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0f);
    }
}
