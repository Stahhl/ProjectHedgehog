using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles in game logic (damage calc)
/// Keeping track of game state (player life)
/// </summary>

public class CombatController : MonoBehaviour
{
    //Fields
    private PlayerController pC;

    public int PlayerLives { get; private set; }

    public void Init(PlayerController pC)
    {
        this.pC = pC;

        PlayerLives = 20;
    }
    //direct hit
    //TODO damage calculator for splash damage (explosions)
    //TODO damage calculator that accounts for range (shotgun?)
    public void DamageCalculator_TowerToEnemy(_Tower tower, _Enemy enemy)
    {
        float ap = 100 - (enemy.Armour / tower.ArmourPeneration);
        float armourFactor = Mathf.Clamp(ap, 0.05f, 0.95f);

        float damage = tower.Damage * armourFactor;

        //Debug.Log("damage = " + damage);
        enemy.AdjustHp(damage);
    }
    public void EnemyReachedTarget()
    {
        Debug.Log("Enemy reached target! ");
        PlayerLives--;
    }
}
