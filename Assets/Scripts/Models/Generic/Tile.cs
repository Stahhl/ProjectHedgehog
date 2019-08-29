using QPath;
using System.Collections.Generic;
using UnityEngine;
using static EnumLibrary;

public class Tile : MonoBehaviour, IQPathTile
{
    //Properties
    public int X { get; private set; }
    public int Y { get; private set; }
    public TileType MyTileType { get; set; }

    //Fields
    private PlayerController pC;
    private Tile[] myNeighbours;

    public string myName;
    public GameObject spriteObj;

    public void Init(PlayerController pC)
    {
        this.pC = pC;
            
        this.X = Mathf.FloorToInt(this.transform.position.x);
        this.Y = Mathf.FloorToInt(this.transform.position.y);

        this.MyTileType = TileType.OPEN;

        this.myName = "Tile_" + X + "_" + Y;
    }
    //Any movement stuff relevant to this tile
    public float GetMovementCost(bool ignoreTerrain)
    {
        float cost = 1f;

        if (ignoreTerrain == true)
            return cost;
        if (MyTileType == TileType.OCCUPIED)
            cost -= 99;

        return cost;
    }

    public Vector3 GetTilePosition()
    {
        //Debug.Log("GetTilePosition");
        return new Vector3(X, Y, 0);
    }
    #region QPath
    public IQPathTile[] GetNeighbours()
    {
        //Debug.Log("GetNeighbours: " + gameObject.name + ": " + X + "_" + Y);
        if (myNeighbours != null)
        {
            //Debug.Log("GetNeighbours - return");
            return myNeighbours;
        }

        List<Tile> neighbours1 = new List<Tile>();

        //TODO: clean this code up, a loop?
        //Every tile has six neighbours
        neighbours1.Add(pC.tileController.GetTileAt(X + 0, Y + 1)); //N
        neighbours1.Add(pC.tileController.GetTileAt(X + 1, Y + 0)); //E
        neighbours1.Add(pC.tileController.GetTileAt(X + 0, Y - 1)); //S
        neighbours1.Add(pC.tileController.GetTileAt(X - 1, Y + 0)); //W

        //A neighbour could be null
        //A second list were nulls are removed
        List<Tile> neighbours2 = new List<Tile>();

        foreach (Tile t in neighbours1)
        {
            if (t != null)
            {
                neighbours2.Add(t);
            }
        }

        if(neighbours2.Count < 1)
        {
            Debug.Log(myName + " neighbours < 1");
        }
        myNeighbours = neighbours2.ToArray();
        return myNeighbours;
    }
    public float AggregateCostToEnter(float costSoFar, IQPathTile sourceTile, IQPathTile[] twins, IQPathUnit theUnit)
    {
        return ((_Enemy)theUnit).TileToTileCost(this, twins);
    }
    #endregion
}
