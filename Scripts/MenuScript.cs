using UnityEngine;
using UnityEngine.SceneManagement;



// Not implemented


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    void Start()
    {
        pauseMenuUI.SetActive(false); // Hide menu at start
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Press ESC to pause/unpause
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume game
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause game
        isPaused = true;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Reset time before quitting
        Application.Quit();
    }
}
