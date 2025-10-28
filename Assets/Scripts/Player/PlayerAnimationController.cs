using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateMove(Vector2 input)
    {
        if (anim == null) return;

        bool moving = input.sqrMagnitude > 0.01f;
        anim.SetBool("isMove", moving);

        if (moving)
        {
            anim.SetFloat("MoveX", input.x);
            anim.SetFloat("MoveY", input.y);
        }
    }
}
