using Newtonsoft.Json;
using System.IO;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI.Table;

[System.Serializable]
public class Road
{
    public int[][] road;
}

public class GridReader : MonoBehaviour
{
    
    public string fileName = "/Script/RoadExtract/grid.json";

    protected int[][] gameGrid; // protected so child classes can access

    protected virtual void Awake()
    {
        LoadRoad();
    }

    /// <summary>
    /// Reads road JSON from file and saves it in roadGrid
    /// </summary>
    protected void LoadRoad()
    {
        string path = Application.dataPath + fileName;
        if (!File.Exists(path))
        {
            Debug.LogError($"Road JSON not found at: {path}");
            return;
        }

        string json = File.ReadAllText(path);
        RoadData data = JsonConvert.DeserializeObject<RoadData>(json);

        if (data != null)
        {
            gameGrid = data.roadGrid;
            Debug.Log($"Loaded roadGrid with {gameGrid.Length} rows.");
        }
        else
        {
            Debug.LogError("Failed to deserialize road JSON.");
        }
    }

    /// <summary>
    /// Safely get the value at (x, y) from the road grid.
    /// Returns -1 if out of bounds.
    /// </summary>
    protected int GetRoadAt(int x, int y)
    {
        // Flip upside down
        /*for (int i = 0; i < roadGrid.Length / 2; i++)
        {
            int[] temp = roadGrid[i];
            roadGrid[i] = roadGrid[roadGrid.Length - 1 - i];
            roadGrid[roadGrid.Length - 1 - i] = temp;
        }*/

        if (gameGrid == null) return -1;

        if (y < 0 || y >= gameGrid.Length) return -1;
        if (x < 0 || x >= gameGrid[y].Length) return -1;

        return gameGrid[y][x]; // correct indexing
    }
}