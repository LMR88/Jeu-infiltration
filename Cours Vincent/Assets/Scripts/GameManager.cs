using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    public AudioManager audiomanagerRef;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TooglePause();
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("pas de scene existante");
        }
    }

    private void TooglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            AudioManager.Instance.StopMusic();
            
        }
        else
        {
            Time.timeScale = 1;
            AudioManager.Instance.PlayMusic();
        }
        
    }
}
