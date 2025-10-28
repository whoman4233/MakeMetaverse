using UnityEngine;
using UnityEngine.UI;

public class FlappyUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverScore;
    [SerializeField] private Text highScore;

    private int score;

    public void SetScore(int score)
    {
        scoreText.text = $"SCORE: {score}";
    }

    public void ShowGameOver(int finalScore)
    {
        score = finalScore;
        gameOverPanel.SetActive(true);
        gameOverScore.text = $"SCORE\n{finalScore}";
    }

    public void ShowStart()
    {
        gameOverPanel.SetActive(false);
        scoreText.text = "0";
    }

    public void RetryBtn()
    {
        GameManager.Instance.LaunchMiniGame("MiniGame_Flappy");
        Time.timeScale = 1;
    }

    public void StopPlayingBtn()
    {
        GameManager.Instance.EndMiniGame(score);
        Time.timeScale = 1;
    }
}
