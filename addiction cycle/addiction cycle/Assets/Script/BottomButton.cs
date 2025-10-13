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
            button[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }
    void FixedUpdate()
    {
        if (DataManager.Data.Level >= 15)
        {
            ButtonUnlock(6);
        }
        else if (DataManager.Data.Level >= 10)
        {
            ButtonUnlock(5);
        }
        else if (DataManager.Data.Level >= 5)
        {
            ButtonUnlock(4);
        }
        else if (DataManager.Data.Money >= 60)
        {
            ButtonUnlock(3);
        }
        
    }
    // This function will be called by each button, passing a number
    public void OnButtonClicked(int index)
    {
        // Call your other script¡¯s function that takes an int
        if (gridManager != null)
        {
            gridManager.SelectionReceiver(index);
        }
    }

    private void ButtonUnlock(int index)
    {
        button[index].GetComponent<Image>().enabled = true;
        button[index].GetComponent<Button>().enabled = true;
    }
}
