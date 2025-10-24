using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    public static IInteractable CurrentInteractable { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            CurrentInteractable = interactable;
            interactable.OnPlayerEnter();
            Debug.Log($"Enter: {interactable.Type}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable) &&
            interactable == CurrentInteractable)
        {
            interactable.OnPlayerExit();
            CurrentInteractable = null;
        }
    }
}