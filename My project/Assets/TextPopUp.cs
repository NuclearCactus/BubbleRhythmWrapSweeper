using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private TextMeshPro _tmpro;
    [SerializeField] private Animator _animator;

    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        _rectTransform.localPosition += _verticalSpeed * transform.up*Time.deltaTime;
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

    public void SetPopup(string text, TextPopupMood mood)
    {

        _tmpro.text = text;
        switch (mood)
        {
            default:
                _animator.Play("TextPopupNeutral");
                break;
            case (TextPopupMood.Positive):
                _animator.Play("TextPopupPositive");

                break;
            case (TextPopupMood.VeryPositive):
                _animator.Play("TextPopupAmazing");

                break;
            case (TextPopupMood.Negative):
                _animator.Play("TextPopupBad");

                break;
        }
    }
}

public enum TextPopupMood
{
    VeryPositive,
    Positive,
    Neutral,
    Negative
}