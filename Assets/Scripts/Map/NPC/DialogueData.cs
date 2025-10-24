using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Game/Dialogue Data", order = 0)]
public class DialogueData : ScriptableObject
{
    [Header("대화 ID (Optional)")]
    public string dialogueId;       // 저장/불러오기용 식별자 (선택)

    [Header("대사 목록")]
    public List<DialogueLine> lines = new List<DialogueLine>();

    [Header("선택지 (Optional)")]
    public List<DialogueChoice> choices = new List<DialogueChoice>();
}

[System.Serializable]
public class DialogueLine
{
    [Tooltip("화자 이름 (없으면 자동으로 NPC 이름 사용)")]
    public string speakerName;

    [TextArea(2, 5)]
    public string content;

    [Tooltip("이 대사가 끝나면 자동으로 다음 선택지로 넘어갈지 여부")]
    public bool autoContinue = true;

    [Tooltip("특정 조건에서만 표시 (예: 퀘스트 단계 등)")]
    public string conditionKey;
}

[System.Serializable]
public class DialogueChoice
{
    [Tooltip("플레이어가 선택할 수 있는 문장")]
    public string optionText;

    [Tooltip("선택 시 트리거될 이벤트 (예: 상점 열기, 퀘스트 시작 등)")]
    public string triggerEvent;

    [Tooltip("선택 시 이어질 대사 데이터 (분기)")]
    public DialogueData nextDialogue;
}
