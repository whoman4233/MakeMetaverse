using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CapsuleCollider2D col;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("E를 누르면 미니게임이 실행됩니다.");
    }
}
