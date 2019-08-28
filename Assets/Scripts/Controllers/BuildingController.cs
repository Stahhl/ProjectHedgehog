using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    //Fields
    private PlayerController pC;
    private bool buildingPlacement;
    private GameObject[] previewObj;
    private List<Tile> previewTiles;

    [SerializeField]
    private GameObject TowerAnchorObj;

    //Properties
    public bool BuildMode { get; private set; }

    public void Init(PlayerController pC)
    {
        this.pC = pC;
    }

    // Update is called once per frame
    void Update()
    {
        if (pC == null)
            return;

        if(
            BuildMode == true && 
            pC.mouseController.CurrentPoint != null &&
            pC.tileController.IsPathBlocked() == false)
        {
            PreviewOnMouse(pC.mouseController.CurrentPoint);
        }
    }
    public void BuildBuilding()
    {
        if(BuildMode == false || buildingPlacement == false)
        {
            return;
        }

        Debug.Log("BuildBuilding");
        PlaceTower(previewTiles, pC.prefabController.TowerBlockerPrefab);
    }
    private void PlaceTower(List<Tile> tiles, GameObject towerPrefab)
    {
        GameObject towerObj = Instantiate(
            towerPrefab, 
            new Vector3(tiles[0].X, tiles[0].Y, 0),
            Quaternion.identity, 
            TowerAnchorObj.transform
            );

        towerObj.GetComponentInChildren<_Tower>().Init(pC, tiles);
    }
    public void PreviewOnMouse(Vector2? point)
    {
        DestroyPreview();

        if (BuildMode == false || point == null)
        {
            //Debug.Log("DisplayPreview - return");
            //BuildMode = false;
            buildingPlacement = false;

            return;
        }

        //Debug.Log("DisplayPreview");

        previewObj = new GameObject[4];
        previewTiles = pC.tileController.GetTilesAroundPoint((Vector2)point);
        buildingPlacement = true;

        foreach (Tile t in previewTiles)
        {
            if(t == null || t.MyTileType != TileType.OPEN)
            {
                buildingPlacement = false;
            }
        }

        DrawPreview(previewTiles, buildingPlacement);
    }
    public void ToggleBuildMode()
    {
        BuildMode = !BuildMode;
        DestroyPreview();
        Debug.Log("ToggleBuildMode: " + BuildMode);
    }
    private void DrawPreview(List<Tile> tiles, bool placement)
    {
        //Debug.Log(previewObj.Length);
        Color color = placement == true ? Color.green : Color.red;

        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i] == null)
                continue;

            previewObj[i] = Instantiate(
                pC.prefabController.PreviewPrefab, 
                tiles[i].GetTilePosition(), 
                Quaternion.identity, 
                TowerAnchorObj.transform
                );

            previewObj[i].GetComponentInChildren<SpriteRenderer>().color = color;
        }
    }
    private void DestroyPreview()
    {
        if (previewObj == null)
            return;

        //Debug.Log("DestroyPreview");

        foreach (var gO in previewObj)
        {
            Destroy(gO);
        }

        previewObj = null;
        previewTiles = null;
    }
}
