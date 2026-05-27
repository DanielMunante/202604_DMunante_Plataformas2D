using System.Collections;
using UnityEngine;

public class PowerUpInvincible : MonoBehaviour
{
    [SerializeField] float duration = 5f;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] Color invincibleColor = Color.yellow;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer playerSprite = other.GetComponent<SpriteRenderer>();
            StartCoroutine(InvincibleRoutine(playerSprite));
            GameManager.Instance.PlaySFX(pickupSound);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, duration);
        }
    }

    IEnumerator InvincibleRoutine(SpriteRenderer playerSprite)
    {
        Color originalColor = playerSprite.color;
        GameManager.Instance.SetInvincible(true);
        playerSprite.color = invincibleColor;

        yield return new WaitForSeconds(duration);

        GameManager.Instance.SetInvincible(false);
        playerSprite.color = originalColor;
    }
}