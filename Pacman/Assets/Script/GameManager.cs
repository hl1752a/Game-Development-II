using UnityEngine;
using UnityEngine.SceneManagement;
using static GameStateNotifier;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameStateNotifier.OnGameStateChange += StateChange;
    }

    private void OnDisable()
    {
        GameStateNotifier.OnGameStateChange -= StateChange;
    }

    private void StateChange(GameState state)
    {
        if(state == GameState.Reset)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        // reload the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
