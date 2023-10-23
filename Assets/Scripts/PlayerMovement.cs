using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event Action Jumped;

    public float Speed = 100f;
    public float JumpForce = 5f;
    public Vector2 HurtForce = Vector2.one * 5f;

    public float WalkInput { get; private set; }
    public float Velocity => body.velocity.x;

    [SerializeField] private BoxCollider2D groundTrigger;

    private Rigidbody2D body;
    private Health health;

    public bool IsGrounded { get; private set; }
    public bool JumpInput { get; private set; }
    public bool CanMove { get; private set; } = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.DamageTaken += OnDamageTaken;
        health.Died += () => body.simulated = CanMove = false;
        health.Respawned += () => body.simulated = CanMove = true;
    }

    private void OnDamageTaken(Vector3 sourcePosition)
    {
        CanMove = false;

        Vector3 direction = (sourcePosition - transform.position).normalized;
        body.velocity = new Vector2(-direction.x * HurtForce.x, HurtForce.y);

        IEnumerator EnableMovementAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            CanMove = true;
        }

        StartCoroutine(EnableMovementAfterDelay());
    }

    private void Update()
    {
        WalkInput = Input.GetAxisRaw("Horizontal");
        JumpInput = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        IsGrounded = groundTrigger.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (groundTrigger.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            Jump();

        if (!CanMove)
            return;

        Walk(Time.deltaTime);

        if (JumpInput && IsGrounded)
            Jump();
    }

    private void Walk(float delta)
    {
        float x = WalkInput * Speed * delta;
        body.velocity = new Vector2(x, body.velocity.y);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, JumpForce);
        Jumped.Invoke();
    }
}
