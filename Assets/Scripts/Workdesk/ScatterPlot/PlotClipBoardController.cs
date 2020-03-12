using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotClipBoardController : MonoBehaviour
{

    private Dropdown scatterplotDropdown;
    private Dropdown scatterplotAxisDropdown;
    private List<PlotData> dataFiles = new List<PlotData>();
    TextAsset[] data;

    // Start is called before the first frame update
    void Awake()
    {
        scatterplotDropdown = GameObject.FindGameObjectWithTag("ScatterplotDropdown").GetComponent<Dropdown>();
        scatterplotAxisDropdown = GameObject.FindGameObjectWithTag("DataDropdown").GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void clearScatterPlotDropdown()
    {
        if (scatterplotDropdown.options.Count != 0) scatterplotDropdown.ClearOptions();
    }

    private void clearScatterPlotAxisDropdown()
    {
        if (scatterplotAxisDropdown.options.Count != 0) scatterplotAxisDropdown.ClearOptions();
    }

    public void fillPlotList(PlotData plotData)
    {
        dataFiles.Add(plotData);
    }

    public void fillPlotListwithList(List<PlotData> list)
    {
        foreach(PlotData plotData in list)
        {
            dataFiles.Add(plotData);
        }
    }

    public void fillScatterPlotDropwdown()
    {
        //Clear Dropdown
        clearScatterPlotDropdown();
      
        foreach(PlotData plotData in dataFiles)
        {
            scatterplotDropdown.options.Add(new Dropdown.OptionData() { text = plotData.CsvAsset.name });
        }
    }

    public void fillScatterplotAxisDropwdown(string[] possibleAxisPlots)
    {
        //Clear Axis Dropdown
        clearScatterPlotAxisDropdown();
        for(int i = 0; i < possibleAxisPlots.Length; i++)
        {
            scatterplotAxisDropdown.options.Add(new Dropdown.OptionData() { text = possibleAxisPlots[i] });
        }
        scatterplotAxisDropdown.value = 0;
    }

    public void changeScatterPlotDropwdownValue(int value)
    {

    }

    public void changeScatterplotAxisDropwdownValue(int value)
    {

    }

    public int getScatterPlotValue()
    {
            Debug.Log("switchScatterplot():  " + scatterplotDropdown.value);
            return scatterplotDropdown.value;
    }

    public int getScatterPlotAxisValue()
    {
        Debug.Log("switchScatterplot():  " + scatterplotAxisDropdown.value);
        return scatterplotAxisDropdown.value;
    }


    public PlotData getPlotDataByDropwDownValue(int value)
    {
        PlotData data = dataFiles[value];
        return data;
    }  
}
