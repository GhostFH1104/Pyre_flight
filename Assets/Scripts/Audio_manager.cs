using UnityEngine;

public class Audio_manager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip flap;
    public AudioClip gamend;

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && background != null)
        {
            musicSource.clip = background;
            musicSource.loop = true;
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }

    public void PauseMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
        }
    }

    public void RestartBackgroundMusic()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
            musicSource.Stop();
            if (background != null)
            {
                musicSource.clip = background;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource != null && clip != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    public void GameOverMusic()
    {
        if (musicSource != null)
        {
            musicSource.loop = false;
            musicSource.Stop();
        }

        if (SFXSource != null && gamend != null)
        {
            SFXSource.ignoreListenerPause = true;
            SFXSource.PlayOneShot(gamend);
        }
    }

    public void StopSFX()
    {
        if (SFXSource != null)
        {
            SFXSource.ignoreListenerPause = false;
            SFXSource.Stop();
        }
    }
}