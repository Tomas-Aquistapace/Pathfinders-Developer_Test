using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile State")]
    public ElementCell elementInTile;
    public int column = 0;
    public int row = 0;

    public enum State
    {
        Waiting,
        Full,
        Empty
    }

    public State tileState;

    // -------------------------------

    [Header("Stetic Values")]
    [SerializeField] private Color highlightedColor = Color.white;

    private SpriteRenderer spriteRenderer;

    // -------------------------------

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        tileState = State.Empty;
    }

    // ------------------------------
    // Move the Cell down:

    public void UpdateTile(ref Tile lastTile, int col, int row)
    {
        if (tileState == State.Empty && elementInTile == null)
        {
            if (row >= 1)
            {
                if(lastTile.elementInTile != null && lastTile.elementInTile.MoveElement(this.transform.position))
                {
                    tileState = State.Waiting;
                    elementInTile = lastTile.elementInTile;
                    lastTile.elementInTile = null;
                    lastTile.tileState = State.Empty;
                }
            }
            else
            {
                tileState = State.Waiting;
                elementInTile = ElementsGenerator.RespawnPiece(col);
                elementInTile.MoveElement(this.transform.position);
            }        
        }
        else if(tileState == State.Waiting && elementInTile != null)
        {
            if(elementInTile.elementState == ElementCell.State.Stop)
            {
                tileState = State.Full;
                elementInTile.elementState = ElementCell.State.Waiting;
            }
        }
        else if(tileState == State.Full && elementInTile == null)
        {
            tileState = State.Empty;
        }
    }

    // ----------------------------
    // Click on the Tile:

    public void ClickEvent()
    {
        if (tileState == State.Full)
        {
            elementInTile.EarnElement();
            elementInTile = null;
        }
    }

    private void OnMouseEnter()
    {
        if(tileState == State.Full)
            spriteRenderer.color = highlightedColor;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;        
    }

    public void CleanTile()
    {
        elementInTile.RestartElement();
        elementInTile = null;

        tileState = State.Empty;
    }

    // -------------------------------------------

    public bool HableToActivate()
    {
        return (tileState == State.Full);
    }

    public int GetElementID()
    {
        if (elementInTile)
            return elementInTile.GetID();
        else
            return 0;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetValues(int _column, int _row)
    {
        column = _column;
        row = _row;
    }

    public bool EqualTo(int _column, int _row)
    {
        return (column == _column && row == _row);
    }
}
