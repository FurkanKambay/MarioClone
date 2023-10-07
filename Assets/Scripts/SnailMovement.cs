using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    public float speed = 100f;
    public float movement = 1f;
    public float stunDuration = 5f;

    public LayerMask bounceMask;
    [SerializeField] private BoxCollider2D forwardTrigger;
    [SerializeField] private BoxCollider2D topTrigger;

    private bool isStunned;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator animator;

    private static readonly int animStunned = Animator.StringToHash("IsStunned");

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        animator.SetBool(animStunned, isStunned);
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
            Invoke(nameof(RemoveStun), stunDuration);
        }
    }

    private void RemoveStun() => isStunned = false;
}
