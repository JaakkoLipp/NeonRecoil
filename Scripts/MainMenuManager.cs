using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text highScoreText;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    public void StartGame()
    {
        
        SceneManager.LoadScene("PersistentUI");
    }

    public void Options()
    {
        Debug.Log("Options menu not implemented yet.");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
