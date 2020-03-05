using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
   public static object[] convertListToArray(List<object> list)
    {
        object[] array;
        array = list.ToArray();
        return array;
    }

    public static string getSceneAsString()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static string readFileFromStreaminAssets(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            return jsonString;
        }
        else
        {
            Debug.Log("readFileFromStreaminAssets(..): File does not exist");
            return null;
        }
    }

}
