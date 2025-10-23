using UnityEngine;
using UnityEngine.Windows;

public class PlayerMainSceneState : IPlayerState
{
    private PlayerStateMachine _player;
    private Rigidbody2D rb;
    private float speed = 3f;
    private Vector2 moveInput;

    // 점프 관련
    private bool isJumping = false;
    private float jumpHeight = 1.5f;
    private float jumpDuration = 0.5f;
    private float jumpTimer = 0f;
    private float startY;

    public void OnEnter(PlayerStateMachine player)
    {
        _player = player;
        rb = PlayerManager.Instance.Rb;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void OnUpdate()
    {

        Vector2 newPos = rb.position + moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        if (!isJumping) return;

        jumpTimer += Time.deltaTime;
        float t = jumpTimer / jumpDuration;

        // 커브 기반으로 위→아래 이동 (포물선 느낌)
        float heightOffset = Mathf.Sin(Mathf.PI * t) * jumpHeight;
        rb.MovePosition(new Vector2(rb.position.x, startY + heightOffset));

        if (t >= 1f)
        {
            isJumping = false;
            rb.MovePosition(new Vector2(rb.position.x, startY)); // 착지
        }
    }

    public void OnExit() { }

    public void OnCollisionEnter(Collision2D collision)
    {
        // 허브에서는 충돌 시 아무 일 없음
    }

    public void OnJump()
    {
        if (isJumping) return; // 연속 점프 방지
        isJumping = true;
        jumpTimer = 0f;
        startY = rb.position.y;
    }

    public void OnMove(Vector2 input)
    {
        moveInput = input;
    }
}
