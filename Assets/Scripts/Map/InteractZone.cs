using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractZone : MonoBehaviour
{
    public ZoneData zoneData;

    public void OnPlayerEnter()
    {
        if (zoneData != null)
            UIManager.Instance.ShowZonePrompt(zoneData.zoneName, zoneData.description);
    }

    public void OnPlayerExit()
    {
        UIManager.Instance.HideZonePrompt();
    }

    public void TryInteract()
    {
        if (zoneData == null) return;
        Debug.Log($"Interacting with {zoneData.zoneName}");

        if (!string.IsNullOrEmpty(zoneData.sceneToLoad))
            GameManager.Instance.LaunchMiniGame(zoneData.sceneToLoad);
        else
            Debug.Log("대화 혹은 이벤트 트리거 실행");
    }
}