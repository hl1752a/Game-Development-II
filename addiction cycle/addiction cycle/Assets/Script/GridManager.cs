using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : GridReader
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected int[][] testDirections = new int[][]
    {
        new int[] {-1, 0}, // left (x-1, y)
        new int[] {1, 0},  // right (x+1, y)
        new int[] {0, -1}, // down (x, y-1)
        new int[] {0, 1}   // up (x, y+1)
    };

    [SerializeField] private Tilemap buildingTilemap;   // buildings
    [SerializeField] private Tilemap roadTilemap;       // road
    [SerializeField] private Tilemap previewTilemap;    // ghost preview

    [SerializeField] private TileBase[] buildingTile;
    [SerializeField] private TileBase previewTile;     // Semi-transparent version

    private Vector3Int currentCellPos = new Vector3Int(0, 0, 0);
    private Vector3 mouseWorldPos = new Vector3(0, 0, 0);
    private bool isOccupied = true;

    private int selectedIndex = 2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentCellPos = buildingTilemap.WorldToCell(mouseWorldPos);
        isOccupied = ShowPreview();
        if (Input.GetMouseButtonDown(0) && !isOccupied)
        {
            PlaceTile();
        }
    }

    protected Vector2Int CellPosConvert(Vector3Int originalPos)
    {
        return new Vector2Int(originalPos.x+5, originalPos.y+5);
    }

    protected void GridUpdate()
    {

    }

    private bool ShowPreview()
    {
        previewTilemap.ClearAllTiles();

        // Don¡¯t preview if already occupied
        if (buildingTilemap.HasTile(currentCellPos) || roadTilemap.HasTile(currentCellPos))
        {
            return true;
        }
        else
        {
            previewTilemap.SetTile(currentCellPos, previewTile);
            return false;
        }           
    }

    private void PlaceTile()
    {
        if (selectedIndex < 0) return;
        buildingTilemap.SetTile(currentCellPos, buildingTile[selectedIndex]);

    }
}
