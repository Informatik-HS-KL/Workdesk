using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkdeskController : MonoBehaviour
{
    public static WorkdeskController _instance;
    // Events for switching Tasks
    [Header("Tasks")]
    [SerializeField] private GameObject ObjecktHolder;
    [SerializeField] private GameObject ScatterPlotholder;
    [SerializeField] private GameObject ArchitectureHolder; 
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

    public void activateObjectTask()
    {
        Debug.Log("Objekt Task activated");
        ScatterPlotholder.SetActive(false);
        ObjecktHolder.SetActive(true);
        ArchitectureHolder.SetActive(false);
    }
    public void activateScatterPlottTask()
    {
        Debug.Log("Objekt Task activated");
        ScatterPlotholder.SetActive(true);
        ObjecktHolder.SetActive(false);
        ArchitectureHolder.SetActive(false);
    }
    public void activateArchitectureTask()
    {
        Debug.Log("Objekt Task activated");
        ScatterPlotholder.SetActive(false);
        ObjecktHolder.SetActive(false);
        ArchitectureHolder.SetActive(true);
    }
}
