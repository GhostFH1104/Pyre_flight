using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu_links : MonoBehaviour
{
    public void playGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitgame()
    {
        Application.Quit();
    }
}