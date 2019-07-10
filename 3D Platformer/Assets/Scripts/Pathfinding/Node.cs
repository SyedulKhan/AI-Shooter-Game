using UnityEngine;
using System.Collections;

/*  This script has been adapted from the reference below:
 
    Lague, S (2015). A* Pathfinding (E02: node grid). Youtube. Available at:
    https://www.youtube.com/watch?v=nhiFx28e7JY&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=2 [Accessed 15 March 2019]
*/

public class Node : IHeapItem<Node>
{

    public bool walkable;
    public Vector3 worldPos;
    public int gridX, gridY, penalty;
    public int gCost, hCost;
    public Node parent;
    int heapIndex;

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY, int penalty)
    {
        this.walkable = walkable;
        this.worldPos = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
        this.penalty = penalty;
    }

    public int FCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node node)
    {
        int compareCost = FCost.CompareTo(node.FCost);
        if (compareCost == 0)
        {
            compareCost = hCost.CompareTo(node.hCost);
        }
        return -compareCost;
    }
}