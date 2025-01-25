using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositionHelper 
{
    public static HexgridPosition WorldToHex(Vector3 worldPosition)
    {
        int q = Mathf.RoundToInt(Mathf.Sqrt(3f) / 3f * worldPosition.x - 1f / 3f * worldPosition.z);
        int r = Mathf.RoundToInt(2f / 3f * worldPosition.z);

        return new HexgridPosition(q, r);
    }
    public static Vector3 HexToWorld(HexgridPosition hexgridPosition)
    {
        float worldX = (Mathf.Sqrt(2) * hexgridPosition.Q + Mathf.Sqrt(2) / 2f * hexgridPosition.R);
        float worldY = (2f / 2f * hexgridPosition.R);

        return new Vector3(worldX, worldY,0 );
    }
}
