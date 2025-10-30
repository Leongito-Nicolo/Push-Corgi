using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource gameSource;
    public AudioSource backgroundSource;

    [Header("Game Audio Clips")]
    public AudioClip winSound;
    public AudioClip menuButtonSound;
    public AudioClip startDragSound;
    public AudioClip rewindSound;

    [Header("Background Audio Clips")]
    public AudioClip mainTrack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);


        PlayBackgroundSound(mainTrack);

    }

    public void PlayGameSound(AudioClip clip)
    {
        gameSource.clip = clip;
        gameSource.Play();
    }

    public void PlayBackgroundSound(AudioClip clip)
    {
        backgroundSource.clip = clip;
        backgroundSource.Play();
    }


}
