using System;
using UnityEngine;

public class PlayerStateNotifier : MonoBehaviour
{
    public static event Action<PlayerState> OnPlayerStateChange;

    public static void PlayerStateChange(PlayerState state)
    {
        OnPlayerStateChange?.Invoke(state);
    }

    public enum PlayerState
    {
        Normal,
        Invincible
    }
}
