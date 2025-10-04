using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMove : GridManager
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector2Int lastPos = new Vector2Int(0, 0);
    [SerializeField] private Vector2Int currentPos = new Vector2Int(0, 0);
    [SerializeField] private Vector2Int nextPos = new Vector2Int(0, 0);

    void Update()
    {
        if (gameGrid != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, nextPos) < 0.01f)
            {
                currentPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                nextPos = findnext(currentPos);
                lastPos = currentPos;
            }
                
        }
    }



    private Vector2Int findnext(Vector2Int pos)
    {
        for (int i = 0; i < testDirections.Length; i++)
        {
            int x = testDirections[i][0];
            int y = testDirections[i][1];
            Vector2Int testPos = new Vector2Int(pos.x + x, pos.y + y);

            if (GetRoadAt(testPos.x, testPos.y) == 1)
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
