using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;
using System.Linq;
using System;

public abstract class _Enemy : MonoBehaviour, IQPathUnit
{
    //Properties
    public Tile StartNode { get; protected set; }
    public Tile TargetNode { get; protected set; }
    public Tile CurrentNode { get; protected set; }
    public float SpeedModifier { get; protected set; }
    public bool IgnoreTerrain { get; protected set; }
    public bool IsDestroyed { get; protected set; }
    public bool ReachedTarget { get; protected set; }
    public List<Tile> MyPath { get; protected set; }

    //Fields
    protected PlayerController pC;
    private Tile nextNode;
    private int index;

    public virtual void Init(PlayerController pC, Tile startNode, Tile targetNode)
    {
        this.pC = pC;
        this.StartNode = startNode;
        this.TargetNode = targetNode;

        this.CurrentNode = this.StartNode;

        this.MyPath = QPath.QPath.FindPath<Tile>(pC, this, StartNode, TargetNode, Tile.CostEstimate).ToList();
        Debug.Log("MyPath: " + MyPath.Count);
    }

    protected virtual void Update()
    {
        if (pC == null && IsDestroyed == true)
            return;

        UpdateMovement();
        CheckIfReachedGoal();
    }

    private void CheckIfReachedGoal()
    {
        if (CurrentNode == TargetNode)
        {
            IsDestroyed = true;
            ReachedTarget = true;
        }
    }

    private void UpdateMovement()
    {
        if (nextNode == null && index == 0)
            nextNode = MyPath[index];

        Vector3 targetPos = nextNode.GetTilePosition();

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
    public Tile[] GetPath()
    {
        if (MyPath == null)
            return null;

        return MyPath.ToArray();
    }
    public int CostToEnterNode(Tile oldTile, Tile newTile)
    {
        //Debug.Log("Tile: " + tile.ToString() + " Movement cost to enter: " + tile.movementCost);
        int movementCostToEnterNewTile = newTile.GetMovementCost(IgnoreTerrain);

        if (newTile.MyTileType == TileType.FRIENDLY)
        {
            movementCostToEnterNewTile -= 99;
        }

        return movementCostToEnterNewTile;
    }
    public float AggregateTurnsToEnterTile(Tile tile, float turnsToDate)
    {
        Tile[] tPath = GetPath();

        Tile newTile = tile;
        Tile oldTile = StartNode;

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
