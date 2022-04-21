using System.Collections.Generic;
using UnityEngine;

public class ConnectElements : MonoBehaviour
{
    [Header("Context")]
    [SerializeField] private LineRendererPath lineRendererPath;
    [SerializeField] private LayerMask layerMask;

    [Header("Actual Combo")]
    [SerializeField] private bool inCombo = false;
    [SerializeField] private int elementID = 0;
    [SerializeField] private int minComboToActivate = 3;
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private List<Tile> tilesCombo = new List<Tile>();

    // ---------------------------

    private void ActivatePoint(Vector3 newPosition)
    {
        lineRendererPath.InstantiatePoint(newPosition);
    }

    private void DisablePoints()
    {
        lineRendererPath.DisablePoints();

        elementID = 0;
        inCombo = false;
        tilesCombo.Clear();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 50, layerMask);
            
            if(hit.collider != null)
            {
                Tile actualTile = hit.transform.GetComponent<Tile>();
                
                if (!tilesCombo.Contains(actualTile))
                {
                    if (actualTile.HableToActivate())
                    {
                        if (!inCombo)
                        {
                            elementID = actualTile.GetElementID();
                            inCombo = true;

                            tilesCombo.Add(actualTile);

                            ActivatePoint(actualTile.GetPosition());
                        }
                        else
                        {
                            if (elementID == actualTile.GetElementID() &&
                                Vector3.Distance(actualTile.GetPosition(), tilesCombo[tilesCombo.Count - 1].GetPosition()) <= maxDistance)
                            {
                                tilesCombo.Add(actualTile);
                                ActivatePoint(actualTile.GetPosition());
                            }
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(tilesCombo.Count >= minComboToActivate)
            {
                for (int i = 0; i < tilesCombo.Count; i++)
                {
                    tilesCombo[i].ClickEvent();
                }
            }
            DisablePoints();
        }
    }
}