using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QPath
{

    public interface IQPathTile
    {
        IQPathTile[] GetNeighbours();
        Vector3 GetTilePosition();

        float AggregateCostToEnter( float costSoFar, IQPathTile sourceTile, IQPathTile[] twins, IQPathUnit theUnit );

    }

}