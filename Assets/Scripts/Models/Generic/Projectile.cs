using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Fields
    private GameObject targetObj;
    private Vector3 targetPos;

    private PlayerController pC;
    private _Tower myTower;
    private _Enemy myEnemy;


    public void Init(PlayerController pC, _Tower tower, GameObject target)
    {
        this.pC = pC;
        this.myTower = tower;

        this.targetObj = target;

        this.myEnemy = target.GetComponent<_Enemy>();
        this.targetPos = target.transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        if (targetObj == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = targetPos - transform.position;

        float travelFrame = 70 * Time.deltaTime;

        if (dir.magnitude <= travelFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * travelFrame, Space.World);
    }
    private void HitTarget()
    {
        //Debug.Log("HitTarget");
        pC.combatController.DamageCalculator_TowerToEnemy(myTower, myEnemy);
        Destroy(gameObject);
    }
}
