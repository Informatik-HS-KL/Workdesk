using IATK;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HTC.UnityPlugin.Vive;
using System.IO;
using UnityEngine.UI;

/// <summary>
/// Load's CSV-Files and creates ScatterplotMatrices.
/// </summary>
public class Visualizer : MonoBehaviour
{
    /// <summary>
    /// All the found CSV-Files
    /// </summary>
    private TextAsset[] dataFiles;

    /// <summary>
    /// The size of the DataPoints.
    /// </summary>
    private float _pointSize = 0.02f;
    public float pointSize
    {
        get { return _pointSize; }
        set
        {
            _pointSize = value;
            if (null != scatterplotMatrix)
            {
                scatterplotMatrix.pointSize = value;
            }
        }
    }

    private CSVDataSource dataSource;

    /// <summary>
    /// A list of all possible Scatterplots for the currently loaded CSV-File.
    /// The first dimension of the array is the index of a possible Scatterplot.
    /// The second dimension contains the three indeces of the column of the CSV-File.
    /// </summary>
    private int[,] possibleScatterplots;
    private string csvDirectoryName = "Datasets";
    private ScatterplotMatrix scatterplotMatrix;
    private int selectedScatterplot = 0;
    private int chosenScatterplot = 0;

    public Dropdown scatterplotDropdown;
    public Dropdown DataDropdown;

    public void Awake()
    {
        dataSource = gameObject.AddComponent<CSVDataSource>();

        loadAllScatterplots();
        create();
    }    

    /// <summary>
    /// Lädt alle CSV-Dateien aus dem StreamingAssets Verzeichnis.
    /// </summary>
    private void loadAllScatterplots()
    {
        dataFiles = Resources.LoadAll<TextAsset>(csvDirectoryName);
        FileInfo[] fileInfos = new DirectoryInfo(Application.streamingAssetsPath).GetFiles("*.csv");

        dataFiles = new TextAsset[fileInfos.Length];
        int textAssetIndex = 0;
        foreach (FileInfo fileInfo in fileInfos)
        {
            scatterplotDropdown.options.Add(new Dropdown.OptionData() { text = fileInfo.Name });
            dataFiles[textAssetIndex] = new TextAsset(File.ReadAllText(fileInfo.FullName));
            dataFiles[textAssetIndex++].name = fileInfo.Name;
        }
    }

    /// <summary>
    /// Lädt die ausgewählte CSV-Datei.
    /// </summary>
    /// <param name="dataFile"></param>
    public void LoadDataSource(TextAsset dataFile)
    {
        if (null != dataFile)
        {
            dataSource.load(dataFile.text, null);
            possibleScatterplots = CalculatePossibleScatterplots();
            Debug.Log("Length: " + possibleScatterplots.GetLength(0));

            Debug.Log("Loaded CSV file from: " + dataFile.name);
        }
        else
        {
            Debug.LogError("Datafile is null!");
        }
    }

    /// <summary>
    /// Setzt die Position der Drehscheibe zurück.
    /// </summary>
    private void resetPos()
    {
        GameObject.FindGameObjectWithTag("ScatterplotPlate").GetComponent<TurningPlate>().resetPosition();
    }

    public void loadOtherScatterplot(int elem)
    {
        selectedScatterplot = elem;
        LoadDataSource(dataFiles[selectedScatterplot]);

        int[] scatterplotIndices = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        CreateScatterplotMatrix(scatterplotIndices);
    }

    /// <summary>
    /// Erstellt einen Scatterplot und lässt ihn auf dem Tisch anzeigen.
    /// </summary>
    private void create()
    {
        LoadDataSource(dataFiles[selectedScatterplot]);
        int[] scatterplotIndices = { 0, 1, 2, 3, 4, 5 , 6 , 7 , 8 , 9};
        CreateScatterplotMatrix(scatterplotIndices);

        string[] tempString = GetPossibleScattersplots();
        for (int i = 0; i < tempString.Length; i++)
        {
            //Debug.Log("POSSIBLE: " + tempString[i]);
        }
    }

    /// <summary>
    /// Creates a ScatterplotMatrix with the chosen Scatterplots.
    /// </summary>
    /// <param name="scatterplotIndices">
    /// An array with the indeces of the chosen Scatterplots.
    /// </param>
    public void CreateScatterplotMatrix(int[] scatterplotIndices)
    {
        if (dataSource.IsLoaded)
        {
            if (null != scatterplotMatrix)
            {
                Destroy(scatterplotMatrix.gameObject);
            }

            int[,] dimCombinations = new int[1, 3];
            //for (int i = 0; dimCombinations.GetLength(0) > i; ++i)
            //{
            chosenScatterplot = 6;
            for (int j = 0; 3 > j; ++j)
            {
                dimCombinations[0, j] = possibleScatterplots[scatterplotIndices[chosenScatterplot], j];
            }
            //}
            Debug.Log("DIMCOMBINATIONS LENGTH 0: " + dimCombinations.GetLength(0));
            Debug.Log("DIMCOMBINATIONS LENGTH 1: " + dimCombinations.GetLength(1));

            scatterplotMatrix = Instantiate(Resources.Load<GameObject>("Prefabs/Scatterplot/ScatterplotMatrix"), transform).GetComponent<ScatterplotMatrix>();
            scatterplotMatrix.Initialize(dataSource, dimCombinations, pointSize);
            Debug.Log("ScatterplotMatrix was created.");
        }
        else
        {
            Debug.LogError("CSVDataSource was not loaded!");
        }
    }

    /// <summary>
    /// Gibt den ausgewählten Scatterplot auf der Drehscheibe aus.
    /// </summary>
    public void nextScatterplot()
    {
        Debug.Log("NEXT SCATTERPLOT");

    }

    /// <summary>
    /// Returns all possible Scatterplots in a string representation.
    /// </summary>
    /// <returns>
    /// Array of: <identifier0> - <identifier1> - <identifier2>
    /// </returns>
    public string[] GetPossibleScattersplots()
    {
        string[] possibilities = new string[possibleScatterplots.GetLength(0)];
        for (int i = 0; possibleScatterplots.GetLength(0) > i; ++i)
        {
            possibilities[i] = dataSource[possibleScatterplots[i, 0]].Identifier + " - "
                + dataSource[possibleScatterplots[i, 1]].Identifier + " - "
                + dataSource[possibleScatterplots[i, 2]].Identifier;
        }
        return possibilities;
    }

    private int[,] CalculatePossibleScatterplots()
    {
        if (!dataSource.IsLoaded) return new int[0, 0];

        int[] indices = new int[dataSource.DimensionCount];
        for (int i = 0; dataSource.DimensionCount > i; ++i)
        {
            indices[i] = i;
        }

        IEnumerable<int>[] combinations = indices.Combinations(3).ToArray();
        int[,] result = new int[combinations.GetLength(0), 3];
        for (int i = 0; combinations.GetLength(0) > i; ++i)
        {
            for (int j = 0; 3 > j; ++j)
            {
                result[i, j] = combinations[i].ToArray()[j];
            }
        }
        return result;
    }
}

/// <summary>
/// Added an extension which creates all combination of size k of a given IEnumerabel.
/// </summary>
static class Extension
{
    // Copied from https://stackoverflow.com/a/1898744
    public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
    {
        return k == 0 ? new[] { new T[0] } :
            elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
    }
}
