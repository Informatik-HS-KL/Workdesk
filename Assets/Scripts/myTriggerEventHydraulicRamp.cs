using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Die Klasse myTriggerEvent wird dazu genutzt, zu erkennen,
/// ob eine Person einen Trigger berührt hat.
/// </summary>
public class myTriggerEventHydraulicRamp : MonoBehaviour
{
    // Wird zur Initialisierung genutzt.
    void Start()
    {

    }

    /// <summary>
    /// Diese Methode wird aufgerufen, solange ein Objekt sich im Collider befindet.
    /// </summary>
    /// <param name="collider">Das Objekt, das mit dem Collider in Berührung kommt.</param>
    private void OnTriggerStay(Collider collider)
    {
        if(collider.attachedRigidbody.gameObject.name.Equals("Caster")) this.transform.GetComponentInParent<ClickButtonHydraulicRamp>().myTriggerStay(collider, this.gameObject.name);
    }

    /// <summary>
    /// Diese Methode wird aufherufen, wenn der Collider das Objekt nicht mehr berührt.
    /// </summary>
    /// <param name="collider">Das Objekt, das mit dem Collider in Berührung kommt.</param>
    private void OnTriggerExit(Collider collider)
    {
        if(collider.attachedRigidbody.gameObject.name.Equals("Caster")) this.transform.GetComponentInParent<ClickButtonHydraulicRamp>().myTriggerExit(collider, this.gameObject.name);
    }
}