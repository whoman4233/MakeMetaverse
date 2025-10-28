using UnityEngine;

public class FlappyState : IPlayerState
{
    private PlayerStateMachine fsm;
    private PlayerContext ctx;
    private bool isAlive = true;
    private float jumpForce = 5f;

    public void OnEnter(PlayerStateMachine player)
    {
        fsm = player;
        ctx = player.Context;

        ctx.Rb.gravityScale = 2f;
        ctx.Rb.velocity = Vector2.zero;
        isAlive = true;

        // 플래피 상태에서는 이동 애니 비활성화
        ctx.Anim.SetBool("isMove", false);
        ctx.Anim.SetFloat("MoveX", 0);
        ctx.Anim.SetFloat("MoveY", 1);

        Debug.Log("[FlappyState] Entered FlappyBird Mode");
    }

    public void OnUpdate()
    {
        // 사망 상태면 입력 무시
        if (!isAlive) return;

        // 좌우 이동 방지 (x축 고정)
        var pos = ctx.Rb.position;
        pos.x = 0;
        ctx.Rb.position = pos;
    }

    public void OnExit()
    {
        ctx.Rb.gravityScale = 0f;
        ctx.Rb.velocity = Vector2.zero;
        Debug.Log("[FlappyState] Exited FlappyBird Mode");
    }

    public void OnCollisionEnter(Collision2D collision)
    {
        if (!isAlive) return;
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isAlive = false;
            ctx.Rb.velocity = Vector2.zero;
            Debug.Log("[FlappyState] Collided! Game Over.");
            GameManager.Instance.CurrentMiniGame?.GameOver();
        }
    }

    public void OnMove(Vector2 input)
    {
        // FlappyBird에서는 이동 입력 무시
    }

    public void OnJump()
    {
        if (!isAlive) return;
        ctx.Rb.velocity = Vector2.up * jumpForce;
    }

    public void OnFixedUpdate()
    { 
    }
}
