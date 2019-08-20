using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Normal_ : _Enemy
{
    //Child values assigned to parent properties in init
    private float speedModifer = 6f;
    private bool ignoreTerrain = false;

    public override void Init(PlayerController pC, Node startNode, Node targetNode)
    {
        base.Init(pC, startNode, targetNode);

        //base
        base.SpeedModifier = speedModifer;
        base.IgnoreTerrain = ignoreTerrain;
        base.MyPath = QPath.QPath.FindPath<Node>(pC, this, StartNode, TargetNode, Node.CostEstimate).ToList();
        Debug.Log("start: " + StartNode.X + "_" + TargetNode.Y);
        Debug.Log("target: " + TargetNode.X + "_" + TargetNode.Y);
        Debug.Log("path: " + MyPath.Count);
    }
    protected override void Update()
    {
        base.Update();
    }
}
