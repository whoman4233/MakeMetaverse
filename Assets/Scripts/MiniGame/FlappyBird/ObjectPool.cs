using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;   // Ǯ���� ������Ʈ
    [SerializeField] private int initialSize = 5; // �ʱ� ���� ����

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
    /// Ǯ���� ������Ʈ �ϳ� ������ Ȱ��ȭ
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
    /// ������Ʈ�� �ٽ� Ǯ�� ��ȯ
    /// </summary>
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}