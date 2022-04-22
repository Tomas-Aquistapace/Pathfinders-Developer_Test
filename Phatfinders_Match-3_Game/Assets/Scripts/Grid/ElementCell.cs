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
    [SerializeField] private float animDelay = 0.5f;

    [SerializeField] private GameObject particlePref;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

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
        animator = GetComponentInChildren<Animator>();

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
            StopCoroutine(Move(finalPos));
            StartCoroutine(Move(finalPos));
            return true;
        }
        return false;
    }

    public void EarnElement()
    {
        ElementsGenerator.RestartPiece(this.gameObject);

        ActivateParticles();
        animator.SetTrigger("Destroy");

        EarnPoints?.Invoke(elementID, totalPoints);
    }

    public void RestartElement()
    {
        ElementsGenerator.RestartPiece(this.gameObject);

        StopCoroutine(Move(Vector3.zero));
    }

    // --------------------------

    private void ChageElement()
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
        animator.SetBool("Moving", true);

        while (time < 1)
        {
            time += Time.deltaTime * speed;

            transform.localPosition = Vector3.Lerp(initialPos, finalPos, time);
            
            yield return null;
        }

        elementState = State.Stop;

        yield return new WaitForSeconds(animDelay);

        animator.SetBool("Moving", false);
    }

    private void ActivateParticles()
    {
        var go = Instantiate(particlePref);
        go.transform.position = this.transform.localPosition;

        Destroy(go, 1f);
    }

    // --------------------------

    public int GetID()
    {
        return elementID;
    }
}
