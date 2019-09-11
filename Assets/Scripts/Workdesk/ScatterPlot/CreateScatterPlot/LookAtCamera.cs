using UnityEngine;

/// <summary>
/// Klasse dient zum Ausrichten der Textanzeige eines Datenpunktes, 
/// sodass der Anwender diese immer sehen kann.
/// </summary>
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