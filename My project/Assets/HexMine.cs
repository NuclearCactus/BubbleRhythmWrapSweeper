using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMine : Hextile
{
  
    public override void ClickLogic()
    {
        base.ClickLogic();
        Debug.Log($"BOOOOOOOOOOOOOOMMMMMMMM!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        GameManager.Instance.PlayExplosionSound();
        _spriteRenderer.color= Color.red;
    }
}
