using System.Collections.Generic;
using UnityEngine;

public class ElementsGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject prefElement;
    [SerializeField] private int numberOfElements = 0;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private float spawnYpos = 2f;

    // =============================

    static List<GameObject> elementsList;

    static GridManager gridGenerator;

    static GameObject[] spawnTrans;

    // =============================

    public void Awake()
    {
        gridGenerator = GetComponent<GridManager>();

        elementsList = new List<GameObject>();

        GameObject elementsParent = new GameObject();
        elementsParent.transform.parent = this.transform;
        elementsParent.transform.name = "Elements Parent Stack";

        numberOfElements = gridGenerator.grid.Count;

        for (int i = 0; i < numberOfElements; i++)
        {
            GameObject go = Instantiate(prefElement, elementsParent.transform);

            go.transform.name = prefElement.name + "_" + i;

            go.SetActive(false);

            elementsList.Add(go);
        }

        // ====================
        // Create the Spawns:

        var spawnParent = new GameObject();
        spawnParent.transform.parent = this.transform;
        spawnParent.transform.localPosition = Vector3.zero;
        spawnParent.transform.name = "Spawn Parent";

        spawnTrans = new GameObject[gridGenerator.gridSizeX];

        for (int col = 0; col < gridGenerator.gridSizeX; col++)
        {
            spawnTrans[col] = new GameObject();
            spawnTrans[col].transform.parent = spawnParent.transform;
            spawnTrans[col].transform.name = "spawnPoint_" + col;
            spawnTrans[col].transform.localPosition = new Vector2(col * spawnDistance, spawnYpos);
        }
    }

    static public ElementCell RespawnPiece(int column)
    {
        GameObject piece;
        piece = elementsList[0];

        elementsList.Remove(piece);

        piece.transform.position = spawnTrans[column].transform.position;

        piece.SetActive(true);

        elementsList.Add(piece);

        return piece.GetComponent<ElementCell>();
    }

    static public void RestartPiece(GameObject piece)
    {
        elementsList.Remove(piece);

        piece.SetActive(false);

        elementsList.Insert(0, piece);

    }
}
