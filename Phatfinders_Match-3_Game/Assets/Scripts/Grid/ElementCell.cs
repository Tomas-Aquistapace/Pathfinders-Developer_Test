using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElementCell : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesElem;
    [SerializeField] private int elementID = 0;

    private SpriteRenderer spriteRenderer;

    // -----------------------------

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        ChageElement();
    }

    private void OnEnable()
    {
        ChageElement();
    }

    private void OnDisable()
    {
        
    }

    public void MoveElement(Vector3 finalPos)
    {
        StartCoroutine(Move(finalPos));
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

        Vector3 initialPos = transform.position;

        while (time < 1)
        {
            time += Time.deltaTime;

            transform.position = Vector3.Lerp(initialPos, finalPos, time);
            
            yield return null;
        }
    }
}
