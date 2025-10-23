using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SJH/Zone Data", fileName = "ZoneData")]
public class ZoneData : ScriptableObject
{
    [Header("기본 정보")]
    public string zoneName;
    [TextArea] public string description;

    [Header("씬 이동 관련")]
    public string sceneToLoad;
    public bool requireInput = true; // true면 E키 입력 후 이동
}