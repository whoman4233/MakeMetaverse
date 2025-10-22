using System.Windows.Input;
using UnityEditor.Timeline.Actions;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputController : MonoBehaviour
{
    private PlayerInput input;
    private Vector2 moveInput;
    private Rigidbody2D rigidbody2d;
    public float speed = 5f;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (moveInput == Vector2.zero) return;
        Vector2 newPos = rigidbody2d.position + moveInput * speed * Time.fixedDeltaTime;
        GetComponent<Rigidbody2D>().MovePosition(newPos);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
