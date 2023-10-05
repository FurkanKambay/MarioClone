using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100f;
    public float jumpForce = 5f;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;
    private CircleCollider2D groundTrigger;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool shouldJump;
    private float movementInput;

    private static readonly int animSpeed = Animator.StringToHash("Speed");
    private static readonly int animJump = Animator.StringToHash("Jump");

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        groundTrigger = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        movementInput = Input.GetAxisRaw("Horizontal");
        shouldJump = Input.GetButton("Jump");
        ChangeSpriteDirection(movementInput < 0);
    }

    private void FixedUpdate()
    {
        isGrounded = groundTrigger.IsTouchingLayers(LayerMask.GetMask("Ground"));
        PlayerWalk();
        PlayerJump();
    }

    private void PlayerWalk()
    {
        anim.SetFloat(animSpeed, Mathf.Abs(body.velocity.x));

        var x = movementInput * speed * Time.fixedDeltaTime;
        body.velocity = new Vector2(x, body.velocity.y);
    }

    private void PlayerJump()
    {
        anim.SetBool(animJump, !isGrounded);

        if (isGrounded && shouldJump)
            body.velocity = new Vector2(body.velocity.x, jumpForce);
    }

    private void ChangeSpriteDirection(bool faceLeft)
        => sprite.flipX = faceLeft;
}
