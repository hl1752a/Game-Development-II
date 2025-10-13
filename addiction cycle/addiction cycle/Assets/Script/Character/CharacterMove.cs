using UnityEngine;

public class CharacterMove : GridManager
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector2Int lastPos = new Vector2Int(0, 0);
    [SerializeField] private Vector2Int currentPos = new Vector2Int(0, 0);
    [SerializeField] private Vector2Int nextPos = new Vector2Int(0, 0);

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GridDataHandler.gameGrid != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, nextPos) < 0.01f)
            {
                currentPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                nextPos = FindNext(currentPos);
                lastPos = currentPos;
            }
                
        }
        if (currentPos.x > nextPos.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private Vector2Int FindNext(Vector2Int pos)
    {
        for (int i = 0; i < testDirections.Length; i++)
        {
            int x = testDirections[i][0];
            int y = testDirections[i][1];
            Vector2Int testPos = new Vector2Int(pos.x + x, pos.y + y);

            if (GetGridAt(testPos.x, testPos.y) == 1)
            {
                if (testPos != lastPos && testPos != currentPos)
                {
                    return testPos;
                }
            }
        }


        return pos; // should never happen 
    }
}
