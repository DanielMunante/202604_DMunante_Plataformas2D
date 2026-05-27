using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int scoreValue = 10;
    [SerializeField] float rotationSpeed = 90f;

    void Update()
    {
        //rotacion visual para que se vea brillante
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
