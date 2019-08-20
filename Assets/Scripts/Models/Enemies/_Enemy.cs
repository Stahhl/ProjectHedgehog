using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;
using System.Linq;

public abstract class _Enemy : MonoBehaviour, IQPathUnit
{
    //Properties
    public Node StartNode { get; protected set; }
    public Node TargetNode { get; protected set; }
    public Node CurrentNode { get; protected set; }
    public float SpeedModifier { get; protected set; }
    public bool IgnoreTerrain { get; protected set; }
    public bool IsDestroyed { get; protected set; }
    public bool ReachedTarget { get; protected set; }
    public List<Node> MyPath { get; protected set; }

    //Fields
    protected PlayerController pC;
    private Node nextNode;
    private int index;

    public virtual void Init(PlayerController pC, Node startNode, Node targetNode)
    {
        this.pC = pC;
        this.StartNode = startNode;
        this.TargetNode = targetNode;

        this.CurrentNode = this.StartNode;
    }

    protected virtual void Update()
    {
        if (pC == null && IsDestroyed == true)
            return;

        UpdateMovement();
        //CheckIfReachedGoal();
    }
    void UpdateMovement()
    {
        if (nextNode == null && index == 0)
            nextNode = MyPath[index];

        Vector3 targetPos = nextNode.GetPosition();

        Vector3 direction = targetPos - transform.position;

        //if (direction != Vector3.zero)
        //    UpdateRotation(direction);

        transform.Translate(direction.normalized * SpeedModifier * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetPos) <= 0.2f)
        {
            CurrentNode = nextNode;
            GetNextTile();
        }
    }
    void GetNextTile()
    {
        index++;

        if (index >= MyPath.Count)
            return;

        nextNode = MyPath[index];
    }
    public Node[] GetPath()
    {
        if (MyPath == null)
            return null;

        return MyPath.ToArray();
    }
    public int CostToEnterNode(Node oldTile, Node newTile)
    {
        //Debug.Log("Tile: " + tile.ToString() + " Movement cost to enter: " + tile.movementCost);
        int movementCostToEnterNewTile = newTile.GetMovementCost(IgnoreTerrain);

        if (newTile.MyNodeType == NodeType.FRIENDLY)
        {
            movementCostToEnterNewTile -= 99;
        }

        return movementCostToEnterNewTile;
    }
    public float AggregateTurnsToEnterTile(Node tile, float turnsToDate)
    {
        Node[] tPath = GetPath();

        Node newTile = tile;
        Node oldTile = StartNode;

        if (tPath != null && tPath.Length > 1)
            oldTile = tPath[tPath.Length - 1];

        return CostToEnterNode(oldTile, newTile);
    }
    //Turn cost to enter tile
    public float CostToEnterTile(IQPathTile sourceTile, IQPathTile destinationTile)
    {
        //Debug.Log("CostToEnterTile");
        return 1f;
    }
}
