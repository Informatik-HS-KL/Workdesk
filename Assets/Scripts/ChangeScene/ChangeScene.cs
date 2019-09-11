using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Diese Klasse wird dazu genutzt, um zwischen den Szenen zu wechseln.
/// </summary>
public class ChangeScene : MonoBehaviour
{
    /// <summary>
    /// Methode zum Laden der Startszene durch den SceneManager.
    /// </summary>
    public void loadWorkdeskScene()
    {
        SceneManager.LoadScene("WorkDesk");
    }

    /// <summary>
    /// Methode zum Laden einer Szene, je nach ausgewähltem Objekt.
    /// </summary>
    /// <param name="chosenObject">Die ausgewählte Szene.</param>
    public void loadScene(int chosenObject)
    {
        switch (chosenObject)
        {
            case 0:
                SceneManager.LoadScene("BikeShowRoom");
                break;
            case 1:
                SceneManager.LoadScene("CarShowRoom");
                break;
            case 2:
                SceneManager.LoadScene("DinosaurShowRoom");
                break;
            case 3:
                SceneManager.LoadScene("PalmTreeShowRoom");
                break;
            case 4:
                SceneManager.LoadScene("TeapotShowRoom");
                break;
        }
    }
}
