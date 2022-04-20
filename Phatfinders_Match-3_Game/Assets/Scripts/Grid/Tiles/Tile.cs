using UnityEngine;

public class Tile : MonoBehaviour
{
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

    private void Awake()
    {
        tileState = State.Empty;
    }

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

    public void SetValues(int _column, int _row)
    {
        column = _column;
        row = _row;
    }
}
