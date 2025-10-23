using UnityEngine;
using UnityEngine.UI;

public class FlappyUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverScore;
    [SerializeField] private Text highScore;

    public void SetScore(int score)
    {
        scoreText.text = $"SCORE: {score}";
    }

    public void ShowGameOver(int finalScore)
    {
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
        GameManager.Instance.LoadScene("MiniGame_Flappy");
        Time.timeScale = 1;
    }

    public void StopPlayingBtn()
    {
        GameManager.Instance.LoadScene("MainScene");
        Time.timeScale = 1;
    }
}
