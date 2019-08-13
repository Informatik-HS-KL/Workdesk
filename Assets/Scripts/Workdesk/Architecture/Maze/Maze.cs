using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

/// <summary>
/// 
/// </summary>
public class Maze : MonoBehaviour
{
    private Text xSizeLabel;
    private Text zSizeLabel;

    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public GameObject north; //1 
        public GameObject south; //2
        public GameObject east; //3
        public GameObject west; //4
    }

    private bool create = true;
    private bool isBuild;
    //public bool solve = true;    
    //public bool show = false;

    //public KeyCode solveMazeKey = KeyCode.Space;

    //benötigte GameObjects
    public GameObject wall;
    //public GameObject solver;
    public GameObject start_cube;
    public GameObject end_cube;

    //Variablen für das Labyrith
    public float wallLength = 1.0f;
    public int xSize = 2;
    public int zSize = 2;

    //public GameObject[] cubes;

    //wird benutzt um den Solver zu spwanen
    //private GameObject tempSolver;

    private int count = 0;
    private List<int> solution;
    private int solutionIterator = 0;

    //hält alle Zellen des labyriths
    public Cell[] cells;
    //aktuelle Zellennummer
    public int currentCell = 0;

    private Vector3 initPos;
    private Vector3 startPos;
    private Vector3 startPosCameraMan;
    private Vector3 planePos;
    private Vector3 planeScale;
    private GameObject mazeHolder;
    private GameObject miniMazeHolder;
    private GameObject cameraMan;

    //xSize * zSize
    private int mazeSize;
    private int maxCount;
    private int visitedCells = 0;

    private int currentNeighbour = 0;
    private List<int> lastCell;
    private int backingUp = 0;
    private int wallToBreak = 0;

    private void Awake()
    {        
        mazeHolder = GameObject.FindGameObjectWithTag("Maze");
        miniMazeHolder = GameObject.FindGameObjectWithTag("MiniMaze");
        xSizeLabel = GameObject.FindGameObjectWithTag("XSize").GetComponent<Text>();
        zSizeLabel = GameObject.FindGameObjectWithTag("ZSize").GetComponent<Text>();
        cameraMan = GameObject.FindGameObjectWithTag("MiniPlayer");
        startPosCameraMan = cameraMan.transform.position;
    }

    // Use this for initialization
    void Start()
    {
        setMazeSize();

        //solve = true;         

        //buildMaze();

        wallLength = wall.transform.localScale.z;
    }

    public void setXSize()
    {
        xSize = int.Parse(xSizeLabel.text);
        setMazeSize();
    }

    public void setZSize()
    {
        zSize = int.Parse(zSizeLabel.text);
        setMazeSize();
    }

    public void setMazeSize()
    {
        mazeSize = xSize * zSize;
        maxCount = (mazeSize * 2) + xSize + zSize + 3;
    }

    //GameObject.FindGameObjectWithTag("GameController").GetComponent<CreateMiniworld>().deactivateClipboard();

    public void buildMaze()
    {
        //erzeugt x * z großes Grid aus Wänden
        createWalls();
        //Teilt die Wände in Zellen auf
        createCells();
        //löscht Wände im Grid bis jede Zelle erreichbar ist 
        createMaze();

        isBuild = true;
    }

    public void buildMiniMaze()
    {
        GameObject tempMiniMaze = Instantiate(mazeHolder, miniMazeHolder.transform.position, Quaternion.identity);
        Vector3 tempScale = new Vector3(0.1f, 0.05f, 0.1f);
        tempMiniMaze.transform.localScale = tempScale;
        tempMiniMaze.name = "MiniMaze";
        tempMiniMaze.tag = "Untagged";

        Teleportable tempTeleportScript = tempMiniMaze.transform.Find("MazeFloor").gameObject.GetComponent<Teleportable>();
        Destroy(tempTeleportScript);

        tempMiniMaze.transform.parent = miniMazeHolder.transform;

        tempMiniMaze = null;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<CreateMiniworld>().deactivateClipboard();
    }

    public void reset()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<CreateMiniworld>().activateClipboard();
        if (isBuild)
        {
            if (GameObject.FindGameObjectWithTag("MiniMaze").transform.childCount == 3)
            {
                Destroy(GameObject.FindGameObjectWithTag("MiniMaze").transform.GetChild(1).gameObject);
                Destroy(GameObject.FindGameObjectWithTag("MiniMaze").transform.GetChild(2).gameObject);
            }
            else Destroy(GameObject.FindGameObjectWithTag("MiniMaze").transform.GetChild(1).gameObject);
            foreach (Transform child in mazeHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
        solutionIterator = 0;
        backingUp = 0;
        visitedCells = 0;
        mazeHolder = null;
        mazeHolder = GameObject.FindGameObjectWithTag("Maze");
        cameraMan.transform.position = startPosCameraMan;
        

        //solve = true;

        isBuild = false;
    }

    void createWalls()
    {
        initPos = new Vector3(mazeHolder.transform.position.x, mazeHolder.transform.position.y, mazeHolder.transform.position.z);

        Vector3 myPos = initPos;
        GameObject tempWall;

        GameObject tempPlane;
        planePos = new Vector3(initPos.x + (xSize / 2.0f) - 0.5f, 0.0f, initPos.z + (zSize / 2.0f) - 1.0f);
        planeScale = new Vector3(1.0f * (xSize / 10.0f), 1.0f, 1.0f * (zSize / 10.0f));
        tempPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        tempPlane.name = "MazeFloor";

        tempPlane.transform.position = planePos;
        tempPlane.transform.localScale = planeScale;
        tempPlane.AddComponent<Teleportable>();

        // x 
        for (int i = 0; i < zSize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initPos.x + (j * wallLength) - wallLength / 2, wall.transform.localScale.y / 2, initPos.z + (i * wallLength) - wallLength / 2);
                tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = mazeHolder.transform;
            }
        }

        // z
        for (int i = 0; i <= zSize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                myPos = new Vector3(initPos.x + (j * wallLength), wall.transform.localScale.y / 2, initPos.z + (i * wallLength) - wallLength);
                tempWall = Instantiate(wall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                tempWall.transform.parent = mazeHolder.transform;
            }
        }

        tempPlane.transform.parent = mazeHolder.transform;
    }//createWalls

    void createCells()
    {
        lastCell = new List<int>();
        lastCell.Clear();
        GameObject[] allWalls;
        int wallCount = mazeHolder.transform.childCount;
        allWalls = new GameObject[wallCount];
        cells = new Cell[xSize * zSize];
        int westEast = 0;
        int child = 0;
        int count = 0;

        for (int i = 0; i < wallCount; i++)
        {
            allWalls[i] = mazeHolder.transform.GetChild(i).gameObject;
        }

        //Wände den Zellen zuordnen
        for (int i = 0; i < cells.Length; i++)
        {
            if (count == xSize)
            {
                westEast++;
                count = 0;
            }
            cells[i] = new Cell();
            cells[i].west = allWalls[westEast];
            cells[i].south = allWalls[child + (xSize + 1) * (zSize)];

            westEast++;
            count++;
            child++;
            cells[i].east = allWalls[westEast];
            cells[i].north = allWalls[(child + (xSize + 1) * zSize) + xSize - 1];
        }
    }//createCells



    void createMaze()
    {
        bool startedBuilding = false;

        while (visitedCells < mazeSize)
        {
            if (startedBuilding)
            {
                getNeighbours();
                if (cells[currentNeighbour].visited == false && cells[currentCell].visited == true)
                {
                    removeWall();
                    cells[currentNeighbour].visited = true;
                    visitedCells++;
                    lastCell.Add(currentCell);
                    currentCell = currentNeighbour;
                    if (lastCell.Count > 0)
                    {
                        backingUp = lastCell.Count - 1;
                    }
                }
            }
            else
            {
                currentCell = Random.Range(0, mazeSize);
                cells[currentCell].visited = true;
                visitedCells++;
                startedBuilding = true;
            }
        }

        markStart();
        markEnd();
        miniMazeActive = true;
    }//createMaze

    // erzeugt Array mit allen Nachbarn der aktuellen Zelle
    void getNeighbours()
    {
        //Anzahl der Nachbarn (wird für die Randomfunktion benötigt)
        int length = 0;
        int[] neighbours = new int[4];
        int[] connectingWalls = new int[4];
        int check = 0;


        check = ((currentCell + 1) / xSize);
        check -= 1;
        check *= xSize;
        check += xSize;

        //north
        if (currentCell + xSize < mazeSize)
        {
            if (cells[currentCell + xSize].visited == false)
            {
                neighbours[length] = currentCell + xSize;
                connectingWalls[length] = 1;
                length++;
            }
        }

        //west
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighbours[length] = currentCell - 1;
                connectingWalls[length] = 2;
                length++;
            }
        }


        //east
        if (currentCell + 1 < mazeSize && (currentCell + 1) != check)
        {
            if (cells[currentCell + 1].visited == false)
            {
                neighbours[length] = currentCell + 1;
                connectingWalls[length] = 3;
                length++;
            }
        }


        //south
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false)
            {
                neighbours[length] = currentCell - xSize;
                connectingWalls[length] = 4;
                length++;
            }
        }


        if (length != 0)
        {
            int randomNeighbour = Random.Range(0, length);
            currentNeighbour = neighbours[randomNeighbour];
            wallToBreak = connectingWalls[randomNeighbour];
        }
        else
        {
            if (backingUp > 0)
            {
                currentCell = lastCell[backingUp];
                backingUp--;
            }
        }
    }//getNeighbours

    void removeWall()
    {
        switch (wallToBreak)
        {
            case 1: Destroy(cells[currentCell].north); break;
            case 2: Destroy(cells[currentCell].west); break;
            case 3: Destroy(cells[currentCell].east); break;
            case 4: Destroy(cells[currentCell].south); break;
        }
    }

    void markStart()
    {
        GameObject start;

        Vector3 myPos = new Vector3(cells[0].west.transform.position.x + wallLength / 2, 1, cells[0].west.transform.position.z);
        start = Instantiate(start_cube, myPos, Quaternion.identity) as GameObject;
        startPos = start.transform.position;

        //if (GameObject.Find("FPSController") != null)
        //{
        //    GameObject.Find("FPSController").transform.position = (startPos);
        //}
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = (startPos);
        }

        start.transform.parent = mazeHolder.transform;

        //start.GetComponent<Button>().onClick.AddListener(reset);
        //Button btn = start.GetComponentInChildren<Button>();
        //btn.onClick.AddListener(reset);
    }

    void markEnd()
    {
        GameObject end;

        Vector3 myPos = new Vector3(cells[mazeSize - 1].east.transform.position.x - wallLength / 2, 1, cells[mazeSize - 1].east.transform.position.z);
        end = Instantiate(end_cube, myPos, Quaternion.identity) as GameObject;

        end.transform.parent = mazeHolder.transform;
    }



    /*void solveMaze()
    {
        Debug.Log("solveStart");

        lastCell = new List<int>();
        lastCell.Clear();
        solution = new List<int>();
        solution.Clear();


        visitedCells = 0;
        lastCell.Clear();
        currentCell = 0;

        //Zurücksetzen der variable visited von allen Zellen
        foreach (var cell in cells)
        {
            cell.visited = false;
        }

        cells[currentCell].visited = true;
        lastCell.Add(currentCell);
        visitedCells++;


        while (visitedCells < (xSize * zSize) && currentCell != ((xSize * zSize) - 1))
        {


            if (cells[currentCell].north == null && cells[currentCell + xSize].visited != true)
            {

                currentCell = currentCell + xSize;
                cells[currentCell].visited = true;
                lastCell.Add(currentCell);
                visitedCells++;

            }
            else if (cells[currentCell].west == null && cells[currentCell - 1].visited != true)
            {

                currentCell = currentCell - 1;
                cells[currentCell].visited = true;
                lastCell.Add(currentCell);
                visitedCells++;

            }
            else if (cells[currentCell].east == null && cells[currentCell + 1].visited != true)
            {

                currentCell = currentCell + 1;
                cells[currentCell].visited = true;
                lastCell.Add(currentCell);
                visitedCells++;

            }
            else if (cells[currentCell].south == null && cells[currentCell - xSize].visited != true)
            {

                currentCell = currentCell - xSize;
                cells[currentCell].visited = true;
                lastCell.Add(currentCell);
                visitedCells++;

            }
            else
            {
                Debug.Log("backup");
                //backup
                if (lastCell.Count > 0)
                {
                    Debug.Log(lastCell.ToArray().ToString());

                    Debug.Log("COUNT: " + lastCell.Count);
                    if (lastCell.Count > 1)
                    {
                        lastCell.RemoveAt(lastCell.Count - 1);
                        currentCell = lastCell[lastCell.Count - 1];
                    }

                }
            }
            cells[currentCell].visited = true;
            solution.Add(currentCell);

            Debug.Log("solveEnd");
        }//while

    }//solveMaze()*/

    //erzeuge nacheinander in jeder Zelle aus dem Array solution das solver Gameobject. Die Position wird von den Zellewänden abgeleitet.
    /*void showSolution()
    {
        
        Debug.Log("start show");
        Debug.Log(solutionIterator);
     
        if (solutionIterator < solution.Count - 1)
        {
            Destroy(tempSolver);

            currentCell = solution[solutionIterator++];


            //solution.RemoveAt(solution.Count - 1);

           
            if (cells[currentCell].east != null)
            {
                //Debug.Log("east wall pos x: " + cells[currentCell].east.transform.position.x + " z: " + cells[currentCell].east.transform.position.z);

                Vector3 myPos = new Vector3(cells[currentCell].east.transform.position.x - 2, 1, cells[currentCell].east.transform.position.z);
                tempSolver = Instantiate(solver, myPos, Quaternion.identity) as GameObject;
            
                //Farbe von Gameobject ändern
                //tempSolver.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 1);

                return;
            }
            else if (cells[currentCell].west != null)
            {
                Debug.Log("west wall pos x: " + cells[currentCell].west.transform.position.x + " z: " + cells[currentCell].west.transform.position.z);

                Vector3 myPos = new Vector3(cells[currentCell].west.transform.position.x + 2, 1, cells[currentCell].west.transform.position.z);
                tempSolver = Instantiate(solver, myPos, Quaternion.identity) as GameObject;

                return;

            }
            else if (cells[currentCell].north != null)
            {
                Debug.Log("north wall pos x: " + cells[currentCell].north.transform.position.x + " z: " + cells[currentCell].north.transform.position.z);

                Vector3 myPos = new Vector3(cells[currentCell].north.transform.position.x, 1, cells[currentCell].north.transform.position.z - 2);
                tempSolver = Instantiate(solver, myPos, Quaternion.identity) as GameObject;

                return;

            }
            else if (cells[currentCell].south != null)
            {
                Debug.Log("south wall pos x: " + cells[currentCell].south.transform.position.x + " z: " + cells[currentCell].south.transform.position.z);

                Vector3 myPos = new Vector3(cells[currentCell].south.transform.position.x, 1, cells[currentCell].south.transform.position.z + 2);
                tempSolver = Instantiate(solver, myPos, Quaternion.identity) as GameObject;

                return;


            }

            

        }//if (solotionIterator...)
        else
        {
            Destroy(tempSolver);
            currentCell = 0;
            solutionIterator = 0;
            show = false;
        }
    }//showSolution()*/

    private bool miniMazeActive = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reset();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildMaze();
        }

        if (miniMazeActive && mazeHolder.transform.childCount < maxCount)
        {
            miniMazeActive = false;
            buildMiniMaze();
        }
        /*if (updateCount >= 35 && show)
        {
            
            showSolution();
            updateCount = 0;
        }


              
        updateCount++;*/
    }

    /*void LateUpdate()
    {
        // start/stop solver
        if (Input.GetKeyUp(solveMazeKey))
        {   
            show = !show;
        }

        if (solve)
        {
            solveMaze();
            //solution.Reverse();
        }
        solve = false;
    }*/
}