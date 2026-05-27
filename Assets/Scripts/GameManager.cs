using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int startLives = 3;
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip victorySound;

    public int score;
    public int lives;
    public bool isInvincible; 
    GameObject gameOverPanel;

    AudioSource musicSource;
    AudioSource sfxSource;
    HudController hud;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudio();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        lives = startLives;
        score = 0;
    }

    void SetupAudio()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.5f;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.volume = 0.8f;

        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ConnectHud());
    }

    System.Collections.IEnumerator ConnectHud()
    {
        yield return null;

        hud = FindFirstObjectByType<HudController>();
        RefreshHud();
    }

    public void AddScore(int amount)
    {
        score += amount;
        PlaySFX(coinSound);
        RefreshHud();
    }

    public void AddLife()
    {
        lives++;
        RefreshHud();
    }

    public void DamagePlayer()
    {
        if (isInvincible) return;

        lives--;
        RefreshHud();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void KillPlayer()
    {
        if (isInvincible) return;

        lives = 0;
        RefreshHud();
        GameOver();
    }

    public void SetInvincible(bool state)
    {
        isInvincible = state;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void LoadNextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(next);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        yield return null;
    }

    public void Victory()
    {
        PlaySFX(victorySound);
        musicSource.Stop();
        SceneManager.LoadScene("Victory");
    }

    void RefreshHud()
    {
        if (hud != null) hud.Refresh();
    }

    public void RegisterGameOverPanel(GameObject panel)
    {
        gameOverPanel = panel;
    }

}