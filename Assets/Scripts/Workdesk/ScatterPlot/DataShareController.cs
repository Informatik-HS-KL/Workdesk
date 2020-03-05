using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using IATK;

public class DataShareController : MonoBehaviour
{

    /* DataShareController kümmert sich darum welche Methode zum Daten laden genutzt wird
    Prio Reihenfolge: 
    1. CommandLineArguments
    2. Json */
    private CSVDataSource dataSource;
    private Visualizer visualizer;

    // Start is called before the first frame update
    void Start()
    {

        dataSource = gameObject.AddComponent<CSVDataSource>();
        visualizer = GameObject.FindGameObjectWithTag("Visualizer").GetComponent<Visualizer>();

        // Bestimmt welcher File als erstes Im Scatterplot angezeigt werden soll
        string outputDir = getCommandLineArgs("-file");
        if (!(outputDir is null))
        {
            //Laden des Files
            if (Path.GetExtension(outputDir).Equals(".csv"))
            {
                Debug.Log(Path.GetExtension(outputDir));
                //CSV Loader
            }
            else if (Path.GetExtension(outputDir).Equals(".json"))
            {
                Debug.Log(Path.GetExtension(outputDir));
                //JSON Loader
            }

        }
        else
        {
            //Laden des Files welcher in der JsonConfigDatei hinterlegt ist
            string configFileName = "ScatterPlotConfig.json";
            string jsonText = Utilities.readFileFromStreaminAssets(configFileName);
            //Fill ConfigHolder with ConfigData
            ConfigHolder.GetInstance().fillWithJSON(jsonText);
            string fileToShow = ConfigHolder.GetInstance().getFileToShow();
            if (fileToShow.Equals("Iris.csv"))
            {
                string irisData = Utilities.readFileFromStreaminAssets("Iris.csv");
                dataSource.load(irisData, null);
                Debug.Log("Datengeladen?  " + dataSource.IsLoaded);
                TextAsset asset = new TextAsset(irisData);
                dataSource.data = asset;
                dataSource.data.name = "Iris.csv";
                Debug.Log(dataSource.data);
                visualizer.createInitialScatterPlot(asset);
            }
            else
            {

            }

            string folderToShow = ConfigHolder.GetInstance().getFolderToShow();
            if (folderToShow.Equals("StreamingAssets"))
            {

            }
            else
            {
                Directory.GetFiles(folderToShow, "*.csv", SearchOption.TopDirectoryOnly);
                DirectoryInfo dir = new DirectoryInfo(folderToShow);
                FileInfo[] info = dir.GetFiles("*.*");
                foreach (FileInfo f in info)
                {
                    Debug.Log(f.Name);
                }
            }

        }
    }

    /* Methode liefert CommandLine Befehle zurück, die nach einem Bestimmt Command folgen */
    private string getCommandLineArgs(string name)
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
