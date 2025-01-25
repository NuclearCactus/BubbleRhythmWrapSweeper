using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : Hextile
{
    [SerializeField] private Color[] _colorList;
   
    public override void ClickLogic()
    {
        GameManager.Instance.CheckIfEnd();
        HasBeenClicked = true;
        HasBeenPopped = true;
        base.ClickLogic();

        int idx = GameManager.Instance.CustomGridScript.GetMineAmountAroundTile(HexPosition);

        if(idx == 0)
        {
            GameManager.Instance.CustomGridScript.ClickAroundTile(HexPosition);
        }

        SpriteRenderer.color = _colorList[idx];
    }
}
