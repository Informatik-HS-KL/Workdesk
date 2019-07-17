using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    [SerializeField]
    new SteeringWheelCollider collider;

    [SerializeField]
    float actualAngle = 0f;

    [SerializeField]
    float maxAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (collider.GetDeltaAngle != 0f && !(actualAngle + collider.GetDeltaAngle <= -maxAngle
            || actualAngle + collider.GetDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            actualAngle += collider.GetDeltaAngle;
        }
        else if (collider.GetDeltaAngle != 0f && (actualAngle + collider.GetDeltaAngle <= -maxAngle
           || actualAngle + collider.GetDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            if (actualAngle < 0) actualAngle = -maxAngle;
            else actualAngle = maxAngle;
        }
        transform.rotation = Quaternion.Euler(0f, actualAngle, 0f);
    }
}
