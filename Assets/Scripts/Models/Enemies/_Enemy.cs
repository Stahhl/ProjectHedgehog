﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;
using System.Linq;
using System;

public abstract class _Enemy : MonoBehaviour, IQPathUnit
{
    //Properties
    public Tile StartTile { get; protected set; }
    public Tile TargetTile { get; protected set; }
    public Tile CurrentTile { get; protected set; }
    public float SpeedModifier { get; protected set; }
    public bool IgnoreTerrain { get; protected set; }
    public bool IsDestroyed { get; protected set; }
    public bool ReachedTarget { get; protected set; }
    public List<Tile> MyPath { get; protected set; }

    //Fields
    protected PlayerController pC;
    private Tile nextTile;
    private int index;

    public virtual void Init(PlayerController pC, Tile startTile, Tile targetTile)
    {
        this.pC = pC;
        this.StartTile = startTile;
        this.TargetTile = targetTile;

        this.CurrentTile = this.StartTile;
        this.MyPath = QPath.QPath.FindPath<Tile>(pC, this, StartTile, TargetTile, Tile.CostEstimate).ToList();
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
        if (CurrentTile == TargetTile)
        {
            IsDestroyed = true;
            ReachedTarget = true;
        }
    }

    private void UpdateMovement()
    {
        if (nextTile == null && index == 0)
            nextTile = MyPath[index];

        Vector3 targetPos = nextTile.GetTilePosition();

        Vector3 direction = targetPos - transform.position;

        //if (direction != Vector3.zero)
        //    UpdateRotation(direction);

        transform.Translate(direction.normalized * SpeedModifier * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetPos) <= 0.2f)
        {
            CurrentTile = nextTile;
            GetNextTile();
        }
    }
    void GetNextTile()
    {
        index++;

        if (index >= MyPath.Count)
            return;

        nextTile = MyPath[index];
    }
    public float TileToTileCost(Tile newTile, IQPathTile[] tiles)
    {
        //Debug.Log("Tile: " + tile.ToString() + " Movement cost to enter: " + tile.movementCost);
        float tileMovementCost = newTile.GetMovementCost(IgnoreTerrain);

        //Penalty to moving diagonally to prevent excesive zigzagging
        if (
            tiles != null && 
            tiles.Length == 2 && 
            tiles[0] != null && 
            tiles[1] != null
            )
        {
            Vector3 olderPos = tiles[0].GetTilePosition();
            Vector3 oldPos = tiles[1].GetTilePosition();
            Vector3 newPos = newTile.GetTilePosition();

            //Debug.Log(olderPos + " " + oldPos + " " + newPos);
            if(olderPos.x == oldPos.x && olderPos.x != newPos.x)
            {
                tileMovementCost += 1f;
            }
            if (olderPos.y == oldPos.y && olderPos.y != newPos.y)
            {
                tileMovementCost += 1f;
            }
        }
        if (newTile.MyTileType == TileType.FRIENDLY)
        {
            tileMovementCost -= 99;
        }

        return tileMovementCost;
    }
    public float CostToEnterTile(IQPathTile sourceTile, IQPathTile destinationTile)
    {
        //Debug.Log("CostToEnterTile");
        return 1f;
    }
}
