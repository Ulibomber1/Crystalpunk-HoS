using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectSource;
    [SerializeField] List<AudioClip> dialogueClips = new List<AudioClip>();
    [SerializeField] AudioClip nextLine;
    //[SerializeField] AudioClip menuNext;
    //[SerializeField] AudioClip menuBack;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

    public void PlayDialogueSFX()
    {
        AudioClip clip = dialogueClips[Random.Range(0, dialogueClips.Count)];

        _effectSource.PlayOneShot(clip, 0.3f);
    }

    public void PlayNextLine()
    {
        _effectSource.PlayOneShot(nextLine, 0.4f);
    }
}
