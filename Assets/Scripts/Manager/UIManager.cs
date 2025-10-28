using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Canvas & UI Panels")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private RectTransform zonePromptPanel;
    [SerializeField] private Text zonePromptText;

    private Camera mainCamera;
    private Transform target; // 현재 프롬프트가 따라갈 대상
    private bool isFollowing;

    [Header("Dialogue")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text speakerText;
    [SerializeField] private Text contentText;
    [SerializeField] private Button nextButton;


    private DialogueData currentDialogue;
    private int currentIndex;
    private GameObject currentPlayer;

    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.GetComponent<CameraFollow>().SetTarget(PlayerManager.Instance.Player.transform);
        zonePromptPanel.gameObject.SetActive(false);
        currentPlayer = PlayerManager.Instance.Player;
        nextButton.onClick.AddListener(ShowNextLine);
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    void Update()
    {
        if (isFollowing && target != null)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position + Vector3.up * 1.5f);
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                mainCanvas.transform as RectTransform,
                screenPos,
                mainCanvas.worldCamera,  // Overlay라면 null 가능
                out anchoredPos
            );
            zonePromptPanel.anchoredPosition = anchoredPos;
        }
    }


    public void ShowZonePrompt(Transform followTarget, string message)
    {
        target = followTarget;
        zonePromptText.text = message;
        zonePromptPanel.gameObject.SetActive(true);
        isFollowing = true;
    }

    public void HideZonePrompt()
    {
        isFollowing = false;
        zonePromptPanel.gameObject.SetActive(false);
        target = null;
    }

    public void ShowDialogue(DialogueData dialogue)
    {
        dialoguePanel.SetActive(true);
        currentDialogue = dialogue;
        currentIndex = 0;
        ShowNextLine();
    }

    private void ShowNextLine()
    {
        if (currentIndex >= currentDialogue.lines.Count)
        {
            dialoguePanel.SetActive(false);
            return;
        }

        DialogueLine line = currentDialogue.lines[currentIndex];
        speakerText.text = line.speakerName;
        contentText.text = line.content;
        currentIndex++;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 메인 카메라 다시 찾아서 갱신
        mainCamera = Camera.main;

        if (mainCamera != null)
        {
            Debug.Log($"[UIManager] Reassigned mainCamera: {mainCamera.name}");
            var camFollow = mainCamera.GetComponent<CameraFollow>();
            if (camFollow != null && PlayerManager.Instance != null)
                camFollow.SetTarget(PlayerManager.Instance.Player.transform);
            if(mainCamera.TryGetComponent<CameraFollow>(out  camFollow))
            {
                camFollow.SetTarget(PlayerManager.Instance.Player.transform);
            }
        }
    }
}
