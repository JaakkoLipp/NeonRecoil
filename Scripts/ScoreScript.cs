using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text healthText;
    private float timer = 30f;
    public int score = 0;
    public PlayerHealth playerHealth;
    public int savedPlayerHealth;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        if (playerHealth == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerHealth = playerObject.GetComponent<PlayerHealth>();
            }
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        timerText.text = "Time: " + FormatTime(timer);

        if (playerHealth != null)
        {
            healthText.text = "HP: " + playerHealth.CurrentHealth + "/5";
        }

        if (playerHealth.CurrentHealth == 1)
        {
            healthText.color = Color.red;
        }
        else if (playerHealth.CurrentHealth == 2)
        {
            healthText.color = new Color(1f, 0.5f, 0f);
        } else {
            healthText.color = Color.white;
        }

        if (timer < 5f)
        {
            timerText.color = Color.red;
        }
    
        if (timer <= 0f){
            Debug.Log("Timer ended! Restarting level...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Level transition if all enemies defeated
        if (SceneManager.GetActiveScene().name == "LevelOne" && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Debug.Log("Active Scene: " + SceneManager.GetActiveScene().name);
            if (playerHealth != null)
                {
                    savedPlayerHealth = playerHealth.CurrentHealth;
                }
            LevelLoader loader = FindFirstObjectByType<LevelLoader>();
            if(loader != null)
            {
                Debug.Log("loader exists, moving level");
                loader.TransitionToNextLevel();
            }
        }
        
        // End game and save high score
        if (SceneManager.GetActiveScene().name == "LevelTwo" && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            int currentScore = GetScore();
            int high = PlayerPrefs.GetInt("HighScore", 0);
            if (currentScore > high)
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
                PlayerPrefs.Save();
            }
            Destroy(gameObject);
            SceneManager.LoadScene("MainMenu");
        }
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Enemy Count: " + enemies.Length);
        Debug.Log(SceneManager.GetActiveScene().name);

    }

    // Format time as MM:SS:fff
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time - Mathf.Floor(time)) * 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void AddScoreForEnemyKill()
    {
        Debug.Log("Score added " + score);
        int killScore = Mathf.FloorToInt(100 * timer);
        score += killScore;
        scoreText.text = "Score: " + score;
    }
    public void ResetTimer(){
        timer = 30f;
    }
    public void ResetGame()
    {
        score = 0;
        timer = 30f;
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + FormatTime(timer);
    }

    public int GetScore() => score;

}
