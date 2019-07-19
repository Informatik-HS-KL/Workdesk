using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    [SerializeField]
    new SteeringWheelCollider collider;

    [SerializeField]
    float zAngle = 0f;

    [SerializeField]
    float yAngle = 0f;

    [SerializeField]
    float xAngle = 0f;

    [SerializeField]
    float maxAngle = 0f;

    public bool activated3D;
    private bool firstTime;

    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
        activated3D = false;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<FillDesktop>().writeOnScreen(activated3D);
    }

    public void toggle3D()
    {
        if (firstTime)
        {
            firstTime = false;
        }
        else
        {
            if (activated3D) activated3D = false;
            else activated3D = true;
        }

        GameObject.FindGameObjectWithTag("GameController").GetComponent<FillDesktop>().writeOnScreen(activated3D);
    }

    // Update is called once per frame
    void Update()
    {
        if (collider.GetDeltaAngle != 0f && !(zAngle + collider.GetDeltaAngle <= -maxAngle
            || zAngle + collider.GetDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            zAngle += collider.GetDeltaAngle;
        }
        else if (collider.GetDeltaAngle != 0f && (zAngle + collider.GetDeltaAngle <= -maxAngle
           || zAngle + collider.GetDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            if (zAngle < 0) zAngle = -maxAngle;
            else zAngle = maxAngle;
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

        if (collider.GetThirdDeltaAngle != 0f && !(yAngle + collider.GetThirdDeltaAngle <= -maxAngle
            || yAngle + collider.GetThirdDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            yAngle += collider.GetThirdDeltaAngle;
        }
        else if (collider.GetThirdDeltaAngle != 0f && (yAngle + collider.GetThirdDeltaAngle <= -maxAngle
           || yAngle + collider.GetThirdDeltaAngle >= maxAngle) || maxAngle == 0)
        {
            if (yAngle < 0) yAngle = -maxAngle;
            else yAngle = maxAngle;
        }

        if (!activated3D)
        {
            xAngle = 0f; // geht einzeln -> auch in VR
            //yAngle = 0f; //geht einzeln -> auch in VR
            zAngle = 0f; // geht einzeln -> auch in VR
        }

        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0f);
    }
}
