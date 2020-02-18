using System;
using UnityEngine;
using UnityEngine.Events;

public class SpeechRecognitionController : MonoBehaviour
{

    [Header("SpeechRecognition Settings")]
    [SerializeField] private string fileName = "speechJson.json";
    [SerializeField] private int commandTimer = 5;

    private JsonSpeechDataReader jsdr;
    private ActivationRecognizer activationRecognizer;

    [Header("Events")]
    //Events for switching a Task
    [SerializeField] public UnityEvent onChangeTaskToObjectInspector;
    [SerializeField] public UnityEvent onChangeTaskToPlotInspector;
    [SerializeField] public UnityEvent onChangeTaskToArchitectureInspector;
    [SerializeField] public UnityEvent onChangeTaskToInteractionInspector;

    void Awake()
    {
        jsdr = (new GameObject("JSDR")).AddComponent<JsonSpeechDataReader>();
       // jsdr.transform.SetParent(this.gameObject.GetComponent<Transform>());
        activationRecognizer = (new GameObject("ActivationRecognizer")).AddComponent<ActivationRecognizer>();
        //activationRecognizer.transform.SetParent(this.gameObject.GetComponent<Transform>());
        //Fill our Singelton KeywordHolder with Data from a Jsonfile located in the "StreamingAssets" folder
        KeywordHolder.GetInstance().fillWithJSON(jsdr.readData(fileName));
        //Starting the ActivationRecognizer
        activationRecognizer.startActivationWordListener(commandTimer);
    }

    private void Start()
    {
        CommandRecognizer.onObjectStringRecognized += this.invokeObjectEvent;
        CommandRecognizer.onPlotStringRecognized += this.invokePlotEvent;
        CommandRecognizer.onArchitectureStringRecognized += this.invokeArchitectureEvent;
        CommandRecognizer.onInteractionStringRecognized += this.invokeInteractionEvent;
    }

    public void invokeObjectEvent()
    {
        onChangeTaskToObjectInspector.Invoke();
    }
    public void invokePlotEvent()
    {
        onChangeTaskToPlotInspector.Invoke();
    }
    public void invokeArchitectureEvent()
    {
        onChangeTaskToArchitectureInspector.Invoke();
    }
    public void invokeInteractionEvent()
    {
        onChangeTaskToInteractionInspector.Invoke();
    }

}
