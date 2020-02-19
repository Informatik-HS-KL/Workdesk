using System;
using UnityEngine;
using UnityEngine.Events;

public class SpeechRecognitionController : MonoBehaviour
{

    [Header("SpeechRecognition Settings")]
    [SerializeField] private string configFileName = "speechJson.json";
    [SerializeField] private int responseTimeInSec = 5;

    private JsonSpeechDataReader jsdr;
    private ActivationRecognizer activationRecognizer;

    [Header("Events")]
    //Events for switching a Task
    [SerializeField] public UnityEvent onChangeEvent1;
    [SerializeField] public UnityEvent onChangeEvent2;
    [SerializeField] public UnityEvent onChangeEvent3;
    [SerializeField] public UnityEvent onChangeEvent4;
    [SerializeField] public UnityEvent onChangeEvent5;

    void Awake()
    {
        jsdr = (new GameObject("JSDR")).AddComponent<JsonSpeechDataReader>();
        jsdr.transform.SetParent(this.gameObject.GetComponent<Transform>());
        activationRecognizer = (new GameObject("ActivationRecognizer")).AddComponent<ActivationRecognizer>();
        activationRecognizer.transform.SetParent(this.gameObject.GetComponent<Transform>());
        //Fill our Singelton KeywordHolder with Data from a Jsonfile located in the "StreamingAssets" folder
        KeywordHolder.GetInstance().fillWithJSON(jsdr.readData(configFileName));
        //Starting the ActivationRecognizer
        activationRecognizer.startActivationWordListener(responseTimeInSec);
        Debug.Log("Test");
    }

    private void Start()
    {
        CommandRecognizer.onEvent1Recognized += this.invokeEvent1;
        CommandRecognizer.onEvent2Recognized += this.invokeEvent2;
        CommandRecognizer.onEvent3Recognized += this.invokeEvent3;
        CommandRecognizer.onEvent4Recognized += this.invokeEvent4;
        CommandRecognizer.onEvent5Recognized += this.invokeEvent5;
    }

    public void invokeEvent1()
    {
        onChangeEvent1.Invoke();
    }
    public void invokeEvent2()
    {
        onChangeEvent2.Invoke();
    }
    public void invokeEvent3()
    {
        onChangeEvent3.Invoke();
    }
    public void invokeEvent4()
    {
        onChangeEvent4.Invoke();
    }
    public void invokeEvent5()
    {
        onChangeEvent5.Invoke();
    }

}
