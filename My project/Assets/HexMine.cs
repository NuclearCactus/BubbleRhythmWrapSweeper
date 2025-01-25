using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HexMine : Hextile
{

    SpriteRenderer _flag;
    private void Start()
    {
        Animator = GetComponent<Animator>();
        GameManager.Instance.metronome.Beat += Beat;
        _flag.enabled = false;
    }
    public override void ClickLogic()
    {
        ClickEvent?.Invoke();
        Debug.Log($"BOOOOOOOOOOOOOOMMMMMMMM!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        GameManager.Instance.PlayExplosionSound();
        GameManager.Instance.TriggerShake(1, 5f);
        GameManager.Instance.RemoveLife();
        SpriteRenderer.color= Color.red;

    }

}
