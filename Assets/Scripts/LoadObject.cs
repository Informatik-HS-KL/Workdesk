using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse wird dazu genutzt, ein Object zu laden und es in der Welt zu platzieren.
/// </summary>
public class LoadObject : MonoBehaviour
{
    private GameObject objectContainer;
    private GameObject trackerObjectContainer;

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        objectContainer = GameObject.FindGameObjectWithTag("ObjectContainer");
        trackerObjectContainer = GameObject.FindGameObjectWithTag("TrackerObjectContainer");
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {

    }
}
