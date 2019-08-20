using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;

public class PlayerController : MonoBehaviour, IQPathWorld
{
    //Properties
    public ArenaController arenaController { get; private set; }
    public EnemyController enemyController { get; private set; }
    public KeyboardController keyboardController { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.arenaController = GetComponent<ArenaController>();
        this.enemyController = GetComponent<EnemyController>();
        this.keyboardController = GetComponent<KeyboardController>();

        arenaController.Init(this);
        enemyController.Init(this);
        keyboardController.Init(this);
    }

    public void EscapeFunction()
    {
        Debug.Log("EscapeFunction");
    }
}
