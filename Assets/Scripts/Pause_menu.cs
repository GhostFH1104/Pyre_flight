using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_menu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private Audio_manager audioManager;

    private void Awake()
    {
        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");
        if (audioObj != null)
        {
            audioManager = audioObj.GetComponent<Audio_manager>();
        }
    }

    public void pause()
    {
        if (pauseMenu != null) pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        if (audioManager != null)
        {
            audioManager.PauseMusic();
        }
    }

    public void resume()
    {
        if (pauseMenu != null) pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        if (audioManager != null)
        {
            audioManager.ResumeMusic();
        }
    }

    public void restart()
    {
        Time.timeScale = 1f;
        if (audioManager != null)
        {
            audioManager.RestartBackgroundMusic();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void home()
    {
        Time.timeScale = 1f;
        if (audioManager != null)
        {
            audioManager.RestartBackgroundMusic();
        }
        SceneManager.LoadScene(0);
    }
}