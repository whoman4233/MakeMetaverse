using UnityEngine;

public class NPCInteractZone : MonoBehaviour, IInteractable
{
    public InteractType Type => InteractType.NPC;
    public DialogueData dialogue;

    public void OnPlayerEnter()
    {
        if (dialogue != null)
            UIManager.Instance.ShowZonePrompt(transform, "E : ¥Î»≠");
    }

    public void OnPlayerExit()
    {
        UIManager.Instance.HideZonePrompt();
    }

    public void TryInteract()
    {
        if (dialogue == null) return;
        Debug.Log($"Interacting with {dialogue.dialogueId}");

        UIManager.Instance.ShowDialogue(dialogue);
    }
}