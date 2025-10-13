using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;
using System.IO;


public class RoadData
{
    public int[][] roadGrid;
}

public class GridFileGenerateor : MonoBehaviour
{
    private Tilemap tilemap;
    public RuleTile roadTile;

    private string SavePath => Application.dataPath + "/Script/RoadExtract/grid.json";

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("No Tilemap found on this GameObject!");
        }
        SaveRoads();
    }

    // ---------------- Save ----------------
    private void SaveRoads()
    {
        BoundsInt bounds = tilemap.cellBounds;
        int rows = bounds.size.y;
        int cols = bounds.size.x;

        int[][] grid = new int[rows][];

        for (int y = 0; y < rows; y++)
        {
            grid[y] = new int[cols];
            for (int x = 0; x < cols; x++)
            {
                Vector3Int cellPos = new Vector3Int(bounds.x + x, bounds.y + y, 0);
                TileBase tile = tilemap.GetTile(cellPos);
                grid[y][x] = (tile == roadTile) ? 1 : 0;
            }
        }

        int[][] bigGrid = EmbedInCenter(grid, 10, 10);

        RoadData data = new RoadData { roadGrid = bigGrid };
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(SavePath, json);

        Debug.Log("Roads saved to: " + SavePath);
    }

    private int[][] EmbedInCenter(int[][] smallGrid, int bigRows, int bigCols)
    {
        // Initialize big grid with zeros
        int[][] bigGrid = new int[bigRows][];
        for (int i = 0; i < bigRows; i++)
        {
            bigGrid[i] = new int[bigCols];
        }

        // Calculate offsets to center smallGrid
        int offsetRow = (bigRows - smallGrid.Length) / 2;
        int offsetCol = (bigCols - smallGrid[0].Length) / 2;

        // Copy smallGrid into bigGrid
        for (int i = 0; i < smallGrid.Length; i++)
        {
            for (int j = 0; j < smallGrid[i].Length; j++)
            {
                bigGrid[i + offsetRow][j + offsetCol] = smallGrid[i][j];
            }
        }

        return bigGrid;
    }

}