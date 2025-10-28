using UnityEngine;

public class PlayerMainSceneState : IPlayerState
{
    private PlayerStateMachine fsm;
    private PlayerContext ctx;
    private Vector2 moveInput;

    public void OnEnter(PlayerStateMachine player)
    {
        fsm = player;
        ctx = player.Context;

        if (ctx?.Rb == null)
        {
            Debug.LogError("[PlayerMainSceneState] Context or Rigidbody is null");
            return;
        }

        ctx.Rb.gravityScale = 0f;
        ctx.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        Debug.Log("[PlayerMainSceneState] Entered MainScene Mode");
    }

    public void OnUpdate()
    {
        ctx.MoveCtrl?.HandleJump();
        ctx.AnimCtrl?.UpdateMove(moveInput);
    }

    public void OnFixedUpdate()
    {
        ctx.MoveCtrl?.Move(moveInput);
    }

    public void OnExit() { }

    public void OnMove(Vector2 input)
    {
        moveInput = input;
        Debug.Log($"[MainSceneState] moveInput = {moveInput}");
    }

    public void OnJump()
    {
        if (ctx?.MoveCtrl == null)
        {
            Debug.LogWarning("[PlayerMainSceneState] MoveCtrl null, jump ignored.");
            return;
        }
        ctx.MoveCtrl.Jump();
    }

    public void OnCollisionEnter(Collision2D collision) { }
}
