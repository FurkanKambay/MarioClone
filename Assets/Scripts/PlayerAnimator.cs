using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement movement;
    private Health health;

    private SpriteRenderer sprite;
    private Animator animator;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<Health>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        health.DamageTaken += _ =>
        {
            animator.SetTrigger(animHurt);
            StartCoroutine(ResetTriggerAfterDelay());
        };

        health.Died += () => sprite.enabled = false;
        health.Respawned += () => sprite.enabled = true;
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = movement.WalkInput switch
        {
            < 0 => -1,
            > 0 => 1,
            _ => scale.x,
        };
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        animator.SetFloat(animSpeed, Mathf.Abs(movement.Velocity));
        animator.SetBool(animJump, movement.JumpInput && !movement.IsGrounded);
    }

    private IEnumerator ResetTriggerAfterDelay()
    {
        yield return new WaitForSeconds(health.InvincibilityTime);
        animator.ResetTrigger(animHurt);
    }

    private static readonly int animSpeed = Animator.StringToHash("Speed");
    private static readonly int animJump = Animator.StringToHash("Jump");
    private static readonly int animHurt = Animator.StringToHash("Hurt");
}
