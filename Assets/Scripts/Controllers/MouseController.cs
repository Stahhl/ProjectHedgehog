using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    //Properties
    public Tile TileUnderMouse { get; private set; }
    public Vector2? CurrentPoint { get; private set; }

    //Fields
    private PlayerController pC;
    private Tile lastTileUnderMouse = null;
    private Vector3 mousePos;
    //private Vector2? currentPoint = null;
    private Vector2? lastPoint = null;

    [SerializeField]
    private LayerMask LayerTileID;

    public void Init(PlayerController pC)
    {
        this.pC = pC;
    }

    // Update is called once per frame
    private void Update()
    {
        if (pC == null)
            return;

        TileUnderMouse = MouseToTile();
        mousePos = pC.mainCamera.ScreenToWorldPoint(Input.mousePosition);

        UpdateTileAndPoint();
        UpdateMouseButtons();
    }
    private void UpdateMouseButtons()
    {
        if(Input.GetMouseButtonDown(0) == true)
        {
            pC.buildingController.BuildBuilding();
        }
    }
    private void UpdateTileAndPoint()
    {
        if (TileUnderMouse != null)
        {
            float X = Mathf.Round(mousePos.x * 2) / 2;
            float Y = Mathf.Round(mousePos.y * 2) / 2;

            CurrentPoint = new Vector2(X, Y);

            if (CurrentPoint != lastPoint)
            {
                //Debug.Log("currentPoint = " + X + "_" + Y);
                //pC.buildingController.PreviewOnMouse(currentPoint);
                lastPoint = CurrentPoint;
            }
        }
        if (TileUnderMouse != lastTileUnderMouse)
        {
            //Debug.Log("new TileUnderMouse");
            if (TileUnderMouse == null)
            {
                CurrentPoint = null;
                lastPoint = null;
                //pC.buildingController.PreviewOnMouse(currentPoint);
            }

            lastTileUnderMouse = TileUnderMouse;
        }
    }
    private Vector2 MouseToPoint()
    {
        Vector3 pointer = pC.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(pointer.x, pointer.y);
    }
    private Tile MouseToTile()
    {
        Ray mouserRay = pC.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(mouserRay, out hitInfo, Mathf.Infinity, LayerTileID.value))
        {
            //Debug.Log("Hit!");

            GameObject tileGo = hitInfo.rigidbody.gameObject;
            Tile tile = tileGo.GetComponent<Tile>();

            return tile != null ? tile : null;
        }
        //Debug.Log("Miss!");
        return null;
    }
}
