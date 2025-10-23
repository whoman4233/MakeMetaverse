using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject zonePromptPanel;
    [SerializeField] private TMP_Text zoneNameText;
    [SerializeField] private TMP_Text zoneDescText;

    public void ShowZonePrompt(string title, string desc)
    {
        if (!zonePromptPanel) return;
        zonePromptPanel.SetActive(true);
        zoneNameText.text = title;
        zoneDescText.text = desc + "\n[E] 키로 진입";
    }

    public void HideZonePrompt()
    {
        if (zonePromptPanel)
            zonePromptPanel.SetActive(false);
    }
}
