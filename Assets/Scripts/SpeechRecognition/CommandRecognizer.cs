using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CommandRecognizer : MonoBehaviour
{
    private bool commandRecognized;
    private KeywordRecognizer keywordrecognizer;

    //EventHandler
    public delegate void ObjectEventHandler();
    public static event ObjectEventHandler onObjectStringRecognized;
    public delegate void PlotEventHandler();
    public static event PlotEventHandler onPlotStringRecognized;
    public delegate void ArchitectureEventHandler();
    public static event ArchitectureEventHandler onArchitectureStringRecognized;
    public delegate void InteractionEventHandler();
    public static event InteractionEventHandler onInteractionStringRecognized;
    public void startCommanddListener()
    {
        keywordrecognizer = new KeywordRecognizer(KeywordHolder.GetInstance().allKeywordsAsArray());
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

        if (KeywordHolder.GetInstance().plotSceneChangeKeywords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            if (onPlotStringRecognized != null)
            {
                onPlotStringRecognized();
            }
        }

        if (KeywordHolder.GetInstance().architecturSceneChangeKeywords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            if (onArchitectureStringRecognized != null)
            {
                onArchitectureStringRecognized();
            }
        }

        if (KeywordHolder.GetInstance().interactionSceneChangeKeywords.Contains(args.text.ToString()))
        {
            Debug.Log(args.text.ToString());
            if (onInteractionStringRecognized != null)
            {
                onInteractionStringRecognized();
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
