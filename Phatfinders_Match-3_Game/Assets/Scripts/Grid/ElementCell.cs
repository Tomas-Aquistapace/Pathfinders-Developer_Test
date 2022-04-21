using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElementCell : MonoBehaviour
{
    public static Action<int, int> EarnPoints;

    [SerializeField] private Sprite[] spritesElem;
    [SerializeField] private int elementID = 0;
    [SerializeField] private int totalPoints = 10;
    [SerializeField] private float speed = 2;

    private SpriteRenderer spriteRenderer;

    public enum State
    {
        Waiting,
        Moving,
        Stop
    }

    public State elementState = State.Waiting;

    // -----------------------------

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        elementState = State.Waiting;

        ChageElement();
    }

    private void OnEnable()
    {
        ChageElement();
    }

    public bool MoveElement(Vector3 finalPos)
    {
        if(elementState == State.Waiting)
        {
            StartCoroutine(Move(finalPos));
            return true;
        }
        return false;
    }

    public void EarnElement()
    {
        ElementsGenerator.RestartPiece(this.gameObject);

        EarnPoints?.Invoke(elementID, totalPoints);
    }

    public void RestartElement()
    {
        ElementsGenerator.RestartPiece(this.gameObject);

        StopCoroutine(Move(Vector3.zero));
    }

    // --------------------------

    void ChageElement()
    {
        elementID = UnityEngine.Random.Range(0, spritesElem.Length);

        spriteRenderer.sprite = spritesElem[elementID];

        elementState = State.Waiting;
    }

    IEnumerator Move(Vector3 finalPos)
    {
        float time = 0f;

        Vector3 initialPos = transform.localPosition;

        elementState = State.Moving;

        while (time < 1)
        {
            time += Time.deltaTime * speed;

            transform.localPosition = Vector3.Lerp(initialPos, finalPos, time);
            
            yield return null;
        }

        elementState = State.Stop;
    }

    // --------------------------

    public int GetID()
    {
        return elementID;
    }
}
