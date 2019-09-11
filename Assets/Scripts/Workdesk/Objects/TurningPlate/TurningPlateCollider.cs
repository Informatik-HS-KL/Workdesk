using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Diese Klasse dient zur Interaktion des Controllers mit dem Collider auf der Platte.
/// </summary>
public class TurningPlateCollider : MonoBehaviour
{
    [SerializeField]
    Vector3 otherCollider;

    private float zAngle = 0f;
    private float xAngle = 0f;
    private float yAngle = 0f;

    public float getXAngle() { return xAngle; }
    public float getYAngle() { return yAngle; }
    public float getZAngle() { return zAngle; }

    private Vector3 newZVector;
    private Vector3 newXVector;
    private Vector3 newYVector;

    private Vector3 oldXVector;
    private Vector3 oldYVector;
    private Vector3 oldZVector;

    [SerializeField]
    bool grabbed = false;

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        oldXVector = Vector3.up;
        oldYVector = Vector3.right;
        oldZVector = Vector3.up;
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {
        if (otherCollider != Vector3.zero && grabbed)
        {
            newXVector = new Vector3(0f, otherCollider.y - transform.position.y, otherCollider.z - transform.position.z).normalized;
            newYVector = new Vector3(otherCollider.x - transform.position.x, 0f, otherCollider.z - transform.position.z).normalized;
            newZVector = new Vector3(otherCollider.x - transform.position.x, otherCollider.y - transform.position.y, 0f).normalized;

            xAngle = Vector3.SignedAngle(oldXVector, newXVector, Vector3.right);
            yAngle = Vector3.SignedAngle(oldYVector, newYVector, Vector3.up);
            zAngle = Vector3.SignedAngle(oldZVector, newZVector, Vector3.forward);


            oldZVector = newZVector;
            oldXVector = newXVector;
            oldYVector = newYVector;
        }
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, solange sich der Controller innerhalb des Colliders befindet.
    /// Zusätzlich wird unterschieden, ob der Trigger gedrückt oder losgelassen ist.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.GetType() == typeof(SphereCollider))
        {
            if (!ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger))
            {
                grabbed = false;
                setZero();
            }
            else if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger) && !grabbed)
            {
                grabbed = true;
                oldZVector = new Vector3(otherCollider.x - transform.position.x, otherCollider.y - transform.position.y, 0f).normalized;
                oldXVector = new Vector3(0f, otherCollider.y - transform.position.y, otherCollider.z - transform.position.z).normalized;
                oldYVector = new Vector3(otherCollider.x - transform.position.x, 0f, otherCollider.z - transform.position.z).normalized;
            }
            otherCollider = other.transform.position;
        }
    }

    /// <summary>
    /// Methode dient zum Zurücksetzen der drei Winkel.
    /// </summary>
    private void setZero()
    {
        xAngle = 0f;
        yAngle = 0f;
        zAngle = 0f;
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, sobald sich der Controller aus dem Colliderbereich der Platte entfernt.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.GetType() == typeof(SphereCollider))
        {
            grabbed = false;
            setZero();
        }
    }
}