using System.Collections;
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

    static Queue<GameObject> elementsQueue;
    static GridManager gridGenerator;

    static GameObject[] spawnTrans;

    private GameObject spawnParent;

    // =============================

    public void Awake()
    {
        gridGenerator = GetComponent<GridManager>();
        elementsQueue = new Queue<GameObject>();

        // ===================
        // Create Objects:

        GameObject elementsParent = new GameObject();
        elementsParent.transform.parent = this.transform;
        elementsParent.transform.name = "Elements Parent";

        numberOfElements = gridGenerator.grid.Count;

        for (int i = 0; i < numberOfElements; i++)
        {
            GameObject go = Instantiate(prefElement, elementsParent.transform);

            go.transform.name = prefElement.name + "_" + i;

            go.SetActive(false);

            elementsQueue.Enqueue(go);
        }

        // ====================
        // Create the Spawns:

        spawnParent = new GameObject();
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
        piece = elementsQueue.Dequeue();

        piece.transform.position = spawnTrans[column].transform.position;

        piece.SetActive(true);
        elementsQueue.Enqueue(piece);

        return piece.GetComponent<ElementCell>();
    }
}
