using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorkdeskController : MonoBehaviour
{
    public void Start()
    {
    }

    public void activateObjectTask()
    {
        Debug.Log("Objekt Task activated");
        SceneManager.LoadScene("TaskObject");
    }
    public void activateScatterPlottTask()
    {
        Debug.Log("Objekt Task activated");
        SceneManager.LoadScene("TaskScatterplott");
    }
    public void activateArchitectureTask()
    {
        Debug.Log("Objekt Task activated");
        SceneManager.LoadScene("TaskArchitecture");
    }
    public void activateInteractionTask()
    {
        Debug.Log("Interaction Task activated");
    }
}
