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

    private void Start()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (HasBeenClicked)
            return;


        HasBeenClicked = true;
        ClickLogic();
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


}
