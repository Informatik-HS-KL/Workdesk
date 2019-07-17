using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    [SerializeField]
    new SteeringWheelCollider collider;

    [SerializeField]
    float yAngle = 0f;

    [SerializeField]
    float xAngle = 0f;

    [SerializeField]
    float maxAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (collider.GetDeltaAngle != 0f && !(yAngle + collider.GetDeltaAngle <= -maxAngle
            || yAngle + collider.GetDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            yAngle += collider.GetDeltaAngle;
        }
        else if (collider.GetDeltaAngle != 0f && (yAngle + collider.GetDeltaAngle <= -maxAngle
           || yAngle + collider.GetDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            if (yAngle < 0) yAngle = -maxAngle;
            else yAngle = maxAngle;
        }

        if (collider.GetSecondDeltaAngle != 0f && !(xAngle + collider.GetSecondDeltaAngle <= -maxAngle
            || xAngle + collider.GetSecondDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            xAngle += collider.GetSecondDeltaAngle;
        }
        else if (collider.GetSecondDeltaAngle != 0f && (xAngle + collider.GetSecondDeltaAngle <= -maxAngle
           || xAngle + collider.GetSecondDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            if (xAngle < 0) xAngle = -maxAngle;
            else xAngle = maxAngle;
        }

        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0f);
    }
}
