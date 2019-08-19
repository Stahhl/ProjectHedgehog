using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the visuals of the arena and the node system.
/// </summary>
public class ArenaController : MonoBehaviour
{
    //Field
    [SerializeField]
    private GameObject nodeAnchorObj;
    [SerializeField]
    private GameObject refObj;
    [SerializeField]
    private GameObject nodePrefab;

    private PlayerController pC;
    private int arenaWidth = 28;
    private int arenaHeight = 22;
    private GameObject[,] nodeObjArray;
    private Node[,] nodeArray;

    // Start is called before the first frame update
    public void Init(PlayerController pC)
    {
        this.pC = pC;

        nodeObjArray = new GameObject[arenaWidth, arenaHeight];
        nodeArray = new Node[arenaWidth, arenaHeight];

        refObj.SetActive(false);

        SetupArena();
    }

    // Update is called once per frame
    private void Update()
    {
        if (pC == null)
            return;
    }
    private void SetupArena()
    {
        for (int X = 0; X < arenaWidth; X++)
        {
            for (int Y = 0; Y < arenaHeight; Y++)
            {
                nodeObjArray[X, Y] = Instantiate(nodePrefab, new Vector3(X, Y, 0), Quaternion.identity, nodeAnchorObj.transform);
                nodeArray[X, Y] = nodeObjArray[X, Y].GetComponent<Node>();

                //? Comment out this to show nodes visually 
                nodeArray[X, Y].spriteObj.SetActive(false);
            }
        }
    }
}
