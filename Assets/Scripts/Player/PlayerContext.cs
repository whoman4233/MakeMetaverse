using UnityEngine;

public class PlayerContext
{
    public GameObject Player;
    public Rigidbody2D Rb;
    public Animator Anim;
    public PlayerMovementController MoveCtrl;
    public PlayerAnimationController AnimCtrl;

    public PlayerContext(GameObject player)
    {
        Player = player;
        Rb = player.GetComponent<Rigidbody2D>();
        Anim = player.GetComponent<Animator>();
        MoveCtrl = player.GetComponent<PlayerMovementController>();
        AnimCtrl = player.GetComponent<PlayerAnimationController>();
    }
}
