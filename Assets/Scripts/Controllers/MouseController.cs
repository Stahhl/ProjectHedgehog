using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    //Fields
    private PlayerController pC;
    private Tile lastTileUnderMouse = null;
    private Tile _tileUnderMouse = null;

    [SerializeField]
    private LayerMask LayerTileID;

    //Properties
    public Tile TileUnderMouse
    {
        get { return _tileUnderMouse; }
        protected set
        {
            if (value == null)
                return;

            _tileUnderMouse = value;
        }
    }
    public void Init(PlayerController pC)
    {
        this.pC = pC;
    }

    // Update is called once per frame
    void Update()
    {
        if (pC == null)
            return;

        //TileUnderMouse = MouseToTile();

        //if (TileUnderMouse != lastTileUnderMouse)
        //{
        //    //gC.BuildingController.DisplayPreviewOnMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //    lastTileUnderMouse = TileUnderMouse;
        //    Debug.Log("New tile under mouse! ");
        //}
    }

    //Tile MouseToTile()
    //{
    //    Ray mouserRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitInfo;

    //    if (Physics.Raycast(mouserRay, out hitInfo, Mathf.Infinity, LayerTileID.value))
    //    {
    //        GameObject tileGo = hitInfo.rigidbody.gameObject;
    //        return tileGo != null ? gC.TileController.GetTileFromGo(tileGo) : null;
    //    }
    //    return null;
    //}
}
