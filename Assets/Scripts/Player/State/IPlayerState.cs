using UnityEngine;

public interface IPlayerState
{
    void OnEnter(PlayerStateMachine player);
    void OnUpdate();
    void OnExit();
    void OnCollisionEnter(Collision2D collision);
    void OnJump();
    void OnMove(Vector2 input);
}
