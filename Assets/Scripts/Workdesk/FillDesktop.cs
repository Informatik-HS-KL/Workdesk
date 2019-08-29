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

    private string chosenObjText;
    private string chosenObj;
    private string chosenModeText;
    private string chosenMode;
    private string turn3DStringDeactivated;
    private string turn3DStringActivated;

    private bool objectView;
    private bool scatterPlotVision;
    private bool architectureVision;

    private bool turn3D;

    private void Awake()
    {
        chosenObjText = "Ausgewähltes Objekt:" + "\n";
        chosenObj = "";
        chosenModeText = "Ausgewählter Modus: " + "\n";
        chosenMode = "";
        turn3DStringDeactivated = "Turn (3D off)";
        turn3DStringActivated = "Turn (3D on)";
    }

    // Wird zur Initialisierung genutzt.
    void Start()
    {
        objectView = true;
        scatterPlotVision = false;
        architectureVision = false;

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

    /// <summary>
    /// Diese Methode speichert ob 3D-Turn aktiviert oder deaktiviert ist.
    /// </summary>
    /// <param name="turn3D"></param>
    public void set3DTurn(bool turn3D)
    {
        this.turn3D = turn3D;
        writeOnDesktop();
    }

    /// <summary>
    /// Diese Methode dient zum setzen des aktuell ausgewählten Objektes.
    /// </summary>
    /// <param name="name"></param>
    public void setObject(string name)
    {
        chosenObj = name;
        writeOnDesktop();
    }

    /// <summary>
    /// Diese Methode dient zum setzen des aktuell ausgewählten Moduses.
    /// </summary>
    /// <param name="name"></param>
    public void setMode(string name)
    {
        chosenMode = name;
        writeOnDesktop();
    }

    /// <summary>
    /// Diese Methode dient dazu, den Bildschirm mit den gegebenen Daten zu füllen.
    /// </summary>
    public void writeOnDesktop()
    {
        string turn3DString = "";

        if (turn3D) turn3DString = turn3DStringActivated;
        else turn3DString = turn3DStringDeactivated;

        if (chosenMode.Equals("Turn")) screenText.text = chosenObjText + chosenObj + "\n" + chosenModeText + turn3DString;
        else screenText.text = chosenObjText + chosenObj + "\n" + chosenModeText + chosenMode;
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
