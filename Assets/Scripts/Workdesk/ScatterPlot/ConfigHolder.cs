using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Singelton Pattern
[Serializable]
public class ConfigHolder
{

    private static ConfigHolder _instance;
    public string defaultFolder = "";
    public string defaultFile = "";
    public string fileToShow = "";
    public string folderToShow = "";

    private ConfigHolder() { }

    public static ConfigHolder GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ConfigHolder();
        }
        return _instance;
    }


    public void fillWithJSON(string jsonData)
    {
        _instance = JsonUtility.FromJson<ConfigHolder>(jsonData);
    }

    public string getFileToShow()
    {
        if(fileToShow == "")
        {
            return defaultFile;
        }
        else
        {
            return fileToShow;
        }
    }

    public string getFolderToShow()
    {
        if (folderToShow == "")
        {
            return defaultFolder;
        }
        else
        {
            return folderToShow;
        }
    }
}
