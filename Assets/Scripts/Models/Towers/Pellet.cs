using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : _Tower
{
    public override void Init(PlayerController pC, List<Tile> tiles)
    {
        base.Init(pC, tiles);

        base.MyRange = 8f;
        base.MyCooldown = 3f;
        base.myProjectilePrefab = pC.prefabController.ProjectilePelletPrefab;
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (pC == null)
            return;

        base.Update();
    }
}
