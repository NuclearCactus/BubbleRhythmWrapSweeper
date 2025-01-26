using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class HexMine : Hextile
{

    private void Start()
    {
        Animator = GetComponent<Animator>();
        GameManager.Instance.metronome.Beat += Beat;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public override void ClickLogic()
    {
        GameManager.Instance.CheckIfEnd();

        HasBeenClicked = true;
        HasBeenPopped = true;
        ClickEvent?.Invoke();
        Debug.Log($"BOOOOOOOOOOOOOOMMMMMMMM!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        GameManager.Instance.TriggerShake(1, 5f);
        GameManager.Instance.RemoveLife();
        SpriteRenderer.color = Color.red;

    }

    protected override void PlaySound()
    {
        GameManager.Instance.PlayExplosionSound();

    }

}

