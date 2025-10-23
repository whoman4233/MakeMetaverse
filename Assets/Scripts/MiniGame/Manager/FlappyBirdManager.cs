using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyGameManager : MonoBehaviour, IMiniGameManager
{
    private int score;
    private bool isGameOver;
    [SerializeField] private PipeSpawner spawner;
    [SerializeField] private FlappyUI ui;

    private void Start()
    {
        GameManager.Instance.RegisterMiniGame(this);
    }

    public void StartGame()
    {
        score = 0;
        isGameOver = false;
        spawner.enabled = true;
        Debug.Log("[Flappy] StartGame");
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        ui.SetScore(score);
        Debug.Log($"[Flappy] Score: {score}");
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("[FlappyGameManager] Game Over!");
        spawner.enabled = false;
        Debug.Log("[Flappy] GameOver");
        ui.ShowGameOver(score);
        Time.timeScale = 0;
    }

    public int GetScore() => score;

    private void ReturnToHub()
    {
        GameManager.Instance.EndMiniGame(score);
    }
}
