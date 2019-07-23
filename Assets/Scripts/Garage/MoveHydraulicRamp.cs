using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse wird dazu genutzt, um die Hebebühne und das Auto zu bewegen. 
/// Zusätzlich kann hier das Licht der Garage gesteuert werden.
/// </summary>
public class MoveHydraulicRamp : MonoBehaviour
{
    private GameObject[] lifts;
    private GameObject[] lamps;
    private GameObject vehicle;
    private bool up;
    private bool down;
    private Vector3[] startPos;
    private Vector3[] endPos;
    private bool loaded;

    public Material lightMaterial;

    private void Awake()
    {
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Grip, ButtonEventType.Down, switchLight);
    }

    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.Grip, ButtonEventType.Down, switchLight);
    }

    /// <summary>
    /// Diese Methode schaltet die Lichtquellen an und aus
    /// </summary>
    private void switchLight()
    {
        for (int i = 0; i < lamps.Length; i++)
        {
            if (lamps[i].GetComponent<Light>().enabled == true) lamps[i].GetComponent<Light>().enabled = false;
            else lamps[i].GetComponent<Light>().enabled = true;
        }

        if (lamps[0].GetComponent<Light>().enabled == false)
        {
            lightMaterial.SetColor("_EmissionColor", Color.black);
        }
        else
        {
            lightMaterial.SetColor("_EmissionColor", Color.white);
        }
    }

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        up = false;
        down = true;
        lifts = GameObject.FindGameObjectsWithTag("Lift");
        vehicle = GameObject.FindGameObjectWithTag("Vehicle");
        lamps = GameObject.FindGameObjectsWithTag("Lamp");
        loaded = false;
    }

    /// <summary>
    /// Methode um die Positionen der vier bewegenden Liftplatten zu speichern.
    /// </summary>
    public void savePositions()
    {
        if (!loaded)
        {
            startPos = new Vector3[lifts.Length];
            endPos = new Vector3[lifts.Length];
            for (int i = 0; i < lifts.Length; i++)
            {
                startPos[i] = lifts[i].gameObject.transform.localPosition;
                endPos[i] = startPos[i] + new Vector3(0.0f, 2.0f, 0.0f);
            }
        }
    }

    /// <summary>
    /// Methode um das Auto hoch- und herunterzufahren.
    /// </summary>
    public void moveCar(string name)
    {
        if (name.Equals("ButtonDown"))
        {
            up = false;
            if (down == false)
            {
                for (int i = 0; i < lifts.Length; i++)
                {
                    if (lifts[i].transform.localPosition.y > startPos[i].y)
                    {
                        lifts[i].transform.localPosition -= new Vector3(0.0f, 0.01f, 0.0f);
                    }
                    else
                    {
                        lifts[i].transform.localPosition = startPos[i];
                        down = true;
                    }
                }
                vehicle.transform.localPosition -= new Vector3(0.0f, 0.01f, 0.0f);
            }

        }
        else if (name.Equals("ButtonUp"))
        {
            down = false;
            if (up == false)
            {
                for (int i = 0; i < lifts.Length; i++)
                {
                    if (lifts[i].transform.localPosition.y < endPos[i].y)
                    {
                        lifts[i].transform.localPosition += new Vector3(0.0f, 0.01f, 0.0f);
                    }
                    else
                    {
                        lifts[i].transform.localPosition = endPos[i];
                        up = true;
                    }
                }
                vehicle.transform.localPosition += new Vector3(0.0f, 0.01f, 0.0f);
            }
        }

    }
}
