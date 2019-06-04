﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Die Klasse myTriggerEvent wird dazu genutzt, zu erkennen,
/// ob eine Person einen Trigger berührt hat.
/// </summary>
public class myTriggerEvent : MonoBehaviour
{
    // Wird zur Initialisierung genutzt.
    void Start()
    {

    }

    /// <summary>
    /// Diese Methode wird immer dann aufgerufen, wenn ein Objekt mit dem festgelegten Collider (Collider als isTrigger) kollidiert.
    /// </summary>
    /// <param name="collider"> Das Objekt, das mit dem Collider in Berührung kommt.</param>
    private void OnTriggerEnter(Collider collider)
    {
        //Ruft die Methode "myTriggerEnter" im Skript "ClickButton" mit dem aktuellen collider und dem Namen des berührten Objektes auf.
        this.transform.GetComponentInParent<ClickButton>().myTriggerEnter(collider, this.gameObject.name);
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {

    }
}