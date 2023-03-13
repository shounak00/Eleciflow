using UnityEngine;

public enum AudioType
{
    nodeDropped,
    levelCompleted
}

public class SoundManager : MonoBehaviour
{
    [Header("Audio sources")]
    public AudioSource audioSource;
    public AudioSource backgroundAudioSource;

    [Header("Audios")]
    public AudioClip nodeDropped;
    public AudioClip levelCompleted;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("isMusic", 1) == 1)
        {
            backgroundAudioSource.Play();
        }
    }

    private void playAudio(AudioType audioType)
    {
        if (audioSource.clip == levelCompleted)
        {
            if (audioSource.isPlaying)
            {
                return;
            }
        }
        switch (audioType)
        {
            case AudioType.nodeDropped:
                audioSource.clip = nodeDropped;
                break;
            case AudioType.levelCompleted:
                audioSource.clip = levelCompleted;
                break;
            default:
                break;
        }
        if (PlayerPrefs.GetInt("isSound", 1) == 1)
        {
            audioSource.Play();
        }
    }

    public void eventNodeDropped()
    {
        playAudio(AudioType.nodeDropped);
    }

    public void eventLevelCompleted()
    {
        playAudio(AudioType.levelCompleted);
    }

}