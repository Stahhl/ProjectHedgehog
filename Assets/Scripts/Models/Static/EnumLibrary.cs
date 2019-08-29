using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumLibrary
{
    public enum WaveType
    {
        NULL,
        NORMAL
    }
    public enum TileType
    {
        NULL,
        OPEN,
        OCCUPIED,
        ENEMY,
        ENEMYSPAWN,
        ENEMYTARGET,
        FRIENDLY
    }
}
