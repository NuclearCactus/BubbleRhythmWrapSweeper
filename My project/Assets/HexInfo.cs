using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : Hextile
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private Color[] colorList;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void ClickLogic()
    {
        base.ClickLogic();

        int idx = GameManager.Instance.CustomGridScript.GetMineAmountAroundTile(HexPosition);

        if(idx == 0)
        {
            GameManager.Instance.CustomGridScript.ClickAroundTile(HexPosition);
        }

        spriteRenderer.color = colorList[idx];
    }
}
