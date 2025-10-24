using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType { Zone, NPC }

public interface IInteractable
{
    InteractType Type { get; }
    void OnPlayerEnter();
    void OnPlayerExit();
    void TryInteract();
}
