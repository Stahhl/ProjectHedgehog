using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles spawning waves and keeping track of enemies
/// </summary>
public class EnemyController : MonoBehaviour
{
    //Properties
    public List<_Enemy> EnemiesList { get; private set; }

    //Fields
    private PlayerController pC;
    private float waveTime;
    private float timer;
    private bool started;
    private Dictionary<_Enemy, GameObject> enemyToGoMap;
    private List<_Enemy> enemiesToDestroyList;

    [SerializeField]
    private GameObject enemyAnchorObj;

    public void Init(PlayerController pC)
    {
        this.pC = pC;

        EnemiesList = new List<_Enemy>();

        enemyToGoMap = new Dictionary<_Enemy, GameObject>();
        enemiesToDestroyList = new List<_Enemy>();

        waveTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pC == null || started == false)
            return;

        UpdateEnemies();

        timer += Time.deltaTime;

        if(timer >= waveTime)
        {
            timer = 0f;
            SendWave();
        }
    }
    public void ForceWave()
    {
        if (started == false)
            started = true;

        SendWave();
    }
    void SendWave()
    {
        Debug.Log("SendWave");

        Tile spawnTile = pC.tileController.GetSpawnTile();
        Tile targetTile = pC.tileController.GetTargetTile();


        GameObject enemyObj = Instantiate(pC.prefabController.EnemyNormalPrefab, new Vector3(spawnTile.X, spawnTile.Y, 0), Quaternion.identity, enemyAnchorObj.transform);
        _Enemy enemy = enemyObj.GetComponentInChildren<_Enemy>();

        EnemiesList.Add(enemy);
        enemyToGoMap[enemy] = enemyObj;

        enemy.Init(pC, spawnTile, targetTile);
    }
    void UpdateEnemies()
    {
        foreach (var enemy in enemyToGoMap)
        {
            if (enemy.Key.IsDestroyed == true)
            {
                if (enemy.Key.ReachedTarget == true)
                    EnemyReachedTarget();

                enemiesToDestroyList.Add(enemy.Key);
            }
        }

        if (enemiesToDestroyList.Count > 0)
            DestroyEnemies();
    }
    void EnemyReachedTarget()
    {
        Debug.Log("Enemy reached target! ");
    }
    void DestroyEnemies()
    {
        foreach (var item in enemiesToDestroyList)
        {
            Destroy(enemyToGoMap[item]);
            enemyToGoMap.Remove(item);
            EnemiesList.Remove(item);
        }

        enemiesToDestroyList.Clear();
    }
}
