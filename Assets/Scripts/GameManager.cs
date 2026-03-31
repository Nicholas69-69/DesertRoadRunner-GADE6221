using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Makes this script accessible from other scripts
    public static GameManager instance;

    // Tracks whether the game is already over
    private bool isGameOver = false;

    void Awake()
    {
        // Sets up the singleton instance
        instance = this;
    }

    public void OnPlayerDeath()
    {
        // Stops this method from running more than once
        if (isGameOver) return;

        isGameOver = true;

        // Pauses the game
        Time.timeScale = 0f;

        // Shows the death screen UI
        UIManager.instance.ShowDeathScreen();
    }

    public void RestartGame()
    {
        // Resets time back to normal
        Time.timeScale = 1f;

        // Reloads the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}