using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 0f;
    public float MaxLifeTime = 10f;
    public int MaxBounces = 3;

    private int bounces;

    private Rigidbody2D body;
    private Animator anim;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start() => Destroy(gameObject, MaxLifeTime);

    private void FixedUpdate()
    {
        body.velocity = new Vector2(Speed * transform.localScale.x, body.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (++bounces < MaxBounces)
                return;

            anim.Play(animExplode);
            body.simulated = false;
            Destroy(gameObject, .5f);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            anim.Play(animExplode);
            body.simulated = false;

            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            enemy.Die();
            Destroy(gameObject, .5f);
        }
    }

    private static readonly int animExplode = Animator.StringToHash("BulletExplode");
}
