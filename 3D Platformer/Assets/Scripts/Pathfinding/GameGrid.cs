using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*  This script has been adapted from the reference below:
 
    Lague, S (2015). A* Pathfinding (E02: node grid). Youtube. Available at:
    https://www.youtube.com/watch?v=nhiFx28e7JY&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=2 [Accessed 15 March 2019]
*/


public class GameGrid : MonoBehaviour
{

    Node[,] grid;
    float diameterOfNode;
    int gridSizeX, gridSizeY;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float radiusOfNode;
    public Vector3 offset;
    public TerrainType[] walkableAreas;
    LayerMask walkableMask;
    Dictionary<int, int> walkableAreasDictionary = new Dictionary<int, int>();

    void Awake()
    {
        diameterOfNode = radiusOfNode * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / diameterOfNode);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / diameterOfNode);

        foreach (TerrainType area in walkableAreas)
        {
            walkableMask.value += area.terrainMask.value;
            walkableAreasDictionary.Add((int) Mathf.Log(area.terrainMask.value, 2), area.terrainPenalty);
        }
        CreateGrid();
    }

    public int MaxGridSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * diameterOfNode + radiusOfNode) + Vector3.forward * (y * diameterOfNode + radiusOfNode);
                bool isWalkable = !(Physics.CheckSphere(worldPoint, radiusOfNode, unwalkableMask));
                int costOfMovement = 1;

                if (isWalkable)
                {
                    Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                    if (Physics.Raycast(ray, out RaycastHit hit, 100, walkableMask))
                    {
                        walkableAreasDictionary.TryGetValue(hit.collider.gameObject.layer, out costOfMovement);
                    }


                }
                grid[x, y] = new Node(isWalkable, worldPoint, x, y, costOfMovement);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int adjustX = node.gridX + x;
                int adjustY = node.gridY + y;

                if (adjustX >= 0 && adjustX < gridSizeX && adjustY >= 0 && adjustY < gridSizeY)
                {
                    neighbours.Add(grid[adjustX, adjustY]);
                }
            }
        }

        return neighbours;
    }


    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        worldPos += offset;
        float percentageX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentageY = (worldPos.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentageX = Mathf.Clamp01(percentageX);
        percentageY = Mathf.Clamp01(percentageY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentageX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentageY);
        return grid[x, y];
    }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }
}