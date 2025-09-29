using System;
using UnityEngine;

public class ItemCollectNotifier : MonoBehaviour
{
    
    public static event Action<ItemType> OnItemCollected;

    public static void ItemCollected(ItemType type)
    {
        OnItemCollected?.Invoke(type);
    }
}

public enum ItemType
{
    Coin,
    PowerUp
}
