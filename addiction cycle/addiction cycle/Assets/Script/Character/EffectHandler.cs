using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectHandler : GridManager
{
    private Stack<int> effects = new Stack<int>();
    private Vector2Int currentCell = new Vector2Int(0, 0);
    private Vector2Int lastCell = new Vector2Int(0, 0);

    [SerializeField] private Image image;

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
                    DataManager.Work();
                    break;
                case 3: // cigarette
                    DataManager.SpendMoney(5);
                    DataManager.EffectEnergy(20);
                    DataManager.AddXp(10);
                    break;
                case 4: // barIn
                    DataManager.SpendMoney(10);
                    DataManager.EffectEnergy(30);
                    DataManager.AddXp(15);
                    break;
                case 5: // alcohol
                    DataManager.SpendMoney(15);
                    DataManager.EffectEnergy(50);
                    DataManager.AddXp(50);
                    break;
                case 6: // drug
                    DataManager.SpendMoney(30);
                    DataManager.EffectEnergy(150);
                    DataManager.AddXp(100);
                    break;
            }
        }
        if (DataManager.Data.Money < 0)
        {
            Time.timeScale = 0f;
            image.GetComponent<Image>().enabled = true;
        }
    }
}
