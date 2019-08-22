using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;

public class PlayerController : MonoBehaviour, IQPathWorld
{
    //Fields
    public GameObject MainCameraObj;

    [HideInInspector]
    public Camera mainCamera;

    //Properties
    public TileController arenaController { get; private set; }
    public EnemyController enemyController { get; private set; }
    public KeyboardController keyboardController { get; private set; }
    public MouseController mouseController { get; private set; }
    public BuildingController buildingController { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = MainCameraObj.GetComponentInChildren<Camera>();

        this.arenaController = GetComponent<TileController>();
        this.enemyController = GetComponent<EnemyController>();
        this.keyboardController = GetComponent<KeyboardController>();
        this.mouseController = GetComponent<MouseController>();
        this.buildingController = GetComponent<BuildingController>();

        arenaController.Init(this);
        enemyController.Init(this);
        keyboardController.Init(this);
        mouseController.Init(this);
        buildingController.Init(this);
    }

    public void EscapeFunction()
    {
        Debug.Log("EscapeFunction");
    }
}
