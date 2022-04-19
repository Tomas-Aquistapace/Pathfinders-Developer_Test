using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Grid Cell")]
    [SerializeField] private GameObject gridTilePref;

    [Header("Grid Values")]
    [SerializeField] private Vector3 gridPosition;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private float distance = 2f;
    [SerializeField] private float spawnYpos = 4f;

    public List<GameObject> grid = new List<GameObject>();
    public List<Tile[]> columns = new List<Tile[]>();
    public GameObject[] spawnTrans;

    private GameObject spawnParent;
    private GameObject tilesParent;

    // -------------------------------
    // Tools:

    public void GenerateGrid()
    {
        transform.position = gridPosition;

        // ==================
        // Parents Creation:

        CreateParents(ref spawnParent, "Spawn Parent");
        CreateParents(ref tilesParent, "Tiles Parent");

        // ==================
        // Spawn Creation:

        spawnTrans = new GameObject[(int)gridSize.x];

        // ==================
        // Grid Creation:

        for (int i = 0; i < (int)gridSize.x; i++)
        {
            Tile[] array = new Tile[(int)gridSize.y];

            columns.Add(array);
        }

        for (int row = 0; row < (int)gridSize.y; row++)
        {
            for (int col = 0; col < (int)gridSize.x; col++)
            {
                var go = Instantiate(gridTilePref, tilesParent.transform);

                go.transform.localPosition = new Vector2(col * distance, row * -distance);

                grid.Add(go);

                go.transform.name = gridTilePref.transform.name + "_" + (row+col).ToString();

                go.GetComponent<Tile>().SetValues(col, row);
                columns[col][row] = go.GetComponent<Tile>();

                // ==================
                // Spawn Creation:

                if (row <= 0)
                {
                    spawnTrans[col] = new GameObject();
                    spawnTrans[col].transform.parent = spawnParent.transform;
                    spawnTrans[col].transform.name = "spawnPoint_" + col;
                    spawnTrans[col].transform.localPosition = new Vector2(col * distance, spawnYpos);
                }
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

            DestroyImmediate(spawnParent);
            DestroyImmediate(tilesParent);
        }
    }

    // ------------------------------
    // 

    public void CallAnElement(int column, int id)
    {
        if(id > 0)
        {
            columns[column][id].elementInTile = columns[column][id - 1].elementInTile;
            columns[column][id].elementInTile.MoveElement(columns[column][id].transform.position);
        }
        else
        {
            // Restaurar LA UNION SOVIETICA los objetos que se usaron antes

            //columns[column][id].elementInTile = columns[column][id - 1].elementInTile;
            columns[column][id].elementInTile.MoveElement(spawnTrans[column].transform.position);
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
}
