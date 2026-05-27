using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] bool isFinalLevel = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFinalLevel)
            {
                GameManager.Instance.Victory();
            }
            else
            {
                GameManager.Instance.LoadNextLevel();
            }
        }
    }
}