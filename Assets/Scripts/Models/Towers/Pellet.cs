using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : _Tower
{
    //Child values assigned to parent properties in init
    float myRange = 8f;
    float myCooldown = 3f;
    float myDamage = 10f;
    float armourPen = 1f;

    public override void Init(PlayerController pC, List<Tile> tiles)
    {
        base.Init(pC, tiles);

        base.Range = myRange;
        base.Cooldown = myCooldown;
        base.Damage = myDamage;
        base.ArmourPeneration = armourPen;

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
