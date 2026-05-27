using UnityEngine;

public class PowerUpHeart : MonoBehaviour
{
    [SerializeField] AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddLife();
            GameManager.Instance.PlaySFX(pickupSound);
            Destroy(gameObject);
        }
    }
}