using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class ActivationRecognizer : MonoBehaviour
{
    private int commandTimer;
    private KeywordRecognizer keywordrecognizer;
    private CommandRecognizer commandRecognizer;

    private GameObject indicator;

    public delegate void waitingHandler();
    public static event waitingHandler onWaitingRecognized;
    public delegate void successHandler();
    public static event successHandler onSuccessRecognized;
    public delegate void failureHandler();
    public static event failureHandler onFailureRecognized;

    private void Awake()
    {
        commandRecognizer = (new GameObject("CommandRecognizer")).AddComponent<CommandRecognizer>();
        commandRecognizer.transform.SetParent(this.gameObject.GetComponent<Transform>());
    }

    public void startActivationWordListener(int commandTimer)
    {
        this.commandTimer = commandTimer;
        keywordrecognizer = new KeywordRecognizer(KeywordHolder.GetInstance().activationWords.ToArray());
        keywordrecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordrecognizer.Start();
        Debug.Log("Start");
        Debug.Log(KeywordHolder.GetInstance().activationWords.Count);
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        onWaitingRecognized?.Invoke();
        if (KeywordHolder.GetInstance().activationWords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            keywordrecognizer.Stop();
            StartCoroutine(waitForCommand());
            commandRecognizer.startCommanddListener();
        }
    }

    private IEnumerator waitForCommand()
    {
        yield return new WaitForSeconds(commandTimer);
        if (commandRecognizer.getCommandHere() == false)
        {
            onFailureRecognized?.Invoke();
            keywordrecognizer.Start();
            commandRecognizer.stopCommandRecognizer();
            Debug.Log("no command recognized");
        }
        else
        {
            onSuccessRecognized?.Invoke();
            keywordrecognizer.Start();
            commandRecognizer.stopCommandRecognizer();
            Debug.Log("command recognized");
        }
    }
}