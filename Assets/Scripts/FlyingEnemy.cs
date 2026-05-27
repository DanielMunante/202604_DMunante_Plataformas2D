using UnityEngine;

//Enemigo volador que patrulla horizontalmente y hace dano al player
public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float patrolDistance = 3f;
    [SerializeField] AudioClip hitSound;

    Vector3 startPosition;
    bool movingRight = true;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //sin gravedad para que vuele
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        //patrullaje horizontal de un lado al otro
        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        //cambia de direccion al llegar al limite
        float distance = transform.position.x - startPosition.x;
        if (distance >= patrolDistance && movingRight)
        {
            movingRight = false;
            spriteRenderer.flipX = true;
        }
        else if (distance <= -patrolDistance && !movingRight)
        {
            movingRight = true;
            spriteRenderer.flipX = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.isInvincible) return;

            GameManager.Instance.DamagePlayer();
            GameManager.Instance.PlaySFX(hitSound);
        }
    }
}