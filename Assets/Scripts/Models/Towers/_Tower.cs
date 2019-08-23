using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Tower : MonoBehaviour
{
    //Fields
    private PlayerController pC;

    //Properties
    public List<Tile> MyTiles { get; protected set; }

    public virtual void Init(PlayerController pC, List<Tile> tiles)
    {
        this.pC = pC;
        this.MyTiles = tiles;

        foreach (var t in MyTiles)
        {
            t.MyTileType = TileType.OCCUPIED;
        }
    }
}
