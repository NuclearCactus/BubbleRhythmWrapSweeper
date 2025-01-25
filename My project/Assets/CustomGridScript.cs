using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        RevealTiles();


    }
    private void OnEnable()
    {
        RevealTiles();
    }

    public void Reset()
    {
        
        foreach (var tile in _tiles)
        {
            Destroy(tile.Value);
        }
        _tiles.Clear();

        DistributeTilesOnPositions(GenerateTilePostions());

        RevealTiles();
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
        tileObject.transform.SetParent(this.transform,false);
        tileObject.transform.localPosition = worldPos;
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

    internal int GetMineAmountAroundTile(HexgridPosition hexgridPosition)
    {
        int i = 0;
        if(CheckIfMine(hexgridPosition+new HexgridPosition(1, 0)))
        {
            i++;
        }
        if (CheckIfMine(hexgridPosition + new HexgridPosition(-1, 0)))
        {
            i++;
        }


        if (CheckIfMine(hexgridPosition + new HexgridPosition(1, -1)))
        {
            i++;
        }
        if (CheckIfMine(hexgridPosition + new HexgridPosition(-1, 1)))
        {
            i++;
        }

        if (CheckIfMine(hexgridPosition + new HexgridPosition(0, 1)))
        {
            i++;
        }
        if (CheckIfMine(hexgridPosition + new HexgridPosition(0, -1)))
        {
            i++;
        }
        return i;



    }

    private bool CheckIfMine(HexgridPosition hexgridPosition)
    {
        if (_tiles.ContainsKey(hexgridPosition))
        {
            GameObject obj = _tiles.GetValueOrDefault(hexgridPosition);

            if (obj == null)
                return false;
            if (obj.GetComponent<HexMine>() != null)
            {
                return true;
            }
        }
        return false;

    }

    internal void ClickAroundTile(HexgridPosition hexgridPosition)
    {

        StartCoroutine(ClickAroundTileCoroutine(hexgridPosition));


    }

    IEnumerator ClickAroundTileCoroutine(HexgridPosition hexgridPosition)
    {
        ClickTile(hexgridPosition + new HexgridPosition(1, 0));
        yield return new WaitForSeconds(15f/102f);
        ClickTile(hexgridPosition + new HexgridPosition(-1, 0));
        yield return new WaitForSeconds(15f / 102f);


        ClickTile(hexgridPosition + new HexgridPosition(1, -1));
        yield return new WaitForSeconds(15f / 102f);

        ClickTile(hexgridPosition + new HexgridPosition(-1, 1));
        yield return new WaitForSeconds(15f / 102f);

        ClickTile(hexgridPosition + new HexgridPosition(0, 1));
        yield return new WaitForSeconds(15f / 102f);

        ClickTile(hexgridPosition + new HexgridPosition(0, -1));
        yield return new WaitForSeconds(15f / 102f);

    }


    private void ClickTile(HexgridPosition hexgridPosition)
    {
        if (_tiles.ContainsKey(hexgridPosition))
        {
            GameObject obj = _tiles.GetValueOrDefault(hexgridPosition);
            if (obj == null)
                return;
            if (obj.GetComponent<Hextile>() != null)
            {
                //obj.GetComponent<Hextile>().ClickLogic();

                Hextile hextile = obj.GetComponent<Hextile>();
                if (!hextile.HasBeenClicked)
                {
                    hextile.HasBeenClicked = true;
                    hextile.ClickLogic();
                }

            }
        }
    }

    public void RevealTiles(float duration)
    {
        foreach (var tile in _tiles)
        {
            Animator animator = tile.Value.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("TileInvisible");
            }

        }
        StartCoroutine(RevealTilesCoroutine(duration));
    }

    public void RevealTiles()
    {
        RevealTiles(0.5f);
    }
    IEnumerator RevealTilesCoroutine(float duration)
    {
        float durationTest = _tiles.Count;
        foreach(var tile in _tiles)
        {
            Animator animator = tile.Value.GetComponent<Animator>();
            if(animator != null)
            {
                animator.Play("TilePopUp");
                if(UnityEngine.Random.Range(0,11)>6)
                GameManager.Instance.BubbleBlowSound();
                yield return new WaitForSeconds(duration/durationTest);
                
            }
        }
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

    public static HexgridPosition operator +(HexgridPosition a, HexgridPosition b)
    {
        return new HexgridPosition(a.Q+b.Q,a.R+b.R);
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