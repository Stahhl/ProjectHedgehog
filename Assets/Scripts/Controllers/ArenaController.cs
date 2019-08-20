using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the visuals of the arena and the node system.
/// </summary>
public class ArenaController : MonoBehaviour
{
    //Fields
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

    private List<Node> spawnNodes1;
    private List<Node> spawnNodes2;
    private List<Node> targetNodes1;
    private List<Node> targetNodes2;

    public void Init(PlayerController pC)
    {
        this.pC = pC;

        nodeObjArray = new GameObject[arenaWidth, arenaHeight];
        nodeArray = new Node[arenaWidth, arenaHeight];

        spawnNodes1 = new List<Node>();
        spawnNodes2 = new List<Node>();
        targetNodes1 = new List<Node>();
        targetNodes2 = new List<Node>();

        refObj.SetActive(false);

        SetupSpecialNodes();
        SetupMainNodes();
    }

    // Update is called once per frame
    private void Update()
    {
        if (pC == null)
            return;
    }
    private void SetupSpecialNodes()
    {
        GameObject sN1 = GameObject.FindGameObjectWithTag("SpawnNodes1");
        GameObject tN1 = GameObject.FindGameObjectWithTag("TargetNodes1");


        for (int i = 0; i < sN1.transform.childCount; i++)
        {
            Node node = sN1.transform.GetChild(i).GetComponent<Node>();

            node.Init(pC);

            spawnNodes1.Add(node);
        }
        for (int i = 0; i < tN1.transform.childCount; i++)
        {
            Node node = tN1.transform.GetChild(i).GetComponent<Node>();

            node.Init(pC);

            targetNodes1.Add(node);
        }
    }
    private void SetupMainNodes()
    {
        for (int X = 0; X < arenaWidth; X++)
        {
            for (int Y = 0; Y < arenaHeight; Y++)
            {
                nodeObjArray[X, Y] = Instantiate(nodePrefab, new Vector3(X + 0.5f, Y + 0.5f, 0), Quaternion.identity, nodeAnchorObj.transform);
                nodeArray[X, Y] = nodeObjArray[X, Y].GetComponent<Node>();

                nodeArray[X, Y].Init(pC);

                //? Comment out this to show nodes visually 
                //nodeArray[X, Y].spriteObj.SetActive(false);
            }
        }
    }
    public Node GetSpawnNode()
    {
        try
        {
            int rnd = UnityEngine.Random.Range(0, spawnNodes1.Count);
            return spawnNodes1[rnd];
        }
        catch
        {
            Debug.LogError("GetSpawnNode");
            return null;
        }
    }
    public Node GetTargetNode()
    {
        try
        {
            int rnd = UnityEngine.Random.Range(0, targetNodes1.Count);
            return targetNodes1[rnd];
        }
        catch
        {
            Debug.LogError("GetTargetNode");
            return null;
        }
    }
    public Node GetTileAt(int X, int Y)
    {
        try
        {
            return nodeArray[X, Y];
        }
        catch
        {
            Debug.LogError("GetTileAt");
            return null;
        }
    }
}
