using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMiniGameManager
{
    void StartGame();
    void AddScore(int amount);
    void GameOver();
    int GetScore();
}