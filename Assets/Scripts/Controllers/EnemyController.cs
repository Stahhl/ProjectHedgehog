using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles spawning waves and keeping track of enemies
/// </summary>
public class EnemyController : MonoBehaviour
{
    //Fields
    private PlayerController pC;
    private float waveTime;
    private float timer;
    private bool started;

    [SerializeField]
    private GameObject normalEnemyPrefab;

    [SerializeField]
    private GameObject enemyAnchorObj;

    public void Init(PlayerController pC)
    {
        this.pC = pC;

        waveTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pC == null || started == false)
            return;

        timer += Time.deltaTime;

        if(timer >= waveTime)
        {
            timer = 0f;
            SendWave();
        }
    }
    public void ForceWave()
    {
        //if (started == false)
        //    started = true;

        SendWave();
    }
    void SendWave()
    {
        Debug.Log("SendWave");
        Node spawnNode = pC.arenaController.GetNodeAt(0, 8);
        Node targetNode = pC.arenaController.GetNodeAt(27, 8);


        Debug.Log("Spawn: " + spawnNode.X + " " + spawnNode.Y);
        Debug.Log("Target: " + targetNode.X + " " + targetNode.Y);

        GameObject enemyObj = Instantiate(normalEnemyPrefab, new Vector3(spawnNode.X, spawnNode.Y, 0), Quaternion.identity, enemyAnchorObj.transform);
        _Enemy enemy = enemyObj.GetComponentInChildren<_Enemy>();

        enemy.Init(pC, spawnNode, targetNode);
    }
}
