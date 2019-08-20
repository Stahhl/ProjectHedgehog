using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    //Fields
    private PlayerController pC;

    public void Init(PlayerController pC)
    {
        this.pC = pC;
    }

    // Update is called once per frame
    void Update()
    {
        if (pC == null)
            return;

        UpdateGeneralButtons();
        UpdateGameplayButtons();
    }
    void UpdateGeneralButtons()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pC.EscapeFunction();
        }
    }
    void UpdateGameplayButtons()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            pC.enemyController.ForceWave();
        }
    }
}
