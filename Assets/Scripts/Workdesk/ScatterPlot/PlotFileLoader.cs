using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlotFileLoader
{
   public PlotFileLoader()
    {

    }

    public PlotData getPlotData(string path)
    {
        if (!(path is null))
        {
            //get Filename
            string fileName = Path.GetFileName(path);
            // read text from file
            string fileContent = File.ReadAllText(path);

            // case simple csv
            if (Path.GetExtension(path).Equals(".csv"))
            {    
                // create TextAsset with content form file
                TextAsset asset = new TextAsset(fileContent);
                //create plotData object for holding the csvdata
                PlotData plotData = new PlotData(fileName, asset);
                //returns created plot
                return plotData;
            }
            // case own json structure
            else if (Path.GetExtension(path).Equals(".json"))
            {
                ExportProtocol protocol = JsonConvert.DeserializeObject<ExportProtocol>(fileContent);
                TextAsset csvAsset = new TextAsset(protocol.csv.ToString());
                List<int> selectedAsset = new List<int>(protocol.selected);
                PlotData plotData = new PlotData(fileName,csvAsset,selectedAsset);
                return plotData;
            }
            //default if no condition is true
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        } 
    }
}
