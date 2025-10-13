using UnityEngine;
using UnityEngine.UI;

public class BottomScript : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        // Make sure buttons are assigned before we add listeners
        if (buttons == null || buttons.Length == 0)
        {
            Debug.LogError("No buttons assigned to BottomButtons!");
            return;
        }
    }

    void Start()
    {
        foreach (var button in buttons)
        {
            if (button == null)
            {
                Debug.LogWarning("One of the buttons in the array is null.");
                continue;
            }
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnBottomClicked(index));
        }
    }

    // This function will be called by each button, passing a number
    public void OnBottomClicked(int index)
    {
        Debug.Log($"Button {index} clicked!");
        //gridManager.SelectionReceiver(index);
    }
}
