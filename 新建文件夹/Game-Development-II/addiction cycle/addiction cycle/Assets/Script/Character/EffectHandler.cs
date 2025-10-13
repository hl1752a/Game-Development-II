using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : GridManager
{
    private Stack<int> effects = new Stack<int>();
    private Vector2Int currentCell = new Vector2Int(0, 0);
    private Vector2Int lastCell = new Vector2Int(0, 0);

    void Update()
    {
        currentCell = WorldCellToFileCell(roadTilemap.WorldToCell(transform.position));
        if(lastCell!= currentCell)
        {
            DateHandler();
            EffectRecevie();
            EffectImplement();
            
            lastCell = currentCell;
        }

    }
    private void DateHandler()
    {
        if(currentCell == new Vector2Int(3,2))
        {
            DataManager.AddDay();
        }
    }

    private void EffectRecevie()
    {
        for (int i = 0; i < testDirections.Length; i++)
        {
            
            int x = testDirections[i][0];
            int y = testDirections[i][1];

            Vector2Int testCell = new Vector2Int(currentCell.x + x, currentCell.y + y);
            effects.Push(GetGridAt(currentCell.x + x, currentCell.y + y));
        }
    }

    private void EffectImplement()
    {
        
        while (effects.Count > 0)
        { 
            switch (effects.Pop())
            {
                case 2: // work
                    DataManager.AddMoney(10);
                    Debug.Log("now money is " + DataManager.Data.Money);
                    break;
                case 3: // cigarette
                    Debug.Log("cigarette");
                    break;
                case 4: // bar
                    Debug.Log("bar");
                    break;
                case 5: // alcohol
                    Debug.Log("alcohol");
                    break;
                case 6: // drug
                    Debug.Log("drug");
                    break;

            }
        }
    }
}
