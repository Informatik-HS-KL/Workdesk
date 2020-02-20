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

    [Header("command events")]
    //command events 
    [SerializeField] public UnityEvent onChangeEvent1;
    [SerializeField] public UnityEvent onChangeEvent2;
    [SerializeField] public UnityEvent onChangeEvent3;
    [SerializeField] public UnityEvent onChangeEvent4;
    [SerializeField] public UnityEvent onChangeEvent5;
    [Header("visual events")]
    [SerializeField] public UnityEvent onWaiting;
    [SerializeField] public UnityEvent onSuccess;
    [SerializeField] public UnityEvent onFailure;


    void Awake()
    {
        jsdr = (new GameObject("JSDR")).AddComponent<JsonSpeechDataReader>();
        jsdr.transform.SetParent(this.gameObject.GetComponent<Transform>());
        activationRecognizer = (new GameObject("ActivationRecognizer")).AddComponent<ActivationRecognizer>();
        activationRecognizer.transform.SetParent(this.gameObject.GetComponent<Transform>());
        //fill our Singelton KeywordHolder with data from a jsonfile located in the "StreamingAssets" folder
        KeywordHolder.GetInstance().fillWithJSON(jsdr.readData(configFileName));
        //Starting the ActivationRecognizer
        activationRecognizer.startActivationWordListener(responseTimeInSec);
        Debug.Log("Test");
    }

    private void Start()
    {
        //on event feedback
        CommandRecognizer.onEvent1Recognized += this.invokeEvent1;
        CommandRecognizer.onEvent2Recognized += this.invokeEvent2;
        CommandRecognizer.onEvent3Recognized += this.invokeEvent3;
        CommandRecognizer.onEvent4Recognized += this.invokeEvent4;
        CommandRecognizer.onEvent5Recognized += this.invokeEvent5;
        //on visual feedback
        ActivationRecognizer.onWaitingRecognized += this.invokeWaiting;
        ActivationRecognizer.onSuccessRecognized += this.invokeSuccess;
        ActivationRecognizer.onFailureRecognized += this.invokeFailure;
       
 
    }

    //command events invoking methods
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

    //visual events invoking methods
    public void invokeWaiting()
    {
        Debug.Log("onWaiting");
        onWaiting.Invoke();
    }
    public void invokeSuccess()
    {
        Debug.Log("onSuccess");
        onSuccess.Invoke();
    }
    public void invokeFailure()
    {
        Debug.Log("onFailure");
        onFailure.Invoke();
    }

}
