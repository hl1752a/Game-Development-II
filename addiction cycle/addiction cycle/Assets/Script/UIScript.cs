using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Day;
    [SerializeField] private TextMeshProUGUI Money;
    [SerializeField] private TextMeshProUGUI Energy;
    [SerializeField] private TextMeshProUGUI Level;

    // Update is called once per frame
    void FixedUpdate()
    {
        Day.text = "Day " + DataManager.Data.Day;
        Money.text = "Money " + DataManager.Data.Money;
        Energy.text = "Energy " + DataManager.Data.Energy;
        if(DataManager.Data.Level <= 15)
        {
            Level.text = "Level " + DataManager.Data.Level;
        }
        else
        {
            Level.text = "Addiction Level " + DataManager.Data.Level;
        }

    }
}
