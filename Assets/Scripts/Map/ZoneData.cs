using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SJH/Zone Data", fileName = "ZoneData")]
public class ZoneData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string zoneName;
    [TextArea] public string description;

    [Header("�� �̵� ����")]
    public string sceneToLoad;
    public bool requireInput = true; // true�� EŰ �Է� �� �̵�
}