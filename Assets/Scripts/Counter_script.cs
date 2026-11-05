using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counter_script : MonoBehaviour
{
    public int Counter = 0;
    public int coin = 0;

    public Text Countertext;
    public Text coinText;
    public GameObject Gameovertext;
    public Bird_script bird;

    private Audio_manager audioManager;
    private bool isGameOver = false;

    private void Awake()
    {
        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");
        if (audioObj != null)
        {
            audioManager = audioObj.GetComponent<Audio_manager>();
        }
    }

    public void addscore()
    {
        if (isGameOver) return;
        Counter++;
        if (Countertext != null)
        {
            Countertext.text = "Score: " + Counter;
        }
    }

    public void addcoin()
    {
        if (isGameOver) return;
        coin++;
        if (coinText != null)
        {
            coinText.text = "Coin: " + coin;
        }
    }

    public void reset()
    {
        if (audioManager != null)
        {
            audioManager.StopSFX();
            audioManager.PlayBackgroundMusic();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Gameover()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (audioManager != null)
        {
            audioManager.GameOverMusic();
        }

        if (Gameovertext != null)
        {
            Gameovertext.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}