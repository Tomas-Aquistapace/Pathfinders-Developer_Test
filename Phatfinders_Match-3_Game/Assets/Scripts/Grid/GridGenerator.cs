using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Grid Cell")]
    [SerializeField] private GameObject gridCellPref;

    [Header("Grid Values")]
    [SerializeField] private Vector3 gridPosition;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private float distance = 2f;

    [SerializeField] private List<GameObject> grid = new List<GameObject>();

    // -------------------------------

    public void GenerateGrid()
    {
        transform.position = gridPosition;

        for (int row = 0; row < (int)gridSize.y; row++)
        {
            for (int col = 0; col < (int)gridSize.x; col++)
            {
                var go = Instantiate(gridCellPref, this.transform);

                go.transform.localPosition = new Vector2(col * distance, row * -distance);

                grid.Add(go);
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
            grid.Clear();
        }
    }

}
