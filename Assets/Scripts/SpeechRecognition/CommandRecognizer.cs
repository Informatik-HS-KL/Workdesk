using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CommandRecognizer : MonoBehaviour
{
    private KeywordRecognizer keywordrecognizer;

    //EventHandler
    public delegate void Event1Handler();
    public static event Event1Handler onEvent1Recognized;
    public delegate void Event2Handler();
    public static event Event2Handler onEvent2Recognized;
    public delegate void Event3handler();
    public static event Event3handler onEvent3Recognized;
    public delegate void Event4Handler();
    public static event Event4Handler onEvent4Recognized;
    public delegate void Event5Handler();
    public static event Event5Handler onEvent5Recognized;
    public delegate void CommandRecognizedHandler();
    public static event CommandRecognizedHandler onCommandRecognized;

    public void startCommanddListener()
    {
        keywordrecognizer = new KeywordRecognizer(KeywordHolder.GetInstance().allKeywordsAsArray());
        keywordrecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordrecognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {

        if (KeywordHolder.GetInstance().event1Keywords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            onEvent1Recognized?.Invoke();
        }
        if (KeywordHolder.GetInstance().event2Keywords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            onEvent2Recognized?.Invoke();
        }
        if (KeywordHolder.GetInstance().event3Keywords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            onEvent3Recognized?.Invoke();
        }
        if (KeywordHolder.GetInstance().event4Keywords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            onEvent4Recognized?.Invoke();
        }
        if (KeywordHolder.GetInstance().event5Keywords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            onEvent5Recognized?.Invoke();
        }
        onCommandRecognized?.Invoke();
    }

    public void stopCommandRecognizer()
    {
        keywordrecognizer.Stop();
        keywordrecognizer.Dispose();
    }

}
