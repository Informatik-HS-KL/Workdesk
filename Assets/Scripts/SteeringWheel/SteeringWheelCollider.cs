using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelCollider : MonoBehaviour
{
    [SerializeField]
    Vector3 otherCollider;

    [SerializeField]
    float offsetAngle;

    [SerializeField]
    float deltaAngle = 0f;

    [SerializeField]
    float secondDeltaAngle = 0f;

    public float GetDeltaAngle
    {
        get
        {
            return deltaAngle;
        }
    }

    public float GetSecondDeltaAngle
    {
        get
        {
            return secondDeltaAngle;
        }
    }

    [SerializeField]
    float actualAngle = 0f;

    [SerializeField]
    Vector3 startVector;

    [SerializeField]
    Vector3 oldVector;

    [SerializeField]
    Vector3 secondOldVector;

    [SerializeField]
    Vector3 newVector;

    [SerializeField]
    bool grabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        oldVector = Vector3.right;
        secondOldVector = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (otherCollider != Vector3.zero && grabbed)
        {
            newVector = new Vector3(otherCollider.x - transform.position.x,
            0f, otherCollider.z - transform.position.z).normalized;

            deltaAngle = Vector3.SignedAngle(oldVector, newVector, Vector3.up);

            actualAngle += deltaAngle;

            oldVector = newVector;
        }
        if (otherCollider != Vector3.zero && grabbed)
        {
            newVector = new Vector3(otherCollider.x - transform.position.x,
            otherCollider.y - transform.position.y, 0f).normalized;

            secondDeltaAngle = Vector3.SignedAngle(secondOldVector, newVector, Vector3.forward);

            actualAngle += secondDeltaAngle;

            secondOldVector = newVector;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetType() == typeof(SphereCollider))
        {
            if (!ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger))
            {
                grabbed = false;
                deltaAngle = 0f;
                secondDeltaAngle = 0f;
            }
            else if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger) && !grabbed)
            {
                grabbed = true;

                oldVector = new Vector3(otherCollider.x - transform.position.x,
                    0f, otherCollider.z - transform.position.z).normalized;

                secondOldVector = new Vector3(otherCollider.x - transform.position.x,
                    otherCollider.x - transform.position.x, 0f).normalized;
            }

            otherCollider = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetType() == typeof(SphereCollider))
        {
            grabbed = false;
            deltaAngle = 0f;
            secondDeltaAngle = 0f;
        }
    }
}
