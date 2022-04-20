using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Cell")]
    [SerializeField] private GameObject gridTilePref;

    [Header("Grid Values")]
    [SerializeField] private Vector3 gridPosition;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private float distance = 2f;

    public List<GameObject> grid = new List<GameObject>();
    public List<Tile[]> columns = new List<Tile[]>();

    private GameObject tilesParent;

    private ElementsGenerator elementGenerator;

    [HideInInspector] public int gridSizeX = 0;
    [HideInInspector] public int gridSizeY = 0;

    // -------------------------------
    // Tools:

    public void GenerateGrid()
    {
        transform.position = gridPosition;

        gridSizeX = (int)gridSize.x;
        gridSizeY = (int)gridSize.y;

        // ==================
        // Parents Creation:

        CreateParents(ref tilesParent, "Tiles Parent");

        // ==================
        // Grid Creation:

        int tileNumber = 0;

        for (int row = 0; row < gridSizeY; row++)
        {
            for (int col = 0; col < gridSizeX; col++)
            {
                var go = Instantiate(gridTilePref, tilesParent.transform);

                go.transform.localPosition = new Vector2(col * distance, row * -distance);

                go.transform.name = gridTilePref.transform.name + "_" + (tileNumber).ToString();

                go.GetComponent<Tile>().SetValues(col, row);
                Debug.Log(col + "-" + row);

                grid.Add(go);

                tileNumber++;
            }
        }
    }

    public void DeleteGrid()
    {
        if(grid.Count > 0)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                DestroyImmediate(grid[i]);
            }
            columns.Clear();
            grid.Clear();

            DestroyImmediate(tilesParent);
        }
    }

    // ------------------------------
    // 

    private void Awake()
    {
        elementGenerator = GetComponent<ElementsGenerator>();

        gridSizeX = (int)gridSize.x;
        gridSizeY = (int)gridSize.y;

        CreateColumns();
    }

    private void Update()
    {
        // utilizar acá un for con todos los tiles para que vayan en orden

        for (int col = 0; col < gridSizeX; col++)
        {
            for (int row = 0; row < gridSizeY; row++)
            {
                int lastRow = row - 1;
                if (lastRow <= 0) { lastRow = 0; }

                columns[col][row].UpdateTile(ref columns[col][lastRow], col, row);
            }
        }
    }

    // ------------------------------
    // Private Functions:

    private void CreateParents(ref GameObject go, string name)
    {
        go = new GameObject();
        go.transform.parent = this.transform;
        go.transform.localPosition = Vector3.zero;

        go.transform.name = name;
    }

    private void CreateColumns()
    {
        for (int i = 0; i < gridSizeX; i++)
        {
            Tile[] array = new Tile[gridSizeY];

            columns.Add(array);
        }

        int id = 0;

        for (int row = 0; row < gridSizeY; row++)
        {
            for (int col = 0; col < gridSizeX; col++)
            {
                columns[col][row] = grid[id].GetComponent<Tile>();
                id++;
            }
        }
    }
}