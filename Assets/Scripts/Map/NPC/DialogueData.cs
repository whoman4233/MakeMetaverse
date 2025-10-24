using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Game/Dialogue Data", order = 0)]
public class DialogueData : ScriptableObject
{
    [Header("��ȭ ID (Optional)")]
    public string dialogueId;       // ����/�ҷ������ �ĺ��� (����)

    [Header("��� ���")]
    public List<DialogueLine> lines = new List<DialogueLine>();

    [Header("������ (Optional)")]
    public List<DialogueChoice> choices = new List<DialogueChoice>();
}

[System.Serializable]
public class DialogueLine
{
    [Tooltip("ȭ�� �̸� (������ �ڵ����� NPC �̸� ���)")]
    public string speakerName;

    [TextArea(2, 5)]
    public string content;

    [Tooltip("�� ��簡 ������ �ڵ����� ���� �������� �Ѿ�� ����")]
    public bool autoContinue = true;

    [Tooltip("Ư�� ���ǿ����� ǥ�� (��: ����Ʈ �ܰ� ��)")]
    public string conditionKey;
}

[System.Serializable]
public class DialogueChoice
{
    [Tooltip("�÷��̾ ������ �� �ִ� ����")]
    public string optionText;

    [Tooltip("���� �� Ʈ���ŵ� �̺�Ʈ (��: ���� ����, ����Ʈ ���� ��)")]
    public string triggerEvent;

    [Tooltip("���� �� �̾��� ��� ������ (�б�)")]
    public DialogueData nextDialogue;
}
