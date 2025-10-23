using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;   // 풀링할 오브젝트
    [SerializeField] private int initialSize = 5; // 초기 생성 개수

    private readonly Queue<GameObject> _pool = new();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
            CreateNewObject();
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        _pool.Enqueue(obj);
        return obj;
    }

    /// <summary>
    /// 풀에서 오브젝트 하나 꺼내서 활성화
    /// </summary>
    public GameObject GetFromPool(Vector3 position)
    {
        if (_pool.Count == 0)
            CreateNewObject();

        GameObject obj = _pool.Dequeue();
        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 오브젝트를 다시 풀로 반환
    /// </summary>
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}