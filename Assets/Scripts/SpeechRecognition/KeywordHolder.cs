using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Singelton Pattern
[Serializable]
public class KeywordHolder { 

    private static KeywordHolder _instance;
    public List<string> activationWords = new List<string>();
    public List<string> plotSceneChangeKeywords = new List<string>();
    public List<string> objectSceneChangeKeywords = new List<string>();
    public List<string> architecturSceneChangeKeywords = new List<string>();

    private KeywordHolder() { }

    public static KeywordHolder GetInstance()
    {
        if(_instance == null)
        {
            _instance = new KeywordHolder();
        }
        return _instance;
    }


    public void fillWithJSON(string jsonData)
    {
        _instance = JsonUtility.FromJson<KeywordHolder>(jsonData);
    }


    public string[] allKeywordsAsArray()
    {
        List<string> listOfAllKeywords = new List<string>(objectSceneChangeKeywords.Count + plotSceneChangeKeywords.Count + architecturSceneChangeKeywords.Count);
        listOfAllKeywords.AddRange(objectSceneChangeKeywords);
        listOfAllKeywords.AddRange(plotSceneChangeKeywords);
        listOfAllKeywords.AddRange(architecturSceneChangeKeywords);
        return listOfAllKeywords.ToArray();

    }
    


}
