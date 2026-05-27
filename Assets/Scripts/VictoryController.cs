using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    void Start()
    {
        //puntuacion final
        scoreText.text = "Puntuacion final: " + GameManager.Instance.score;
    }

    public void GoToMenu()
    {
        GameManager.Instance.lives = 3;
        GameManager.Instance.score = 0;
        SceneManager.LoadScene("MainMenu");
    }
}