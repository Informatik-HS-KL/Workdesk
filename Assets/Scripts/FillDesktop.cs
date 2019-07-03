using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FillDesktop : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Image picture;
    public RawImage rawPicture;

    private Camera firstPersonCam;
    private Rect rect;
    private RenderTexture renderTexture;
    private int count;
    private int resWidth;
    private int resHeight;

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        count = 0;
        resWidth = 1920;
        resHeight = 1080;

        rect = new Rect(0, 0, resWidth, resHeight);
        renderTexture = new RenderTexture(resWidth, resHeight, 24);
    
        firstPersonCam = GameObject.FindGameObjectWithTag("FirstPersonCamera").GetComponent<Camera>();
    }

    private void takeScreenshot()
    {      
        firstPersonCam.targetTexture = renderTexture;
        firstPersonCam.Render();

        RenderTexture.active = renderTexture;

        firstPersonCam.targetTexture = null;
        RenderTexture.active = null;

        rawPicture.texture = renderTexture;
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {
        //Macht einen Screenshot der Szene und zeigt ihn auf dem Bildschirm an.
        //Nutzt die aktive Hauptkamera.
        //picture.texture = ScreenCapture.CaptureScreenshotAsTexture(1);
        //if(count++ == 20)
        //{
            count = 0;
            takeScreenshot();
        //}
        
    }
}
