using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] AudioClip damageSound;

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.DamagePlayer();
            GameManager.Instance.PlaySFX(damageSound);

            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 8f);            
        }
    }
}