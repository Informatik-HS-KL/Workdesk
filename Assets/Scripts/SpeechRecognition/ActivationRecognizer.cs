using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class ActivationRecognizer: MonoBehaviour
{
    private int commandTimer;
    private KeywordRecognizer keywordrecognizer;
    private CommandRecognizer commandRecognizer = new CommandRecognizer();

    public void startActivationWordListener(int commandTimer)
    {
        this.commandTimer = commandTimer;
        keywordrecognizer = new KeywordRecognizer(KeywordHolder.GetInstance().activationWords.ToArray());
        keywordrecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordrecognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (KeywordHolder.GetInstance().activationWords.Contains(args.text.ToString())){
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
            keywordrecognizer.Start();
            commandRecognizer.stopCommandRecognizer();
            Debug.Log("no command recognized");
        }
        else
        {
            keywordrecognizer.Start();
            commandRecognizer.stopCommandRecognizer();
            Debug.Log("command recognized");
        }
    }

}