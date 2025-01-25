using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Hextile : MonoBehaviour, IPointerClickHandler
{
    public HexgridPosition HexPosition;
    public bool HasBeenClicked = false;
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _highLightColor;
    [SerializeField] private UnityEvent _clickEvent;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (HasBeenClicked)
            return;


        HasBeenClicked = true;
        ClickLogic();
    }

    public virtual void ClickLogic()
    {
        Debug.Log($"{name} on position {HexPosition.GetCube()} got clicked!");
        _clickEvent?.Invoke();
    }


    public virtual void Highlight(float duration)
    {
        StartCoroutine(HighlightCoroutine(duration));
    }

    IEnumerator HighlightCoroutine(float duration)
    {
        Color c = _spriteRenderer.color;
        for (float timer = 0f; timer < duration; timer += 0.1f)
        {
            _spriteRenderer.color = Color.Lerp(_highLightColor,c, timer/duration);
            yield return null;
        }
        _spriteRenderer.color = _highLightColor;

    }


}
