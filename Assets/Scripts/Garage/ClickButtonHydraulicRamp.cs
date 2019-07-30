using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Diese Klasse steuert die Interaktion der Knöpfe zum bewegen der Hebebühne.
/// </summary>
public class ClickButtonHydraulicRamp : MonoBehaviour
{
    public GameObject buttonObject;
    public Vector3 buttonDownDisplacement;

    private float startPosY;
    private float endPosY;
    private bool exit;
    private bool stay;

    // Wird zur Initialisierung genutzt.
    private void Start()
    {
        exit = false;
        stay = false;
        startPosY = buttonObject.transform.localPosition.y;
        endPosY = startPosY + buttonDownDisplacement.y;

        GameObject.FindGameObjectWithTag("GameController").GetComponent<MoveHydraulicRamp>().savePositions();
    }

    /// <summary>
    /// Methode die aufgerufen wird, wenn wir mit dem Controller den Button berühren (für längere Zeit).
    /// Sofern clickable auf false gesetzt wurde.
    /// </summary>
    /// <param name="collider">Das Objekt, das mit dem Collider in Berührung kommt.</param>
    /// <param name="name">Name des berührten Objektes.</param>
    public void myTriggerStay(Collider collider, string name)
    {
        stay = true;
        if (buttonObject.transform.localPosition.y > endPosY) buttonObject.transform.localPosition = buttonObject.transform.localPosition + new Vector3(0f, -0.01f, 0f);
        else
        {
            buttonObject.transform.localPosition = new Vector3(buttonObject.transform.localPosition.x, endPosY, buttonObject.transform.localPosition.z);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<MoveHydraulicRamp>().moveCar(name);
        }
    }

    /// <summary>
    /// Methode die aufgerufen wird, wenn wir mit dem Controller den Button wieder verlassen.
    /// Der Knopf geht so an seine Ursprungsposition zurück.
    /// </summary>
    /// <param name="collider">Das Objekt, das mit dem Collider in Berührung kam.</param>
    public void myTriggerExit(Collider collider, string name)
    {
        exit = true;
        stay = false;
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {
        if (exit && !stay)
        {
            if (buttonObject.transform.localPosition.y < startPosY) buttonObject.transform.localPosition = buttonObject.transform.localPosition + new Vector3(0f, 0.01f, 0f);
            else
            {
                buttonObject.transform.localPosition = new Vector3(buttonObject.transform.localPosition.x, startPosY, buttonObject.transform.localPosition.z);
                exit = false;
            }
        }
    }
}