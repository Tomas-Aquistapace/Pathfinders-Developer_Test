using UnityEngine;

public class ConnectElements : MonoBehaviour
{
    [SerializeField] private LineRendererPath lineRendererPath;

    static public LineRendererPath lrp;

    private void Awake()
    {
        lrp = lineRendererPath;
    }

    static public void ActivatePoint(Vector3 newPosition)
    {
        lrp.InstantiatePoint(newPosition);
    }

    static public void DisablePoints()
    {
        lrp.DisablePoints();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider == null)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                DisablePoints();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DisablePoints();
        }
    }
}