using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : Hextile
{
    [SerializeField] private Color[] colorList;
   
    public override void ClickLogic()
    {
        base.ClickLogic();

        int idx = GameManager.Instance.CustomGridScript.GetMineAmountAroundTile(HexPosition);

        if(idx == 0)
        {
            GameManager.Instance.CustomGridScript.ClickAroundTile(HexPosition);
        }

        _spriteRenderer.color = colorList[idx];
    }
}
