using System;
using UnityEngine;

public class GameStateNotifier : MonoBehaviour
{
    public static event Action<GameState> OnGameStateChange; // could add something

    public static void GameStateChange(GameState state)
    {
        OnGameStateChange?.Invoke(state);
    }

    public enum GameState
    {
       Reset
    }
}
