using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using IATK;
using Newtonsoft.Json;

public class DataShareController : MonoBehaviour
{

    /* DataShareController kümmert sich darum welche Methode zum Daten laden genutzt wird
    Prio Reihenfolge: 
    1. CommandLineArguments
    2. Json */
    private CSVDataSource dataSource;
    private Visualizer visualizer;
    private PlotClipBoardController plotClipBoardController;
    string fileToShow = "";

    // Start is called before the first frame update
    void Start()
    {

        dataSource = gameObject.AddComponent<CSVDataSource>();
        visualizer = GameObject.FindGameObjectWithTag("Visualizer").GetComponent<Visualizer>();
        plotClipBoardController = (new GameObject("PlotClipBoardController")).AddComponent<PlotClipBoardController>();
 
          // Bestimmt welcher File als erstes Im Scatterplot angezeigt werden soll
        string outputFile = getCommandLineArgs("--file");
        fileToShow = outputFile;
        // command line arguments case
        if (!(outputFile is null))
        {
            //Init File
            PlotFileLoader fileLoader = new PlotFileLoader();
            PlotData data = fileLoader.getPlotData(outputFile);
            visualizer.createInitialScatterPlot(data);
            plotClipBoardController.fillPlotList(data);
        }
        //configfile case
        else
        {
            //loading configFile in StreamingAsset folder
            string configFileName = "ScatterPlotConfig.json";
            string jsonText = Utilities.readFileFromStreaminAssets(configFileName);
            //Fill ConfigHolder with ConfigData
            ConfigHolder.GetInstance().fillWithJSON(jsonText);

            fileToShow = ConfigHolder.GetInstance().getFileToShow();

            PlotFileLoader fileLoader = new PlotFileLoader();
            PlotData data = fileLoader.getPlotData(fileToShow);
            visualizer.createInitialScatterPlot(data);
            plotClipBoardController.fillPlotList(data);           
        }

        string outputDir = getCommandLineArgs("--dir");
        if (!(outputDir is null))
        {
            PlotDirectoryLoader dirLoader = new PlotDirectoryLoader(fileToShow);
            List<PlotData> dataList = dirLoader.getPlotDataListFromDir(outputDir);
            plotClipBoardController.fillPlotListwithList(dataList);
        }
        else
        {
            string folderToShow = ConfigHolder.GetInstance().getFolderToShow();

            PlotDirectoryLoader dirLoader = new PlotDirectoryLoader(fileToShow);
            List<PlotData> dataList = dirLoader.getPlotDataListFromDir(folderToShow);
            plotClipBoardController.fillPlotListwithList(dataList);

        }
        plotClipBoardController.fillScatterPlotDropwdown();
        plotClipBoardController.fillScatterplotAxisDropwdown(visualizer.GetPossibleScattersplots());
    }




    public void loadSelectedScatterPlot()
    {
        int value = plotClipBoardController.getScatterPlotValue();
        PlotData data = plotClipBoardController.getPlotDataByDropwDownValue(value);
        visualizer.createInitialScatterPlot(data);
        plotClipBoardController.fillScatterplotAxisDropwdown(visualizer.GetPossibleScattersplots());
    }

    public void loadSelectedAxisScatterPlot()
    {
        int value = plotClipBoardController.getScatterPlotAxisValue();
        visualizer.loadSpecificScatterplot(value);
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
