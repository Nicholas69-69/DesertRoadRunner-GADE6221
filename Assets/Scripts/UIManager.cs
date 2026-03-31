using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI finalScoreText;

    public GameObject deathScreen;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        deathScreen.SetActive(false);
    }

    void Update()
    {
        if (ScoreManager.instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.instance.GetScore();

            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + ScoreManager.instance.GetHighScore();
            }
        }
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);

        if (ScoreManager.instance != null)
        {
            finalScoreText.text = "Final Score: " + ScoreManager.instance.GetScore();
        }
    }
}