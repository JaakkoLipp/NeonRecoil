using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    void Start()
    {
        if (ScoreManager.Instance != null && ScoreManager.Instance.savedPlayerHealth > 0)
        {
            currentHealth = ScoreManager.Instance.savedPlayerHealth;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    public int CurrentHealth { get { return currentHealth; } }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }
    void Die()
    {
        // Save high score if beaten
        int currentScore = ScoreManager.Instance.GetScore();
        int savedHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > savedHigh)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }

        Destroy(ScoreManager.Instance.gameObject);

        // Load Main Menu
        SceneManager.LoadScene("MainMenu");
    }
}
