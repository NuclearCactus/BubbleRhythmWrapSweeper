using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Hextile : MonoBehaviour, IPointerClickHandler
{
    public HexgridPosition HexPosition;
    public bool HasBeenClicked = false;
    protected SpriteRenderer SpriteRenderer;
    [SerializeField] private Color _highLightColor;
    [SerializeField] protected UnityEvent ClickEvent;
    [SerializeField] protected Animator Animator;
    [SerializeField] SpriteRenderer _flag;


    private void Start()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        GameManager.Instance.metronome.Beat += Beat;
        _flag.enabled = false;

    }
    private void OnDestroy()
    {
        GameManager.Instance.metronome.Beat -= Beat;

    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            if (HasBeenClicked)
                return;


            HasBeenClicked = true;
            ClickLogic();
                    GameManager.Instance.tileClick();

        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {

            if (!_flag.enabled && !HasBeenClicked)
            {
                _flag.enabled= true;
                HasBeenClicked = true;

                Debug.Log("works pls ");
                GameManager.Instance.tileFlag();


            }
            else if (_flag.enabled && HasBeenClicked)
            {
                {
                    _flag.enabled = false;


                    HasBeenClicked = false;

                    GameManager.Instance.tileUnflag();

                }
            }
        }
    }

    public virtual void ClickLogic()
    {
        //Debug.Log($"{name} on position {HexPosition.GetCube()} got clicked!");
        ClickEvent?.Invoke();
        GameManager.Instance.PlayBubblePopSound();
    }


    public virtual void Highlight(float duration)
    {
        StartCoroutine(HighlightCoroutine(duration));
    }

    IEnumerator HighlightCoroutine(float duration)
    {
        Color c = SpriteRenderer.color;
        for (float timer = 0f; timer < duration; timer += 0.1f)
        {
            SpriteRenderer.color = Color.Lerp(_highLightColor,c, timer/duration);
            yield return null;
        }
        SpriteRenderer.color = _highLightColor;

    }

    protected void Beat(object sender, EventArgs e)
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("TileGeneral"))
        {
            Animator.Play("TileBeat");
        }
    }
}
