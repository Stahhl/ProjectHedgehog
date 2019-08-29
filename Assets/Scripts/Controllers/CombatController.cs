using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    //Fields
    private PlayerController pC;

    public void Init(PlayerController pC)
    {
        this.pC = pC;
    }
    public void DamageCalculator(_Tower tower, _Enemy enemy)
    {
        //Debug.Log();
        enemy.AdjustHp(10f);
    }
}
