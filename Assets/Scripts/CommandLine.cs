using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLine : MonoBehaviour
{
    public GameObject tisch;

    // Start is called before the first frame update
    void Start()
    {
        System.Console.WriteLine("DataShare Scene");
        Debug.developerConsoleVisible = true;
        Debug.Log("Debug console");

        string outputDir = GetArg("-tablecolor");
        if (outputDir.Equals("green"))
        {
            tisch.transform.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }
}
