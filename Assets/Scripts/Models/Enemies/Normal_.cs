using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Normal_ : _Enemy
{
    //Child values assigned to parent properties in init
    private float speedModifer = 6f;
    private bool ignoreTerrain = false;

    public override void Init(PlayerController pC, Tile startNode, Tile targetNode)
    {
        base.SpeedModifier = speedModifer;
        base.IgnoreTerrain = ignoreTerrain;

        //base
        base.Init(pC, startNode, targetNode);
    }
    protected override void Update()
    {
        base.Update();
    }
}
