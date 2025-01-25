using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hextile : MonoBehaviour, IPointerClickHandler
{
    public HexgridPosition HexPosition;
    public bool HasBeenClicked = false;



    public void OnPointerClick(PointerEventData eventData)
    {
        if (HasBeenClicked)
            return;


        HasBeenClicked = true;
        ClickLogic();
    }

    private void ClickLogic()
    {
        Debug.Log($"{name} on position {HexPosition.GetCube()} got clicked!");
    }

}
