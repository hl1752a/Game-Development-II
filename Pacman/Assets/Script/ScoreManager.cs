using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    public int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    private void OnEnable()
    {
        ItemCollectNotifier.OnItemCollected += ItemPickUp;
    }

    private void OnDisable()
    {
        ItemCollectNotifier.OnItemCollected -= ItemPickUp;

    }

    public void ItemPickUp(ItemType type)
    {
        if (type == ItemType.Coin)
        {
            score += 100;
        }
        else if(type == ItemType.PowerUp)
        {
            score += 500;
        }
        scoreText.text = "SCORE: " + score.ToString();
    }
}
