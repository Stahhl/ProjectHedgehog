using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPath;

public class PlayerController : MonoBehaviour, IQPathWorld
{
    //Fields
    [SerializeField]
    private GameObject MainCameraObj;
    [SerializeField]
    private GameObject PrefabControllerObj;
    [SerializeField]
    private GameObject UiControllerObj;

    //Anchors
    public GameObject TowerAnchorObj;
    public GameObject EnemyAnchorObj;
    public GameObject ProjectileAnchorObj;

    //Properties
    public TileController tileController { get; private set; }
    public EnemyController enemyController { get; private set; }
    public KeyboardController keyboardController { get; private set; }
    public MouseController mouseController { get; private set; }
    public BuildingController buildingController { get; private set; }
    public PrefabController prefabController { get; private set; }
    public CombatController combatController { get; private set; }
    public UiController uiController { get; private set; }

    public Camera mainCamera { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = MainCameraObj.GetComponentInChildren<Camera>();

        this.tileController = GetComponent<TileController>();
        this.enemyController = GetComponent<EnemyController>();
        this.keyboardController = GetComponent<KeyboardController>();
        this.mouseController = GetComponent<MouseController>();
        this.buildingController = GetComponent<BuildingController>();
        this.combatController = GetComponent<CombatController>();

        this.prefabController = PrefabControllerObj.GetComponent<PrefabController>();
        this.uiController = UiControllerObj.GetComponent<UiController>();

        tileController.Init(this);
        enemyController.Init(this);
        keyboardController.Init(this);
        mouseController.Init(this);
        buildingController.Init(this);
        combatController.Init(this);
        uiController.Init(this);
    }

    public void EscapeFunction()
    {
        Debug.Log("EscapeFunction");
    }
}
