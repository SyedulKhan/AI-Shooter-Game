using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*  This script has been adapted from the reference below:
 
    Lague, S (2015). A* Pathfinding (E03: algorithm implementation). Youtube. Available at:
    https://www.youtube.com/watch?v=mZfyt03LDH4&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=3 [Accessed 16 March 2019]
*/

public class Pathfinder : MonoBehaviour
{

    PathManager pathManager;
    GameGrid grid;

    void Awake()
    {
        pathManager = GetComponent<PathManager>();
        grid = GetComponent<GameGrid>();
    }

    public void BeginFindingPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] pointsInPath = new Vector3[0];
        bool pathFound = false;

        Node start = grid.NodeFromWorldPoint(startPos);
        Node target = grid.NodeFromWorldPoint(targetPos);

        if (start.walkable && target.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxGridSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == target)
                {
                    pathFound = true;
                    break;
                }

                foreach (Node neighbourNode in grid.GetNeighbours(currentNode))
                {
                    if (!neighbourNode.walkable || closedSet.Contains(neighbourNode))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbourNode) + neighbourNode.penalty;
                    if (newMovementCostToNeighbour < neighbourNode.gCost || !openSet.Contains(neighbourNode))
                    {
                        neighbourNode.gCost = newMovementCostToNeighbour;
                        neighbourNode.hCost = GetDistance(neighbourNode, target);
                        neighbourNode.parent = currentNode;

                        if (!openSet.Contains(neighbourNode))
                            openSet.Add(neighbourNode);
                        else
                        {
                            openSet.UpdateItem(neighbourNode);
                        }
                    }
                }
            }
        }
        yield return null;

        if (pathFound)
        {
            pointsInPath = RetracePath(start, target);
        }
        pathManager.FinishedProcessingPath(pointsInPath, pathFound);
        
    }

    Vector3[] RetracePath(Node start, Node destination)
    {
        List<Node> path = new List<Node>();
        Node currentNode = destination;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] pointsInPath = SimplifyPath(path);
        Array.Reverse(pointsInPath);
        return pointsInPath;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> pointsInPath = new List<Vector3>();
        Vector2 oldDir = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 newDir = new Vector2(path[i-1].gridX - path[i].gridX, path[i-1].gridY - path[i].gridY);
            if (newDir != oldDir)
            {
                pointsInPath.Add(path[i-1].worldPos);
            }
            oldDir = newDir;
        }
        return pointsInPath.ToArray();
    }

    int GetDistance(Node A, Node B)
    {
        int distanceX = Mathf.Abs(A.gridX - B.gridX);
        int distanceY = Mathf.Abs(A.gridY - B.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }


}