using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public int LastScore { get; private set; }
    public string LastMiniGame { get; private set; }
    public event Action<string> OnMiniGameStarted;
    public event Action<string, int> OnMiniGameEnded;
    public static event Action<string> OnSceneChanged;

    public IMiniGameManager currentMiniGame;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    void Start()
    {
        // 첫 씬도 강제로 한번 방송 (놓치는 경우 방지)
        var current = SceneManager.GetActiveScene().name;
        Debug.Log($"[GameManager] Start broadcast: {current}");
        OnSceneChanged?.Invoke(current);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[GameManager] Scene Loaded: {scene.name}");
        OnSceneChanged?.Invoke(scene.name);
    }

    public void LaunchMiniGame(string sceneName)
    {
        LastMiniGame = sceneName;
        OnMiniGameStarted?.Invoke(sceneName); // FSM / PlayerManager에 알림
        SceneManager.LoadScene(sceneName);
    }

    public void RegisterMiniGame(IMiniGameManager manager)
    {
        currentMiniGame = manager;
        currentMiniGame.StartGame();
    }

    public void EndMiniGame(int score)
    {
        Debug.Log($"[GameManager] MiniGame ended with score {score}");
        currentMiniGame = null;
        SceneManager.LoadScene("MainScene");
    }

    public IMiniGameManager CurrentMiniGame => currentMiniGame;
}
