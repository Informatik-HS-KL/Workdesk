using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorColorChange : MonoBehaviour
{
    public GameObject indicator;
    public Color successColor = Color.green;
    public Color failureColor = Color.grey;
    public Color waitingColor = Color.blue;
 
    public void changeToWaitingColor()
    {
        indicator.GetComponent<Renderer>().material.color = waitingColor;
    }

    public void changeToFailureColor()
    {
        indicator.GetComponent<Renderer>().material.color = waitingColor;
    }

    public void changeToSuccessColor()
    {
        indicator.GetComponent<Renderer>().material.color = successColor;
    }
}
