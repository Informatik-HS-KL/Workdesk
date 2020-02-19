using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Singelton Pattern
[Serializable]
public class KeywordHolder
{

    private static KeywordHolder _instance;
    public List<string> activationWords = new List<string>();
    public List<string> event1Keywords = new List<string>();
    public List<string> event2Keywords = new List<string>();
    public List<string> event3Keywords = new List<string>();
    public List<string> event4Keywords = new List<string>();
    public List<string> event5Keywords = new List<string>();

    private KeywordHolder() { }

    public static KeywordHolder GetInstance()
    {
        if (_instance == null)
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
        List<string> listOfAllKeywords = new List<string>(event1Keywords.Count + event2Keywords.Count + event3Keywords.Count + event4Keywords.Count + event5Keywords.Count);
        listOfAllKeywords.AddRange(event1Keywords);
        listOfAllKeywords.AddRange(event2Keywords);
        listOfAllKeywords.AddRange(event3Keywords);
        listOfAllKeywords.AddRange(event4Keywords);
        listOfAllKeywords.AddRange(event5Keywords);
        return listOfAllKeywords.ToArray();

    }



}
