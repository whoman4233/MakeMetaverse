using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerManager player;
    private IPlayerState _currentState;

    public void Initialize(PlayerManager manager)
    {
        player = manager; // FSM�� � ������Ʈ�� �������� ����
    }

    public void ChangeState(IPlayerState newState)
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState?.OnEnter(this);
    }

    private void Update()
    {
        _currentState?.OnUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _currentState?.OnCollisionEnter(collision);
    }

    public void HandleMove(Vector2 moveInput)
    {
        _currentState?.OnMove(moveInput);
    }

    public void HandleJump()
    {
        _currentState?.OnJump();
    }
}
