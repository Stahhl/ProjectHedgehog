﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace QPath
{
    public class QPath_AStar<T> where T : IQPathTile {

        public QPath_AStar( 
            IQPathWorld world, 
            IQPathUnit unit, 
            T startTile, 
            T endTile
        )
        {
           // Do Setup

            //this.world = world;
            this.unit = unit;
            this.startTile = startTile;
            this.endTile = endTile;
            //this.costEstimateFunc = costEstimateFunc;

            // Do we need to explicitly create a graph?
        }

        //IQPathWorld world;
        IQPathUnit unit;
        T startTile;
        T endTile;
        //CostEstimateDelegate costEstimateFunc;

        Queue<T> path;
        Dictionary<IQPathTile, IQPathTile> pathList;
        IQPathTile[] twins;

        public void DoWork()
        {
            //Debug.Log("QPath_AStar::DoWork");
            path = new Queue<T>();
            pathList = new Dictionary<IQPathTile, IQPathTile>();
            //twins = new Queue<IQPathTile>();

            HashSet< T > closedSet = new HashSet<T>();

            PathfindingPriorityQueue< T > openSet = new PathfindingPriorityQueue<T>();
            openSet.Enqueue(startTile, 0);

            Dictionary<T, T> came_From = new Dictionary<T, T>();

            Dictionary<T, float> g_score = new Dictionary<T, float>();
            g_score[startTile] = 0;

            Dictionary<T, float> f_score = new Dictionary<T, float>();
            f_score[startTile] = CostEstimate(startTile, endTile);

            while (openSet.Count > 0)
            {
                T current = openSet.Dequeue();

                // Check to see if we are there.
                if ( System.Object.ReferenceEquals( current, endTile ) )
                {
                    Reconstruct_path(came_From, current);
                    return;
                }

                closedSet.Add(current);

                foreach (T edge_neighbour in current.GetNeighbours())
                {
                    T neighbour = edge_neighbour;

                    if (closedSet.Contains(neighbour))
                    {
                        continue; // ignore this already completed neighbor
                    }


                    float total_pathfinding_cost_to_neighbor = 
                        neighbour.AggregateCostToEnter( g_score[current], current, LastTwoTiles(null), unit );

                    if(total_pathfinding_cost_to_neighbor < 0)
                    {
                        // Values less than zero represent an invalid/impassable tile
                        continue;
                    }

                    float tentative_g_score = total_pathfinding_cost_to_neighbor;

                    // Is the neighbour already in the open set?
                    //   If so, and if this new score is worse than the old score,
                    //   discard this new result.
                    if (openSet.Contains(neighbour) && tentative_g_score >= g_score[neighbour])
                    {
                        continue;
                    }

                    // This is either a new tile or we just found a cheaper route to it
                    came_From[neighbour] = current;
                    //pathList[neighbour] = current;
                    g_score[neighbour] = tentative_g_score;
                    f_score[neighbour] = g_score[neighbour] + CostEstimate(neighbour, endTile);

                    LastTwoTiles(neighbour);

                    openSet.EnqueueOrUpdate(neighbour, f_score[neighbour]);
                } // foreach neighbour
            } // while
        }
        public static float CostEstimate(IQPathTile aa, IQPathTile bb)
        {
            return Distance((Tile)aa, (Tile)bb);
        }
        public static float Distance(Tile a, Tile b)
        {
            float dC = Mathf.Abs(a.X - b.X); //Column
            float dR = Mathf.Abs(a.Y - b.Y); //Row

            return Mathf.Max(dC, dR);
        }
        //Keep track of the two latest tiles to calc direction
        private IQPathTile[] LastTwoTiles(IQPathTile t)
        {
            if(twins == null)
                twins = new IQPathTile[2];

            if (t == null)
                return twins;

            if (twins.Length == 0)
            {
                twins[0] = t;
            }
            if (twins.Length == 1)
            {
                twins[1] = t;
            }
            if(twins.Length == 2)
            {
                twins[0] = twins[1];
                twins[1] = t;
            }
            if(twins.Length > 2)
            {
                Debug.LogError("twins.Length > 2");
            }

            return twins;
        }
        private void Reconstruct_path(
            Dictionary<T, T> came_From,
            T current)
        {
            // So at this point, current IS the goal.
            // So what we want to do is walk backwards through the Came_From
            // map, until we reach the "end" of that map...which will be
            // our starting node!
            Queue<T> total_path = new Queue<T>();
            total_path.Enqueue(current); // This "final" step is the path is the goal!

            while (came_From.ContainsKey(current))
            {
                /*    Came_From is a map, where the
            *    key => value relation is real saying
            *    some_node => we_got_there_from_this_node
            */

                current = came_From[current];
                total_path.Enqueue(current);
            }

            // At this point, total_path is a queue that is running
            // backwards from the END tile to the START tile, so let's reverse it.
            path = new Queue<T>(total_path.Reverse());
        }


        public T[] GetList()
        {
            return path.ToArray();
        }

    }
}