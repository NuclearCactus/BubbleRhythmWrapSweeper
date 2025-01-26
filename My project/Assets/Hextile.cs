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
    public bool HasBeenPopped = false;
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


            float TimingPoints = GameManager.Instance.ScoreSetter.GetPointsForTiming();
            if (TimingPoints > 10)
            {
                Animator.Play("TilePerfect");
            }
            else if (TimingPoints < 0) 
            {
                Animator.Play("TileBad");

            }

            PlaySound();
            
            ClickLogic();
                    GameManager.Instance.tileClick();

        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {

            if (!_flag.enabled && !HasBeenClicked)
            {
                
                    _flag.enabled = true;
                    HasBeenClicked = true;

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

    protected virtual void PlaySound()
    {
        if (GameManager.Instance.ScoreSetter.GetPointsForTiming() > 0)
        {
            GameManager.Instance.PlayBubblePopSound();

        }
        else
        {
            GameManager.Instance.PlayFailSound();

        }
    }

    public virtual void ClickLogic()
    {
        //Debug.Log($"{name} on position {HexPosition.GetCube()} got clicked!");
        HasBeenClicked = true;
        HasBeenPopped = true;
        GameManager.Instance.CheckIfEnd();

        ClickEvent?.Invoke();
        
    }

    public void ClickFromEnvironment()
    {
        GameManager.Instance.PlayBubblePopSound(0.35f);

        ClickLogic();
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
