using UnityEngine;

public interface IPlayerState
{
    void OnEnter(PlayerStateMachine player);
    void OnUpdate();
    void OnFixedUpdate();
    void OnExit();
    void OnCollisionEnter(Collision2D collision);
    void OnJump();
    void OnMove(Vector2 input);
}
