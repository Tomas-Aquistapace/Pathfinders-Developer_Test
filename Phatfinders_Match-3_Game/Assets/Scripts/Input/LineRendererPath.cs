using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererPath : MonoBehaviour
{
    [SerializeField] private GameObject pointPref;
    [SerializeField] private int maxMatchTouchs = 15;

    private List<GameObject> pointsList;

    private LineRenderer lineRenderer;
    private GameObject pointsParent;

    // ----------------------------

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        pointsList = new List<GameObject>();

        pointsParent = new GameObject();
        pointsParent.transform.parent = this.transform;
        pointsParent.transform.name = "Points of the Line";
    }

    private void Update()
    {
        if (lineRenderer.enabled)
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                lineRenderer.SetPosition(i, pointsList[i].transform.localPosition);
            }
        }
    }

    public void InstantiatePoint(Vector3 newPosition)
    {
        GameObject go = Instantiate(pointPref, pointsParent.transform);
        go.transform.position = newPosition;

        pointsList.Add(go);
        go.transform.name = pointPref.name + "_" + pointsList.Count;

        int obj = 0;
        for (int i = 0; i < pointsList.Count; i++)
        {
            if(pointsList[i].activeSelf)
                obj++;
        }
        if (obj > 1)
            lineRenderer.enabled = true;

        lineRenderer.positionCount = obj;
    }

    public void DisablePoints()
    {
        lineRenderer.enabled = false;
     
        if(pointsList.Count > 0)
        { 
            for (int i = 0; i < pointsList.Count; i++)
            {
                Destroy(pointsList[i]);
            }
            pointsList.Clear();
        }
    }
}
