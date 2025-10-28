using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState _currentState;
    private PlayerContext _context;

    public void Initialize(PlayerManager manager)
    {
        _context = new PlayerContext(manager.Player);
        Debug.Log($"[StateMachine] Context initialized with {_context?.Rb}");
    }

    public void ChangeState(IPlayerState newState)
    {
        Debug.Log($"[StateMachine] ChangeState -> {newState.GetType().Name}");
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter(this);
    }
    private void Update() => _currentState?.OnUpdate();
    private void FixedUpdate() => _currentState?.OnFixedUpdate();

    public void OnMove(Vector2 input)
    {
        Debug.Log($"[StateMachine] HandleMove -> {input}");
        if (_currentState == null)
            Debug.LogError("[StateMachine] currentState is NULL!");
        _currentState?.OnMove(input);
    }
    public void OnJump() => _currentState?.OnJump();

    public PlayerContext Context => _context;
}
