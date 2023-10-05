using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }

    private void PlayerWalk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0)
        {
            body.velocity = new Vector2(speed, body.velocity.y);
            ChangeDirection(false);
        }
        else if (horizontal < 0)
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
            ChangeDirection(true);
        }
        else
        {
            body.velocity = new Vector2(0f, body.velocity.y);
        }

        anim.SetFloat("Speed", Mathf.Abs(body.velocity.x));
    }

    private void ChangeDirection(bool faceLeft)
    {
        sprite.flipX = faceLeft;
        // Vector3 scale = transform.localScale;
        // scale.x = faceRight ? 1 : -1;
        // transform.localScale = scale;
    }
}
