using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ArenaController arenaController { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        this.arenaController = GetComponent<ArenaController>();

        arenaController.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
