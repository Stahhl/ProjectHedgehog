using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Tower : MonoBehaviour
{
    //Properties
    public List<Tile> MyTiles { get; protected set; }
    public float MyCooldown { get; protected set; }
    public float MyRange { get; protected set; }

    //Fields
    protected PlayerController pC;
    protected GameObject myProjectilePrefab;
    protected GameObject myTarget;
    protected bool inRange;
    protected float cooldown;

    [SerializeField]
    private GameObject baseObj;
    [SerializeField]
    private GameObject turretObj;
    [SerializeField]
    private GameObject firePoint;

    public virtual void Init(PlayerController pC, List<Tile> tiles)
    {
        this.pC = pC;
        this.MyTiles = tiles;

        foreach (var t in MyTiles)
        {
            t.MyTileType = TileType.OCCUPIED;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        cooldown += Time.deltaTime;

        if (myTarget == null)
            myTarget = FindNeareastEnemy();

        if (myTarget != null)
            myTarget = TrackTarget(myTarget);

        if (myTarget == null)
            return;

        //Has target ------------------
        Vector3 direction = myTarget.transform.position - transform.position;

        UpdateRotation(direction);

        if (cooldown >= MyCooldown)
            Fire();
    }
    void Fire()
    {
        Debug.Log("Fire");
        cooldown = 0f;
        GameObject projObj = Instantiate(myProjectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
        Projectile proj = projObj.GetComponent<Projectile>();

        proj.SetTarget(myTarget.transform.position);
    }
    void UpdateRotation(Vector3 direction)
    {
        //Debug.Log("UpdateRotation");
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //TurrerObj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        turretObj.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        //TurrerObj.transform.LookAt(myTarget.transform, Vector3.up);
    }
    GameObject FindNeareastEnemy()
    {
        GameObject enemyObj = null;
        float? shortDist = null;

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Debug.Log(enemies.Length);
        foreach (var e in enemies)
        {
            float dist = Vector3.Distance(this.transform.position, e.transform.position);

            if (dist >= MyRange)
                continue;

            if (shortDist == null || dist < shortDist)
                enemyObj = e;
        }

        return enemyObj;
    }
    GameObject TrackTarget(GameObject enemyObj)
    {
        float dist = Vector3.Distance(this.transform.position, enemyObj.transform.position);

        if (dist <= MyRange)
        {
            //Debug.Log("TrackTarget: " + dist);
            return enemyObj;
        }

        return null;
    }
}
