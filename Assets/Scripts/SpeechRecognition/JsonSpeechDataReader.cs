using UnityEngine;
using System.IO;

public class JsonSpeechDataReader : MonoBehaviour
{
    public  string readData(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            return jsonString;
        }
        else
        {
            Debug.Log("doesnt exists");
            return null;
        }
    }


}
