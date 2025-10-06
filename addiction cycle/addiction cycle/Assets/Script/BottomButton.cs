using UnityEngine;
using UnityEngine.UI;

public class BottomButton : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    public Button[] button;

    void Start()
    {
        // Automatically hook up button click listeners
        for (int i = 2; i < button.Length; i++)
        {
            int index = i; // local copy to avoid closure issue
            button[i].onClick.AddListener(() => OnBottomClicked(index));
        }
    }

    // This function will be called by each button, passing a number
    public void OnBottomClicked(int index)
    {
        // Call your other script¡¯s function that takes an int
        if (gridManager != null)
        {
            gridManager.SelectionReceiver(index);
        }
    }
}
