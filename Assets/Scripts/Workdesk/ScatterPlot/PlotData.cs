using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Datatype containing the main csv and selectiondata
public class PlotData
{
    //contains csv as TextAsset (string)
    private TextAsset csvAsset;
    //contains csv of selected as TextAsset
    private List<int> selectedAsset;
    //name of file for dropdownmenu
    private string name;

    //Getter Setter
    public string Name { get => name; set => name = value; }
    public TextAsset CsvAsset { get => csvAsset; set => csvAsset = value; }
    public List<int> SelectedAsset { get => selectedAsset; set => selectedAsset = value; }

    //contructors
    public PlotData(TextAsset csvAsset)
    {
        this.csvAsset = csvAsset;
    }

    public PlotData(string name, TextAsset csvAsset)
    {
        this.name = name;
        this.csvAsset = csvAsset;
        this.csvAsset.name = name;
    }

    public PlotData(string name, TextAsset csvAsset, List<int> selectedAsset)
    {
        this.name = name;
        this.csvAsset = csvAsset;
        this.csvAsset.name = name;
        this.selectedAsset = selectedAsset;
    }

}
