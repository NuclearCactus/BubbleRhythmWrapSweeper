using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMine : Hextile
{
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void ClickLogic()
    {
        base.ClickLogic();
        Debug.Log($"BOOOOOOOOOOOOOOMMMMMMMM!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        spriteRenderer.color= Color.red;
    }
}
