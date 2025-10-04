using UnityEngine;

public class RoadAnalyzer : GridReader
{
    protected override void Awake()
    {
        base.Awake(); // loads the road

        if (gameGrid != null)
        {
            int roadCount = 0;
            foreach (var row in gameGrid)
                foreach (var cell in row)
                    if (cell == 1) roadCount++;

            Debug.Log($"There are {roadCount} road tiles in this map.");
        }
    }
}
