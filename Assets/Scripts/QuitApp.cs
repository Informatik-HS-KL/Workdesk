using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Klasse zum Beenden der Applikation.
/// </summary>
public class QuitApp : MonoBehaviour
{
    void Awake()
    {
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Menu, ButtonEventType.Press, quit);
    }

    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.Menu, ButtonEventType.Press, quit);
    }

    /// <summary>
    /// Methode zum Beenden der Applikation. Falls diese im Unity Editor läuft wird dieser ebenfalls gestoppt, da
    /// hier Application.Quit nicht funktioniert.s
    /// </summary>
    private void quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif  
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) quit();
    }
}
