using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        LoadLevel("LevelOne");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set active scene if not PersistentUI
        if (scene.name != "PersistentUI")
        {
            Debug.Log("Setting active scene to: " + scene.name);
            SceneManager.SetActiveScene(scene);
        }
    }

    public void UnloadLevel(string levelName)
    {
        SceneManager.UnloadSceneAsync(levelName);
    }

    public void TransitionToNextLevel()
    {
        // Only transition if no enemies remain
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if (ScoreManager.Instance.playerHealth != null)
            {
                ScoreManager.Instance.savedPlayerHealth = ScoreManager.Instance.playerHealth.CurrentHealth;
            }
            UnloadLevel("LevelOne");
            LoadLevel("LevelTwo");
            ScoreManager.Instance.ResetTimer();
        }
    }
}
