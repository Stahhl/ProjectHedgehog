using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UiController : MonoBehaviour
{
    //Fields
    private PlayerController pC;

    //Stat panel
    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField]
    private TextMeshProUGUI waveText;
    [SerializeField]
    private TextMeshProUGUI enemiesKilledText;

    //Building panel
    [SerializeField]
    private TextMeshProUGUI goldText;

    //Old values
    private int oldLivesText = -1;
    private int oldWaveText = -1;
    private int oldEnemiesKilledText = -1;
    private int oldGoldText = -1;

    public void Init(PlayerController pC)
    {
        this.pC = pC;
    }
    private void Update()
    {
        if (pC == null)
            return;

        //Stat panel
        UpdatePlayerLivesText(pC.combatController.PlayerLives);
        UpdateWaveNumberText(pC.enemyController.WaveNumber);
        UpdateEnemiesKilledText(pC.enemyController.EnemiesKilled);

        //Building panel
        UpdateGoldText(pC.buildingController.Gold);
    }
    #region StatPanel
    public void UpdatePlayerLivesText(int value)
    {
        if(oldLivesText != value)
        {
            livesText.text = $"Lives: {value}";
            oldLivesText = value;
        }
    }
    public void UpdateWaveNumberText(int value)
    {
        if(oldWaveText != value)
        {
            waveText.text = $"Wave: {value}";
            oldWaveText = value;
        }
    }
    public void UpdateEnemiesKilledText(int value)
    {
        if(oldEnemiesKilledText != value)
        {
            enemiesKilledText.text = $"Kills: {value}";
            oldEnemiesKilledText = value;
        }
    }
    #endregion
    #region BuildingPanel
    private void UpdateGoldText(int value)
    {
        if (oldGoldText != value)
        {
            goldText.text = $"Gold: {value}";
            oldGoldText = value;
        }
    }
    #endregion
}
