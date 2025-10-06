using Newtonsoft.Json;
using System.IO;
using UnityEngine;


public static class GridDataHandler
{
    public static int[][] gameGrid;
    /// <summary>
    /// Reads road JSON from file and saves it in roadGrid
    /// </summary>
    public static int[][] LoadRoad()
    {
        string path = Application.dataPath + "/Script/RoadExtract/grid.json";
        if (!File.Exists(path))
        {
            Debug.LogError($"Road JSON not found at: {path}");
            return null;
        }

        string json = File.ReadAllText(path);
        RoadData data = JsonConvert.DeserializeObject<RoadData>(json);

        if (data != null)
        {
            gameGrid = data.roadGrid;
        }
        else
        {
            Debug.LogError("Failed to deserialize road JSON.");
            return null;
        }
        return gameGrid;
    }

    public static void GridUpdate(Vector2Int pos, int index)
    {
        gameGrid[pos.y][pos.x] = index; //y first
    }
}