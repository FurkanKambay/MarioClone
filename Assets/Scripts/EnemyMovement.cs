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
    private Animator animator;

    private Health playerHealth;

    private static readonly int animStunned = Animator.StringToHash("IsStunned");

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        var player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<Health>();
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
            if (box.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                playerHealth.InflictDamage(1, transform.position);
                return;
            }
        }
    }

    private void CheckIfDying()
    {
        if (topTrigger.IsTouchingLayers(LayerMask.GetMask("Player")))
            Die();
    }

    public void Die()
    {
        body.simulated = false;
        isDying = true;
        animator.SetBool(animStunned, isDying);
        Destroy(gameObject, StunDuration);
    }
}
