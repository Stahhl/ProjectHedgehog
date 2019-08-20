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
        if (started == false)
            started = true;

        SendWave();
    }
    void SendWave()
    {
        Debug.Log("SendWave");
        Node spawnNode = pC.arenaController.GetSpawnNode();

        Debug.Log(spawnNode.X + " " + spawnNode.Y);
    }
}
