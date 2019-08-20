using QPath;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    NULL,
    OPEN,
    OCCUPIED,
    ENEMY,
    ENEMYSPAWN,
    ENEMYTARGET,
    FRIENDLY
}

public class Node : MonoBehaviour, IQPathTile
{
    //Properties
    public GameObject spriteObj;
    public float X { get; private set; }
    public float Y { get; private set; }
    public NodeType MyNodeType { get; set; }


    //Fields
    private PlayerController pC;

    public void Init(PlayerController pC)
    {
        this.pC = pC;
            
        this.X = this.transform.position.x;
        this.Y = this.transform.position.y;
    }
    public int GetMovementCost(bool ignoreTerrain)
    {
        int cost = 1;

        if (ignoreTerrain == true)
            return cost;
        if (MyNodeType == NodeType.OCCUPIED)
            cost -= 99;

        return cost;
    }
    public static float CostEstimate(IQPathTile aa, IQPathTile bb)
    {
        return Distance((Node)aa, (Node)bb);
    }
    public static float Distance(Node a, Node b)
    {
        float dC = Mathf.Abs(a.X - b.X); //Column
        float dR = Mathf.Abs(a.Y - b.Y); //Row

        var x = Mathf.Max(dC, dR);
        //Debug.Log(x);
        return x;
    }
    public Vector3 GetPosition()
    {
        //Debug.Log("GetTilePosition");
        return new Vector3(X, Y, 0);
    }
    #region QPath
    Node[] neighbours;

    public IQPathTile[] GetNeighbours()
    {
        //Debug.Log("Tile -- GetNeighbours of: " + C + "_" + R);
        if (this.neighbours != null)
            return this.neighbours;

        List<Node> neighbours1 = new List<Node>();

        //TODO: clean this code up, a loop?
        //Every tile has six neighbours
        neighbours1.Add(pC.arenaController.GetNodeAt(X + 0, Y + 1)); //N
        neighbours1.Add(pC.arenaController.GetNodeAt(X + 1, Y + 0)); //E
        neighbours1.Add(pC.arenaController.GetNodeAt(X + 0, Y - 1)); //S
        neighbours1.Add(pC.arenaController.GetNodeAt(X - 1, Y + 0)); //W

        //A neighbour could be null
        //A second list were nulls are removed
        List<Node> neighbours2 = new List<Node>();

        foreach (Node t in neighbours1)
        {
            if (t != null)
            {
                neighbours2.Add(t);
            }
        }

        //Debug.Log("neighbours2: " + neighbours2.Count);
        this.neighbours = neighbours2.ToArray();
        return this.neighbours;
    }
    public float AggregateCostToEnter(float costSoFar, IQPathTile sourceTile, IQPathUnit theUnit)
    {
        return ((_Enemy)theUnit).AggregateTurnsToEnterTile(this, costSoFar);
    }
    #endregion
}
