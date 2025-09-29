using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
                Destroy(gameObject);
        }
    }
}
