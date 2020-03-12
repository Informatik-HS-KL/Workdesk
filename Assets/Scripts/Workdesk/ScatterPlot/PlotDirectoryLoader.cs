using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlotDirectoryLoader : MonoBehaviour
{

    private string initFile;

    public PlotDirectoryLoader(string initFile)
    {
        this.initFile = initFile.Replace("/", @"\");
    }

    public List<PlotData> getPlotDataListFromDir(string path)
    {
        if (!(path is null))
        {
            List<PlotData> plotDataList = new List<PlotData>();

            Directory.GetFiles(path, "*.csv", SearchOption.TopDirectoryOnly);
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] infocsv = dir.GetFiles("*.csv");
            FileInfo[] infojson = dir.GetFiles("*.json");
            FileInfo[] info = new FileInfo[infocsv.Length + infojson.Length];
            Array.Copy(infocsv, info, infocsv.Length);
            Array.Copy(infojson, 0, info, infocsv.Length, infojson.Length);

            foreach (FileInfo f in info)
            {
                //create asset  
                if (!(initFile.Equals(f.FullName)))
                {
                    PlotFileLoader plotFileLoader = new PlotFileLoader();
                    PlotData data = plotFileLoader.getPlotData(f.FullName);
                    plotDataList.Add(data);
                }
                else
                {
                   
                }
            }

            return plotDataList;
        }
        else
        {
            return null;
        }
    }

}
