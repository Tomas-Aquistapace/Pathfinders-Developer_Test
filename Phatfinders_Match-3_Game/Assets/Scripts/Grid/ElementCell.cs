using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElementCell : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesElem;
    [SerializeField] private int elementID = 0;
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

    private void OnDisable()
    {
        
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

    // --------------------------

    void ChageElement()
    {
        elementID = Random.Range(1, spritesElem.Length);

        spriteRenderer.sprite = spritesElem[elementID];
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
}
