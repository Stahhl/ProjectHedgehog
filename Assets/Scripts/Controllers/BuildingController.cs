using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    //Fields
    private PlayerController pC;
    private Vector3 point;

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


    }
    public void DisplayPreview(Tile tile)
    {
        if (BuildMode == false || tile == null)
        {
            //todo if preview is not null delete it
            return;
        }

        //Debug.Log("DisplayPreview");
        point = new Vector2(tile.X, tile.Y);

    }
    public void ToggleBuildMode()
    {
        BuildMode = !BuildMode;
        Debug.Log("ToggleBuildMode: " + BuildMode);
    }
}
