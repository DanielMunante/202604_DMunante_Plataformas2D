using System.Collections;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 2f;
    [SerializeField] float duration = 5f;
    [SerializeField] AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController2D controller = other.GetComponent<CharacterController2D>();
            controller.StartCoroutine(SpeedRoutine(controller));
            GameManager.Instance.PlaySFX(pickupSound);

            //escondemos el powerup mientras dura el efecto
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, duration);
        }
    }

    IEnumerator SpeedRoutine(CharacterController2D controller)
    {
        controller.SetSpeedMultiplier(speedMultiplier);
        yield return new WaitForSeconds(duration);
        controller.ResetSpeed();
    }
}