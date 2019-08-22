using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;

public class PlayerController : MonoBehaviour, IQPathWorld
{
    //Properties
    public TileController arenaController { get; private set; }
    public EnemyController enemyController { get; private set; }
    public KeyboardController keyboardController { get; private set; }
    public MouseController mouseController { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.arenaController = GetComponent<TileController>();
        this.enemyController = GetComponent<EnemyController>();
        this.keyboardController = GetComponent<KeyboardController>();
        this.mouseController = GetComponent<MouseController>();

        arenaController.Init(this);
        enemyController.Init(this);
        keyboardController.Init(this);
        mouseController.Init(this);
    }

    public void EscapeFunction()
    {
        Debug.Log("EscapeFunction");
    }
}
