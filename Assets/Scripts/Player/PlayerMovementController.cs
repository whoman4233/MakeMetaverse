using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 3f;

    // 점프 관련
    private bool isJumping;
    private float jumpHeight = 1.5f;
    private float jumpDuration = 0.5f;
    private float jumpTimer;
    private float startY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 이동 처리 (Top-down용)
    /// </summary>
    public void Move(Vector2 input)
    {
        Vector2 newPos = rb.position + input * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPos);
    }

    /// <summary>
    /// 점프 시작 (간단한 y축 포물선)
    /// </summary>
    public void Jump()
    {
        if (isJumping) return;
        isJumping = true;
        jumpTimer = 0f;
        startY = rb.position.y;
    }

    /// <summary>
    /// 점프 중 위치 갱신
    /// </summary>
    public void HandleJump()
    {
        if (!isJumping) return;

        jumpTimer += Time.deltaTime;
        float t = jumpTimer / jumpDuration;
        float heightOffset = Mathf.Sin(Mathf.PI * t) * jumpHeight;
        rb.MovePosition(new Vector2(rb.position.x, startY + heightOffset));

        if (t >= 1f)
        {
            isJumping = false;
            rb.MovePosition(new Vector2(rb.position.x, startY));
        }
    }
}
