using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text livesText;
    [SerializeField] GameObject gameOverPanel;

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            GameManager.Instance.RegisterGameOverPanel(gameOverPanel);
        }
        Refresh();
    }

    public void Refresh()
    {
        scoreText.text = "Score: " + GameManager.Instance.score;
        livesText.text = "Vidas: " + GameManager.Instance.lives;
    }
}