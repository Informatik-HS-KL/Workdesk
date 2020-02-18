using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HTC.UnityPlugin.Vive;

public class ViveInputWandController : MonoBehaviour
{

    [Header("Events")]
    //Events for switching a Task
    [SerializeField] public UnityEvent onChangeTaskToObjectInspector;
    [SerializeField] public UnityEvent onChangeTaskToPlotInspector;
    [SerializeField] public UnityEvent onChangeTaskToArchitectureInspector;
    [SerializeField] public UnityEvent onChangeTaskToInteractionInspector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.DPadLeft))
        {
            Debug.Log("DPadLeft");
            onChangeTaskToObjectInspector.Invoke();
        }
        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.DPadUp))
        {
            Debug.Log("DPadUp");
            onChangeTaskToPlotInspector.Invoke();
        }
        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.DPadRight))
        {
            Debug.Log("DPadRight");
            onChangeTaskToArchitectureInspector.Invoke();
        }

        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.DPadDown))
        {
            Debug.Log("DPadDown");
            onChangeTaskToInteractionInspector.Invoke();
        }
    }

}
