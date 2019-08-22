using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    //Fields
    private PlayerController pC;

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
}
