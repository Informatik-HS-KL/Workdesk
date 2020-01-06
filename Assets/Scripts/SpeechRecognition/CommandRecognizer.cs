using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CommandRecognizer : MonoBehaviour
{
    private bool commandRecognized;
    private List<string> allCommands;
    private KeywordRecognizer keywordrecognizer;

    //EventHandler
    public delegate void ObjectEventHandler();
    public static event ObjectEventHandler onObjectStringRecognized;

    public void startCommanddListener()
    {
        keywordrecognizer = new KeywordRecognizer(KeywordHolder.GetInstance().objectSceneChangeKeywords.ToArray());
        keywordrecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordrecognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (KeywordHolder.GetInstance().objectSceneChangeKeywords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            if (onObjectStringRecognized != null)
            {
                onObjectStringRecognized();
            }
        }
   
        commandRecognized = true;
    }

    public bool getCommandHere()
    {
        return commandRecognized;
    }
    public void stopCommandRecognizer()
    {
        keywordrecognizer.Stop();
        keywordrecognizer.Dispose();
    }

}
