using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputController : MonoBehaviour
{
    private PlayerInput input;
    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    // InputSystem Event - Move
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        if (context.performed || context.canceled)
            stateMachine.HandleMove(move);
    }

    // InputSystem Event - Jump
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            stateMachine.HandleJump();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Input Interact");
        if (context.performed && InteractDetector.CurrentZone != null)
        {
            InteractDetector.CurrentZone.TryInteract();
        }
    }
}
