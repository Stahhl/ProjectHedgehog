using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Enemy : MonoBehaviour
{
    //Properties
    public Node StartNode { get; protected set; }
    public Node TargetNode { get; protected set; }
    public Node CurrentNode { get; protected set; }
    public float SpeedModifier { get; protected set; }
    public bool IgnoreTerrain { get; protected set; }
    public bool IsDestroyed { get; protected set; }
    public bool ReachedTarget { get; protected set; }
    public List<Node> MyPath { get; protected set; }

    //Fields
    protected PlayerController pC;

    public virtual void Init(PlayerController pC, Node startNode, Node targetNode)
    {
        this.pC = pC;
        this.StartNode = startNode;
        this.TargetNode = targetNode;

        this.CurrentNode = this.StartNode;
    }

    protected virtual void Update()
    {
        if (pC == null && IsDestroyed == true)
            return;

        //UpdateMovement();
        //CheckIfReachedGoal();
    }
}
