using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Normal : _Enemy
{
    //Child values assigned to parent properties in init
    private float hp = 30f;
    private float speedModifer = 6f;
    private bool ignoreTerrain = false;
    private float armour = 1f;

    public override void Init(PlayerController pC, Tile startNode, Tile targetNode)
    {
        base.SpeedModifier = speedModifer;
        base.IgnoreTerrain = ignoreTerrain;
        base.Armour = armour;
        base.HealthPoints = hp + pC.enemyController.WaveNumber;

        //base
        base.Init(pC, startNode, targetNode);
    }
    protected override void Update()
    {
        base.Update();
    }
}
