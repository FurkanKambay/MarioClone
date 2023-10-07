using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 100f;
    public float movement = 1f;
    public float stunDuration = 1f;

    public LayerMask bounceMask;
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

        Walk();
        Bounce();
        CheckIfDying();
        CheckAttackBoxes();
    }

    private void Walk()
    {
        var x = movement * speed * Time.fixedDeltaTime;
        body.velocity = new Vector2(x, body.velocity.y);
    }

    private void Bounce()
    {
        var bounceBox = sideTriggers[0];
        if (!bounceBox.IsTouchingLayers(bounceMask))
            return;

        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        movement = -movement;
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
            Invoke(nameof(Die), stunDuration);
        }
    }

    private void Die() => Destroy(this.gameObject);
}
