using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHydraulicRamp : MonoBehaviour
{

    private GameObject[] lifts;
    private bool up;
    private bool down;
    private Vector3[] startPos;
    private Vector3[] endPos;
    private bool loaded;

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        up = false;
        down = true;
        lifts = GameObject.FindGameObjectsWithTag("Lift");
        loaded = false;
    }

    /// <summary>
    /// Methode um die Positionen der vier bewegenden Liftplatten zu speichern.
    /// </summary>
    public void savePositions()
    {
        if (!loaded)
        {
            startPos = new Vector3[4];
            endPos = new Vector3[4];
            for (int i = 0; i < 4; i++)
            {
                Debug.Log(i + " " + lifts[i].gameObject.transform.localPosition);
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
                for (int i = 0; i < 4; i++)
                {
                    if (lifts[i].transform.localPosition.y > startPos[i].y)
                    {
                        lifts[i].transform.localPosition -= new Vector3(0.0f, 0.01f, 0.0f);
                    }
                    else
                    {
                        Debug.Log("Kleiner Gleich");
                        lifts[i].transform.localPosition = startPos[i];
                        down = true;
                    }
                }
            }

        }
        else if (name.Equals("ButtonUp"))
        {
            down = false;
            if (up == false)
            {
                for (int i = 0; i < 4; i++)
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
            }
        }

    }
}
