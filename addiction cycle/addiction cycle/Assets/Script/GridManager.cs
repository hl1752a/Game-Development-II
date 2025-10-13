using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    protected int[][] testDirections = new int[][]
    {
        new int[] {-1, 0}, // left (x-1, y)
        new int[] {1, 0},  // right (x+1, y)
        new int[] {0, -1}, // down (x, y-1)
        new int[] {0, 1}   // up (x, y+1)
    };

    [SerializeField] private Tilemap buildingTilemap;   // buildings
    [SerializeField] protected Tilemap roadTilemap;       // road
    [SerializeField] private Tilemap previewTilemap;    // ghost preview

    [SerializeField] private TileBase[] buildingTile;
    [SerializeField] private TileBase previewTile;     // Semi-transparent version

    protected Vector3Int currentMouseWorldCellPos = new Vector3Int(0, 0, 0);
    private Vector3 mouseWorldPos = new Vector3(0, 0, 0);
    private bool isEmptyCell = true;
    private bool isNextRoad = false;
    private bool isBuilding = false;

    private int selectedIndex = 2;

    private int clickCount = 0;
    private float timer = 0f;

    void Awake()
    {
        GridDataHandler.LoadRoad();
    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMouseWorldCellPos = buildingTilemap.WorldToCell(mouseWorldPos);

        previewTilemap.ClearAllTiles(); //clear preview

        isNextRoad = IsNextToRoad();
        isEmptyCell = IsEmptyCell();
        isBuilding = IsBuilding();

        if (isEmptyCell && isNextRoad)
        {
            //preview
            previewTilemap.SetTile(currentMouseWorldCellPos, previewTile);

            if (Input.GetMouseButtonDown(0))
            {
                PlaceTile(selectedIndex);
            }
        }
        else if(IsBuilding())
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (clickCount == 0)
                {
                    timer = 1f;
                }

                clickCount++;

                // Check if success
                if (clickCount >= DataManager.Data.Level/2)
                {
                    RemoveTile();
                    clickCount = 0;
                    timer = 0f;
                }  
            }
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            // If time ran out, reset
            if (timer <= 0)
            {
                clickCount = 0;
            }
        }
    }
    /// <summary>
    /// Safely get the value at (x, y) from the road grid.
    /// Returns -1 if out of bounds.
    /// </summary>
    protected int GetGridAt(int x, int y)
    {
        // Flip upside down
        /*for (int i = 0; i < roadGrid.Length / 2; i++)
        {
            int[] temp = roadGrid[i];
            roadGrid[i] = roadGrid[roadGrid.Length - 1 - i];
            roadGrid[roadGrid.Length - 1 - i] = temp;
        }*/

        if (GridDataHandler.gameGrid == null) return -1;

        if (y < 0 || y >= GridDataHandler.gameGrid.Length) return -1;
        if (x < 0 || x >= GridDataHandler.gameGrid[y].Length) return -1;

        return GridDataHandler.gameGrid[y][x]; // correct indexing
    }

    protected Vector2Int WorldCellToFileCell(Vector3Int pos)
    {
        return new Vector2Int(pos.x+5, pos.y+5);
    }

    private void PlaceTile(int index)
    {
        if (index < 0) return;
        buildingTilemap.SetTile(currentMouseWorldCellPos, buildingTile[index]);
        GridDataHandler.GridUpdate(WorldCellToFileCell(currentMouseWorldCellPos), index);
    }

    private void RemoveTile()
    {
        buildingTilemap.SetTile(currentMouseWorldCellPos, null);
        GridDataHandler.GridUpdate(WorldCellToFileCell(currentMouseWorldCellPos), 0);
    }

    private bool IsEmptyCell()
    {
        if (buildingTilemap.HasTile(currentMouseWorldCellPos) || roadTilemap.HasTile(currentMouseWorldCellPos))
        {
            return false;
        }
        else
        {
            return true;
        }           
    }

    private bool IsNextToRoad() {

        Vector2Int pos = WorldCellToFileCell(currentMouseWorldCellPos);

        for (int i = 0; i < testDirections.Length; i++)
        {
            int x = testDirections[i][0];
            int y = testDirections[i][1];
            Vector2Int testPos = new Vector2Int(pos.x + x, pos.y + y);

            if (GetGridAt(testPos.x, testPos.y) == 1)
            {
                return true;
            }
        }

        
        return false;
    }

    private bool IsBuilding()
    {
        if (buildingTilemap.HasTile(currentMouseWorldCellPos))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectionReceiver(int index)
    {
        selectedIndex = index;
    }

}
