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

    //Properties
    private List<List<Node>> listOfLists;
    public List<Node> spawnNodes1;
    private List<Node> spawnNodes2;
    public List<Node> targetNodes1;
    private List<Node> targetNodes2;

    public void Init(PlayerController pC)
    {
        this.pC = pC;

        nodeObjArray = new GameObject[arenaWidth, arenaHeight];
        nodeArray = new Node[arenaWidth, arenaHeight];


        spawnNodes1 = new List<Node>();
        //spawnNodes2 = new List<Node>();
        targetNodes1 = new List<Node>();
        //targetNodes2 = new List<Node>();

        listOfLists = new List<List<Node>>();
        listOfLists.Add(spawnNodes1);
        //listOfLists.Add(spawnNodes2);
        listOfLists.Add(targetNodes1);
        //listOfLists.Add(targetNodes2);

        refObj.SetActive(false);

        SetupMainNodes();
        SetupSpecialNodes();
        CheckNeighbours();
    }

    private void CheckNeighbours()
    {
        var nodes = GameObject.FindGameObjectsWithTag("Node");

        foreach (var n in nodes)
        {
            var node = n.GetComponent<Node>();

            if(node.GetNeighbours().Length <= 0)
            {
                Debug.LogError(node.myName);
            }
        }
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

            spawnNodes1.Add(node);

            node.Init(pC);
        }
        for (int i = 0; i < tN1.transform.childCount; i++)
        {
            Node node = tN1.transform.GetChild(i).GetComponent<Node>();

            targetNodes1.Add(node);

            node.Init(pC);
        }

        //for (int i = 0; i < spawnNodes1.Count; i++)
        //{
        //    spawnNodes1[i].SpecialNode(spawnNodes1, i);
        //}
        //for (int i = 0; i < targetNodes1.Count; i++)
        //{
        //    targetNodes1[i].SpecialNode(targetNodes1, i);
        //}
    }
    private void SetupMainNodes()
    {
        for (int X = 0; X < arenaWidth; X++)
        {
            for (int Y = 0; Y < arenaHeight; Y++)
            {
                nodeObjArray[X, Y] = Instantiate(nodePrefab, new Vector3(X, Y, 0), Quaternion.identity, nodeAnchorObj.transform);
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
    public Node GetNodeAt(float X, float Y)
    {
        try
        {
            int intX = Mathf.FloorToInt(X);
            int intY = Mathf.FloorToInt(Y);

            return nodeArray[intX, intY];
        }
        catch
        {
            return GetSpecialNode(X, Y);
        }
    }
    private Node GetSpecialNode(float X, float Y)
    {
        foreach (var list in listOfLists)
        {
            if(list[0].X == X || list[0].Y == Y)
            {
                foreach (var node in list)
                {
                    if (node.X == X && node.Y == Y)
                    {
                        Debug.Log("GetSpecialNode");
                        return node;
                    }
                }
            }
        }

        return null;
    }
}
