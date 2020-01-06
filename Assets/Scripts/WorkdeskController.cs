using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkdeskController : MonoBehaviour
{
    public static WorkdeskController _instance;
    // Events for switching Tasks
   
    private WorkdeskController() { }

    public static WorkdeskController GetInstance()
    {
        if (_instance == null)
        {
            _instance = new WorkdeskController();
        }
        return _instance;
    }

    public void Start()
    {
       
    }

    public void printInConsoleObject()
    {
        Debug.Log("Objekt Task wurde gedrückt");
    }

}
