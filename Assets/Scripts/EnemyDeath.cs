using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] int scoreOnKill = 50;
    [SerializeField] AudioClip deathSound;

    Life life;

    void Awake()
    {
        life = GetComponent<Life>();
    }

    void OnEnable()
    {
        if (life != null)
        {
            life.onLifeDepleted.AddListener(OnLifeDepleted);
        }
    }

    void OnDisable()
    {
        if (life != null)
        {
            life.onLifeDepleted.RemoveListener(OnLifeDepleted);
        }
    }

    void OnLifeDepleted(float startLife)
    {
        Die();
    }

    //llamado cuando el player invencible toca al enemigo
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.isInvincible)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.AddScore(scoreOnKill);
        GameManager.Instance.PlaySFX(deathSound);
        Destroy(gameObject);
    }
}