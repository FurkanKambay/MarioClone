using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 100f;
    public float Movement = 1f;
    public float StunDuration = 1f;

    [SerializeField] private LayerMask bounceMask;
    [SerializeField] private BoxCollider2D topTrigger;

    [Tooltip("The first item must be the forward (bounce) trigger")]
    [SerializeField] private BoxCollider2D[] sideTriggers;

    private bool isDying;

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

    private void FixedUpdate()
    {
        if (isDying)
            return;

        Walk(Time.deltaTime);
        Bounce();
        CheckIfDying();
        CheckAttackBoxes();
    }

    private void Walk(float delta)
    {
        float x = Movement * Speed * delta;
        body.velocity = new Vector2(x, body.velocity.y);
    }

    private void Bounce()
    {
        BoxCollider2D bounceBox = sideTriggers[0];
        if (!bounceBox.IsTouchingLayers(bounceMask))
            return;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        Movement = -Movement;
    }

    private void CheckAttackBoxes()
    {
        foreach (BoxCollider2D box in sideTriggers)
        {
            if (!box.IsTouchingLayers(LayerMask.GetMask("Player")))
                continue;

            // kill player
            // + death screen or respawn
            return;
        }
    }

    private void CheckIfDying()
    {
        if (topTrigger.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            isDying = true;
            animator.SetBool(animStunned, isDying);
            Invoke(nameof(Die), StunDuration);
        }
    }

    private void Die() => Destroy(this.gameObject);
}
