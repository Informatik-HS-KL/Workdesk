using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Diese Klasse wird dazu genutzt, um die Kamera auszulesen, die an der kleinen Figur angebracht ist.
/// Das Bild dieser Kamera wird auf dem Bildschirm wiedergegeben.
/// </summary>
public class FillDesktop : MonoBehaviour
{
    public RawImage rawPicture;
    public Text screenText;

    private Camera firstPersonCam;
    private RenderTexture renderTexture;
    private int resWidth;
    private int resHeight;

    private string firstPersonString;
    private string turn3DStringDeactivated;
    private string turn3DStringActivated;

    private bool objectVision;
    private bool scatterPlotVision;
    private bool architectureVision;

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        objectVision = true;
        scatterPlotVision = false;
        architectureVision = false;

        firstPersonString = "First Person Camera in Standby.";
        turn3DStringDeactivated = "3D turning deactivated.";
        turn3DStringActivated = "3D turning activated.";

        resWidth = 1920;
        resHeight = 1080;

        renderTexture = new RenderTexture(resWidth, resHeight, 24);

        if (GameObject.FindGameObjectWithTag("FirstPersonCamera") != null) openCamera();
    }

    /// <summary>
    /// Diese Methode sucht die First Person Kamera in der Szene und weißt sie dem Kameraobjekt zu. 
    /// Zusätzlich aktiviert sie die Anzeige auf dem Bildschirm.
    /// </summary>
    public void openCamera()
    {
        firstPersonCam = GameObject.FindGameObjectWithTag("FirstPersonCamera").GetComponent<Camera>();
        rawPicture.gameObject.SetActive(true);
    }

    /// <summary>
    /// Diese Methode löscht die Kamera und deaktiviert die Anzeige auf dem Bildschirm.
    /// </summary>
    public void closeCamera()
    {
        firstPersonCam = null;
        rawPicture.gameObject.SetActive(false);
    }

    /// <summary>
    /// Diese Methode nimmt einen Screenshot auf und gibt ihn auf dem Bildschirm wieder.
    /// </summary>
    private void takeScreenshot()
    {
        firstPersonCam.targetTexture = renderTexture;
        firstPersonCam.Render();

        RenderTexture.active = renderTexture;

        firstPersonCam.targetTexture = null;
        RenderTexture.active = null;

        rawPicture.texture = renderTexture;
    }

    public void writeOnScreen(bool turn3D)
    {
        if (turn3D && objectVision)
        {
            screenText.text = firstPersonString + "\n" + turn3DStringActivated;
        }
        else
        {
            screenText.text = firstPersonString + "\n" + turn3DStringDeactivated;
        }
    }

    // Update wird einmal pro Frame aufgerufen.
    void Update()
    {
        //Macht einen Screenshot der Szene und zeigt ihn auf dem Bildschirm an.
        //Nutzt die aktive Hauptkamera.
        //picture.texture = ScreenCapture.CaptureScreenshotAsTexture(1);
        if (firstPersonCam != null) takeScreenshot();
    }
}
