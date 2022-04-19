using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] int numberOfElements = 50;
    [SerializeField] private GameObject[] prefElements;
    
    private GameObject prefElement;

    // =============================

    private Queue<GameObject> elementsQueue;
    //private GridEditor gridEditor;

    // =============================

    public void Awake()
    {
        Object pref = Resources.Load("Bridge/Piece_of_Bridge", typeof(GameObject));
        prefElement = (GameObject)pref;

        elementsQueue = new Queue<GameObject>();

        for (int i = 0; i < numberOfElements; i++)
        {
            GameObject go = Instantiate(prefElement);
            go.transform.parent = this.transform;

            go.transform.name = prefElement.name + "_" + i;

            go.SetActive(false);

            elementsQueue.Enqueue(go);
        }
    }

    private void Update()
    {
        //if (spawnerState != SpawnerState.Stop)
        //{
        //    timeBridge += Time.deltaTime;
        //
        //    if (timeBridge >= timeToRespawnBridge)
        //    {
        //        RespawnPiece();
        //
        //        timeToRespawnBridge = Random.Range(minTimeToRespawnBridge, maxTimeToRespawnBridge);
        //
        //        timeBridge = 0f;
        //    }
        //}
    }

    public void RespawnPiece()
    {
        //GameObject piece;
        //piece = elementsQueue.Dequeue();

        //float rand = Random.Range(-bridgeSpawner.localScale.x / 2, bridgeSpawner.localScale.x / 2);
        //Vector3 newPos = new Vector3(rand, bridgeSpawner.position.y, bridgeSpawner.position.z);

        //piece.transform.GetComponent<PieceController>().ResetState(newPos);

        //switch (spawnerState)
        //{
        //    case SpawnerState.Initializing:

        //        piece.SetActive(true);
        //        elementsQueue.Enqueue(piece);

        //        break;

        //    case SpawnerState.Updating:

        //        elementsQueue.Enqueue(piece);

        //        break;
        //}
    }
}
