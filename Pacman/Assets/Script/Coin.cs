using UnityEngine;

public class Coin : Item
{
    // + 1 score
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Score + 1");
        }
    }
}
