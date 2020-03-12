using IATK;
using UnityEngine;

/// <summary>
/// Capsules and creates DataPoints.
/// </summary>
public class Scatterplot : MonoBehaviour
{
    public CSVDataSource dataSource;

    /// <summary>
    /// The size of the DataPoints.
    /// </summary>
    private float _pointSize;
    public float pointSize
    {
        get { return _pointSize; }
        set
        {
            _pointSize = value;
            foreach (DataPoint dataPoint in dataPoints)
            {
                dataPoint.pointSize = value;
            }
        }
    }

    /// <summary>
    /// The three indeces of the columns in the CSV-File.
    /// </summary>
    public int xDim, yDim, zDim;

    private DataPoint[] dataPoints = new DataPoint[0];
    private bool bigScatterplot;

    /// <summary>
    /// Initializes the ScatterplotMatrix.
    /// Should always be called after creating this component.
    /// </summary>
    /// <param name="dataSource"></param>
    /// <param name="matrixPosX"></param>
    /// <param name="matrixPosZ"></param>
    /// <param name="pointSize"></param>
    /// <param name="xDim"></param>
    /// <param name="yDim"></param>
    /// <param name="zDim"></param>
    public void Initialize(CSVDataSource dataSource, float matrixPosX, float matrixPosZ, float pointSize, int xDim, int yDim, int zDim, bool bigScatterplot)
    {
        this.dataSource = dataSource;
        this.pointSize = pointSize;
        this.xDim = xDim;
        this.yDim = yDim;
        this.zDim = zDim;
        this.bigScatterplot = bigScatterplot;

        float posOffset = 1;

        InitializeAxesLabel();
        CreateDataPoints();
        transform.Translate(new Vector3(matrixPosX + posOffset * matrixPosX, 0, matrixPosZ + posOffset * matrixPosZ));
    }

    /// <summary>
    /// Sets the text of the Axes to the identifiers of the columns of the CSV-File.
    /// </summary>
    private void InitializeAxesLabel()
    {
        Transform axes = transform.Find("Axes");
        axes.Find("X Axis").GetComponentInChildren<TextMesh>().text = dataSource[xDim].Identifier;
        axes.Find("Y Axis").GetComponentInChildren<TextMesh>().text = dataSource[yDim].Identifier;
        axes.Find("Z Axis").GetComponentInChildren<TextMesh>().text = dataSource[zDim].Identifier;
    }

    private void CreateDataPoints()
    {
        GameObject pointPrefab = Resources.Load<GameObject>("Prefabs/Scatterplot/DataPoint");
        Transform scatterplotTransform = null;

        if (bigScatterplot)
        {
            GameObject scatterplotHolder = GameObject.FindGameObjectWithTag("bigScatterplot");
            scatterplotTransform = scatterplotHolder.GetComponent<Transform>();
        }
        else
        {
            GameObject scatterplotPlate = GameObject.FindGameObjectWithTag("Visualizer");
            scatterplotTransform = scatterplotPlate.GetComponent<Transform>();
        }

        dataPoints = new DataPoint[dataSource.DataCount];

        for (int i = 0; dataSource.DataCount > i; ++i)
        {
            Vector3 position = new Vector3(dataSource[xDim].Data[i] + scatterplotTransform.position.x,
                dataSource[yDim].Data[i] + scatterplotTransform.position.y + 0.02f, dataSource[zDim].Data[i] + scatterplotTransform.position.z);
            DataPoint dataPoint = Instantiate(pointPrefab, transform).GetComponent<DataPoint>();
            dataPoint.Initialize(i, pointSize, position);

            dataPoints[i] = dataPoint;
        }
    }

    /// <summary>
    /// Iterates through all DataPoints and highligths the selected one.
    /// Also shows his attributes in a dialog.
    /// This Method is called from ScatterplotMatrix.
    /// </summary>
    /// <param name="index"></param>
    public void SelectDataPoint(int index)
    {
        foreach (DataPoint dataPoint in dataPoints)
        {
            if (dataPoint.index == index)
            {
                //dataPoint.GetComponent<Renderer>().material.color = Color.black;
                dataPoint.ShowText(true);
                dataPoint.pointSize = pointSize + 0.01f;
            }
            else
            {
                //dataPoint.GetComponent<Renderer>().material.color = dataPoint.getColor();
                dataPoint.ShowText(false);
                dataPoint.pointSize = pointSize;
            }
        }
    }
}
