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

        TileUnderMouse = MouseToTile();

        if (TileUnderMouse != lastTileUnderMouse)
        {
            //Debug.Log("new TileUnderMouse");

            pC.buildingController.DisplayPreview(TileUnderMouse);
            lastTileUnderMouse = TileUnderMouse;
        }
    }
    Tile MouseToTile()
    {
        Ray mouserRay = pC.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(mouserRay, out hitInfo, Mathf.Infinity, LayerTileID.value))
        {
            GameObject tileGo = hitInfo.rigidbody.gameObject;
            Tile tile = tileGo.GetComponent<Tile>();

            return tile != null ? tile : null;
        }
        return null;
    }
}
