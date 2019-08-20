using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //Properties
    public GameObject spriteObj;
    public float X { get; private set; }
    public float Y { get; private set; }

    //Fields
    private PlayerController pC;

    public void Init(PlayerController pC)
    {
        this.pC = pC;

        this.X = this.transform.position.x;
        this.Y = this.transform.position.y;
    }
}
