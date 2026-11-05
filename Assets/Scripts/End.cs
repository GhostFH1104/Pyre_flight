using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class End : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        Time.timeScale = 1f;

        // Cut background music and sound effects
        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");
        if (audioObj != null)
        {
            Destroy(audioObj);
        }

        // Find the VideoPlayer if not assigned
        if (videoPlayer == null)
        {
            videoPlayer = Object.FindFirstObjectByType<VideoPlayer>();
        }
    }

    private IEnumerator Start()
    {
        // 1. Give the incoming transition animator a brief moment to complete its fade-in
        yield return new WaitForSecondsRealtime(0.5f);

        // 2. Clean up persistent scene manager after transition has completed
        if (Scene_manager.instance != null)
        {
            Destroy(Scene_manager.instance.gameObject);
        }

        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
        if (gameManagerObj != null)
        {
            Destroy(gameManagerObj);
        }

        // 3. Play video routine
        if (videoPlayer != null)
        {
            yield return StartCoroutine(PlayVideoRoutine());
        }
        else
        {
            Debug.LogError("No VideoPlayer found in scene! Loading Menu.");
            yield return StartCoroutine(CleanAndGoToMainMenu());
        }
    }

    private IEnumerator PlayVideoRoutine()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.Play();

        // Wait until the video has genuinely advanced past the first frames
        // (prevents frame-0 timestamp glitch from exiting immediately)
        while (videoPlayer.time < 0.1)
        {
            yield return null;
        }

        // Wait for the full length of the video
        double duration = videoPlayer.length;
        if (duration > 0)
        {
            yield return new WaitForSecondsRealtime((float)(duration - videoPlayer.time));
        }
        else
        {
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }
        }

        yield return StartCoroutine(CleanAndGoToMainMenu());
    }

    private IEnumerator CleanAndGoToMainMenu()
    {
        AsyncOperation unload = Resources.UnloadUnusedAssets();
        while (!unload.isDone)
        {
            yield return null;
        }
        System.GC.Collect();

        SceneManager.LoadScene(0);
    }
}