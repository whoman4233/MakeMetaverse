using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool pipePool;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float spawnX = 10f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            _timer = 0f;
            float randomY = Random.Range(-1.5f, 3);
            Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);
            pipePool.GetFromPool(spawnPos);
        }
    }
}
