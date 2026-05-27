using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.lives = 3;
        GameManager.Instance.score = 0;
        SceneManager.LoadScene("Level1");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.lives = 3;
        GameManager.Instance.score = 0;
        SceneManager.LoadScene("MainMenu");
    }
}