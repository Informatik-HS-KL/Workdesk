using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse wird dazu genutzt, den Drehteller mitsamt des Objektes darauf zu drehen.
/// Die Drehung erfolg hierbei nur in 2D oder 3D.
/// </summary>
public class TurningTable : MonoBehaviour
{
    public Transform attachmentBase;
    public bool dreidimensional;

    private float rotationY;


    // Wird zur Initialisierung genutzt.
    void Start()
    {
        rotationY = 0;
        dreidimensional = false;
    }

    void LateUpdate()
    {
        if (attachmentBase﻿ && dreidimensional)
        {
            transform.position = attachmentBase.position;
        }
        else if (attachmentBase﻿ && !dreidimensional)
        {
            transform.position = attachmentBase.position;

            rotationY = transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(attachmentBase.rotation.x, rotationY, attachmentBase.rotation.z);
        }
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {
        
    }
}
