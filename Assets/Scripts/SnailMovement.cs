using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    public float speed = 100f;
    public float movement = 1f;
    public LayerMask bounceMask;

    public bool isStunned;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private BoxCollider2D forwardTrigger;
    [SerializeField] private BoxCollider2D topTrigger;

    private static readonly int animStunned = Animator.StringToHash("IsStunned");

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool(animStunned, isStunned);
    }

    private void FixedUpdate()
    {
        SnailMove();
        SnailBounce();
        SnailStun();
    }

    private void SnailMove()
    {
        if (isStunned)
            return;

        var x = movement * speed * Time.fixedDeltaTime;
        body.velocity = new Vector2(x, body.velocity.y);
    }

    private void SnailBounce()
    {
        if (forwardTrigger.IsTouchingLayers(bounceMask))
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            movement = -movement;
        }
    }

    private void SnailStun()
    {
        if (topTrigger.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            isStunned = true;
            // remove stun after 5 seconds
            Invoke(nameof(ResetStun), 5f);
        }
    }

    private void ResetStun() => isStunned = false;
}
