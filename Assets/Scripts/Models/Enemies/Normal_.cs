﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_ : _Enemy
{
    //Child values assigned to parent properties in init
    private float speedModifer = 6f;
    private bool ignoreTerrain = false;

    public override void Init(PlayerController pC, Node startNode, Node targetNode)
    {
        base.Init(pC, startNode, targetNode);

        //base
        base.SpeedModifier = speedModifer;
        base.IgnoreTerrain = ignoreTerrain;
        //base.MyPath = QPath.QPath.FindPath<Tile>(GC, this, StartTile, TargetTile, Tile.CostEstimate).ToList();
    }
    protected override void Update()
    {
        base.Update();
    }
}
