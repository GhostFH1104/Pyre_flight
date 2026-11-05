using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_manager : MonoBehaviour
{
    public static Scene_manager instance;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private float transitionDuration = 1f;

    private static readonly int EndTrigger = Animator.StringToHash("End");
    private static readonly int StartTrigger = Animator.StringToHash("Start");

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        Time.timeScale = 1f;

        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger(EndTrigger);
            yield return new WaitForSecondsRealtime(transitionDuration);
        }

        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");
        if (audioObj != null)
        {
            Audio_manager audioMan = audioObj.GetComponent<Audio_manager>();
            if (audioMan != null)
            {
                audioMan.StopSFX();
            }
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger(StartTrigger);
        }
    }
}