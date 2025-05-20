using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("[EndGame] OnCollisionEnter2D with: " + collision.gameObject.name + " (tag=" + collision.gameObject.tag + ")");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("[EndGame] Player reached finish, saving high score and loading MainMenu");

            // Save high score if beaten
            int currentScore = ScoreManager.Instance.GetScore();
            int savedHigh   = PlayerPrefs.GetInt("HighScore", 0);
            if (currentScore > savedHigh)
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
                PlayerPrefs.Save();
            }

            // Load Main Menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}
