using UnityEngine;

public class FlappyState : IPlayerState
{
    private PlayerStateMachine player;
    private Rigidbody2D rb;

    private bool isAlive = true;
    private float jumpForce = 5f;

    public void OnEnter(PlayerStateMachine _player)
    {
        player = _player;
        rb = PlayerManager.Instance.Rb;

        rb.gravityScale = 2f;
        rb.velocity = Vector2.zero;
        isAlive = true;

        Debug.Log("[FlappyState] Entered FlappyBird Mode");
    }
    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
    }

    public void OnCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("[FlappyPlayer] Collided!");
            GameManager.Instance.CurrentMiniGame.GameOver();
        }
    }

    public void OnMove(Vector2 input)
    {

    }


    public void OnJump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

}
