using QPath;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    NULL,
    OPEN,
    OCCUPIED,
    ENEMY,
    ENEMYSPAWN,
    ENEMYTARGET,
    FRIENDLY
}

public class Tile : MonoBehaviour, IQPathTile
{
    //Properties
    public int X { get; private set; }
    public int Y { get; private set; }
    public TileType MyTileType { get; private set; }


    //Fields
    private PlayerController pC;
    private Tile[] myNeighbours;
    private List<Tile> specialNeighbours;

    public string myName;
    public GameObject spriteObj;

    public void Init(PlayerController pC)
    {
        this.pC = pC;
            
        this.X = Mathf.FloorToInt(this.transform.position.x);
        this.Y = Mathf.FloorToInt(this.transform.position.y);

        this.myName = "Tile_" + X + "_" + Y;
    }
    public int GetMovementCost(bool ignoreTerrain)
    {
        int cost = 1;

        if (ignoreTerrain == true)
            return cost;
        if (MyTileType == TileType.OCCUPIED)
            cost -= 99;

        return cost;
    }
    public static float CostEstimate(IQPathTile aa, IQPathTile bb)
    {
        return Distance((Tile)aa, (Tile)bb);
    }
    public static float Distance(Tile a, Tile b)
    {
        float dC = Mathf.Abs(a.X - b.X); //Column
        float dR = Mathf.Abs(a.Y - b.Y); //Row

        var x = Mathf.Max(dC, dR);
        return x;
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
        neighbours1.Add(pC.arenaController.GetTileAt(X + 0, Y + 1)); //N
        neighbours1.Add(pC.arenaController.GetTileAt(X + 1, Y + 0)); //E
        neighbours1.Add(pC.arenaController.GetTileAt(X + 0, Y - 1)); //S
        neighbours1.Add(pC.arenaController.GetTileAt(X - 1, Y + 0)); //W

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
        if(specialNeighbours != null)
        {
            Debug.Log(myName + " add specialNeighbours");
            neighbours2.AddRange(specialNeighbours);
        }
        if(neighbours2.Count < 1)
        {
            Debug.Log(myName + " neighbours < 1");
        }
        myNeighbours = neighbours2.ToArray();
        return myNeighbours;
    }
    public float AggregateCostToEnter(float costSoFar, IQPathTile sourceTile, IQPathUnit theUnit)
    {
        return ((_Enemy)theUnit).AggregateTurnsToEnterTile(this, costSoFar);
    }
    #endregion
}
