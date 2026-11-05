using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_script : MonoBehaviour
{
    public GameObject Levelcompletetext;
    public float moveSpeed = 5f;
    private bool levelFinished = false;

    private void Start()
    {
        if (Levelcompletetext != null)
        {
            Levelcompletetext.SetActive(false);
        }
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird") && !levelFinished)
        {
            levelFinished = true;
            UnlockNewLevel();

            Rigidbody2D birdRb = collision.GetComponent<Rigidbody2D>();
            if (birdRb != null)
            {
                birdRb.linearVelocity = Vector2.zero;
                birdRb.simulated = false;
            }

            Collider2D birdCol = collision.GetComponent<Collider2D>();
            if (birdCol != null)
            {
                birdCol.enabled = false;
            }

            StartCoroutine(LevelClearedRoutine());
        }
    }

    private IEnumerator LevelClearedRoutine()
    {
        if (Levelcompletetext != null)
        {
            Levelcompletetext.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(0.5f);

        if (Scene_manager.instance != null)
        {
            Scene_manager.instance.NextLevel();
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void UnlockNewLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        int currentUnlocked = PlayerPrefs.GetInt("unlockedLevel", 1);

        if (nextLevel > currentUnlocked)
        {
            PlayerPrefs.SetInt("unlockedLevel", nextLevel);
            PlayerPrefs.Save();
        }
    }
}