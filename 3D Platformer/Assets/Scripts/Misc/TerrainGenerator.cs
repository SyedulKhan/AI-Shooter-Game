
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width;
    public int height;
    public int depth;

    public float scale;

    public float offsetX;
    public float offsetY;

    private void Start()
    {
        //offsetX = Random.Range(0f, 9999f);
        //offsetY = Random.Range(0f, 9999f);
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;

    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                heights[i, j] = CalculateHeight(i, j);
            }
        }
        return heights;
    }

    float CalculateHeight(int i, int j)
    {
        float x;
        float y;

        if (i > 0 && j > 0 && i < 100 && j < 210)
        {
            x = (float)i / width + offsetX;
            y = (float)j / height + offsetY;
        }
        else
        {
            x = (float)i / width * scale + offsetX;
            y = (float)j / height * scale + offsetY;
        }
        //x = (float)i / width * scale + offsetX;
        //y = (float)j / height * scale + offsetY;

        return Mathf.PerlinNoise(x, y);
    }
}
