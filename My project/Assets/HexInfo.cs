using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : Hextile
{
    [SerializeField] private Color[] _colorList;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _numberRenderer;
   
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
        else 
        {
            _numberRenderer.enabled = true;
            _numberRenderer.sprite = _sprites[idx];
        }

        SpriteRenderer.color = _colorList[idx];
    }
}
