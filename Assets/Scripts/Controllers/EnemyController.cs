using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumLibrary;

/// <summary>
/// Handles spawning waves and keeping track of enemies
/// </summary>
public class EnemyController : MonoBehaviour
{
    //Properties
    public List<_Enemy> EnemiesList { get; private set; }
    public int WaveNumber { get; private set; }

    //Fields
    private bool evenWave;
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
    private void Update()
    {
        if (pC == null || started == false)
            return;

        UpdateEnemies();
        UpdateWaveTimer();
    }
    private void UpdateWaveTimer()
    {
        timer += Time.deltaTime;

        if (timer >= waveTime)
            SendWave();
    }
    public void ForceWave()
    {
        if (started == false)
            started = true;

        SendWave();
    }
    private void SendWave()
    {
        Debug.Log("SendWave");
        timer = 0f;
        WaveNumber++;

        StartCoroutine(SpawnTimedWave(4, WaveType.NORMAL));
    }
    private IEnumerator SpawnTimedWave(int nbEnemies, WaveType waveType)
    {
        for (int i = 0; i < nbEnemies; i++)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                GameObject prefabToRender = null;

                //Enemy prefab
                switch (waveType)
                {
                    case WaveType.NORMAL:
                        prefabToRender = pC.prefabController.EnemyNormalPrefab;
                        break;
                }

                SpawnEnemy(prefabToRender);
                break;
            }
        }
    }
    private void SpawnEnemy(GameObject prefabToRender)
    {
        Tile spawnTile = pC.tileController.GetSpawnTile(WaveNumber);
        Tile targetTile = pC.tileController.GetTargetTile(WaveNumber);


        GameObject enemyObj = Instantiate(pC.prefabController.EnemyNormalPrefab, new Vector3(spawnTile.X, spawnTile.Y, 0), Quaternion.identity, enemyAnchorObj.transform);
        _Enemy enemy = enemyObj.GetComponentInChildren<_Enemy>();

        EnemiesList.Add(enemy);
        enemyToGoMap[enemy] = enemyObj;

        enemy.Init(pC, spawnTile, targetTile);
    }
    private void UpdateEnemies()
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
    private void EnemyReachedTarget()
    {
        Debug.Log("Enemy reached target! ");
    }
    private void DestroyEnemies()
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
