using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public int highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Awake()
    {
        // Sets up the singleton instance
        instance = this;
    }

    void Start()
    {
        // Loads the saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddPoint()
    {
        // Increases the score when the player passes an obstacle
        score++;

        // Updates and saves the high score if needed
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    public int GetScore()
    {
        // Returns the current score
        return score;
    }

    public int GetHighScore()
    {
        // Returns the saved high score
        return highScore;
    }

    void UpdateUI()
    {
        // Updates the score text on screen
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        // Updates the high score text on screen
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}