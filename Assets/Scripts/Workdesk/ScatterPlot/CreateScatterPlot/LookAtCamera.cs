using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    /// <summary>
    /// Reference to the MiddleVR HeadNode.
    /// </summary>
    private GameObject headNode;

    private void Awake()
    {
        headNode = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - headNode.transform.position);
        }
    }
}
