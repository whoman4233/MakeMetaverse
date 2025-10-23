using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private ObjectPool _pool;

    private void Start()
    {
        _pool = GetComponentInParent<ObjectPool>();
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // È­¸é ¿ÞÂÊ ¹þ¾î³ª¸é ¹ÝÈ¯
        if (transform.position.x < -10f)
        {
            _pool.ReturnToPool(gameObject);
            GameManager.Instance.currentMiniGame.AddScore(1);
        }
    }
}
