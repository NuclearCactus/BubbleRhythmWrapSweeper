using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomGridScript : MonoBehaviour
{
    [SerializeField] private Vector2 _cellSize;
    [SerializeField] private int _gridRadius;
    [SerializeField] private Hextile _normalTile;
    [SerializeField] private List<SpecialTileContainer> _specialTiles;


    private Dictionary<HexgridPosition, GameObject> _tiles = new Dictionary<HexgridPosition, GameObject>();


    private void Start()
    {
        DistributeTilesOnPositions(GenerateTilePostions());

    }

    private void DistributeTilesOnPositions(List<HexgridPosition> hexgridPositions)
    {
        IListExtensions.Shuffle(hexgridPositions);
        int currentTileIndex = 0;
        foreach(SpecialTileContainer cntr in _specialTiles)
        {
            for (int i = 0; i < cntr.SpecialTileAmount; i++) 
                {
                if (currentTileIndex < hexgridPositions.Count)
                {
                    PlaceTilePrefab(cntr.SpecialTile, hexgridPositions[currentTileIndex]);
                    currentTileIndex++;
                }
                else
                {
                    return;
                }
                }
        }
        while (currentTileIndex < hexgridPositions.Count)
        {
            
           PlaceTilePrefab(_normalTile, hexgridPositions[currentTileIndex]);
           currentTileIndex++;
            
        }
    }

    private void PlaceTilePrefab(Hextile Tile, HexgridPosition hexgridPosition)
    {
        Vector3 worldPos = PositionHelper.HexToWorld(hexgridPosition);
        GameObject tileObject = Instantiate(Tile.gameObject, worldPos, Quaternion.identity);
        Hextile hextile = tileObject.GetComponent<Hextile>();
        hextile.HexPosition = hexgridPosition;
        _tiles.Add(hexgridPosition, tileObject);
    }

    private List<HexgridPosition> GenerateTilePostions()
    {
        List<HexgridPosition> positionList = new List<HexgridPosition>();

        //Spawn all the tiles in the radius
        for (int q = -_gridRadius; q <= _gridRadius; q++)
        {
            int r1 = Mathf.Max(-_gridRadius, -q - _gridRadius);
            int r2 = Mathf.Min(_gridRadius, -q + _gridRadius);
            for (int r = r1; r <= r2; r++)
            {
                HexgridPosition position = new HexgridPosition(q, r);
                positionList.Add(position);
            }
        }
        return positionList; 
    }
}


[Serializable] public class SpecialTileContainer
{
    public Hextile SpecialTile;
    public int SpecialTileAmount;
}


public struct HexgridPosition
{
    public int Q { get; set; }      // axis x, direction: \
    public int R { get; set; }      // axis y, direction: -
    public HexgridPosition(int q, int r)
    {
        Q = q;
        R = r;
    }
    public override string ToString()
    {
        return $"HexgridPosition ({Q}, {R})";
    }
    public static bool operator ==(HexgridPosition a, HexgridPosition b)
    {
        return a.Equals(b);
    }
    public static bool operator !=(HexgridPosition a, HexgridPosition b)
    {
        return !(a == b);
    }

    public Vector2 GetAxial()
    {
        return new Vector2(Q, R);
    }

    public Vector3 GetCube()
    {
        return new Vector3(Q, R, -Q-R);
    }
}