using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GetDesktop : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage picture;

    // Wird zur Initialisierung genutzt.
    void Start()
    {

    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {
        //Macht einen Screenshot der Szene und zeigt ihn auf dem Bildschirm an.
        //picture.texture = ScreenCapture.CaptureScreenshotAsTexture(1);
        
    }
}
