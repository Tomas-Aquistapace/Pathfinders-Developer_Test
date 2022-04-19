using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public ElementCell elementInTile;
    public int column = 0;
    public int id = 0;

    enum State
    {
        Waiting,
        Full,
        Empty
    }

    private State tileState;


    // -------------------------------

    private void Awake()
    {
        tileState = State.Empty;
    }

    private void Update()
    {
        
        if(tileState == State.Empty)
        {
            tileState = State.Waiting;

            // GridGenerator.CallAnElement(column, id);
        }

    }


    public void SetValues(int _column, int _id)
    {
        column = _column;
        id = _id;
    }
}
