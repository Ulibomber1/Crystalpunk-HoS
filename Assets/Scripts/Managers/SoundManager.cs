using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectSource;

    [Header("SFX")]
    [SerializeField] List<AudioClip> dialogueClips = new List<AudioClip>();
    [SerializeField] AudioClip nextLine;
    [SerializeField] public AudioClip purchase; //i feel like theres probably a better way then having all the sounds be public but it saves a lot of extra code passing around sounds
    [SerializeField] public AudioClip reload;
    [SerializeField] public AudioClip fire;
    [SerializeField] public AudioClip gearPickup;
    [SerializeField] public AudioClip menuNext;
    [SerializeField] public AudioClip menuBack;

    [Header("Music")]
    [SerializeField] public AudioClip menuMusic;
    [SerializeField] public AudioClip hubMusic;


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

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Start")
            PlayMusic(menuMusic);
        else
            PlayMusic(hubMusic);
    }

    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip music)
    {
        _musicSource.gameObject.GetComponent<AudioSource>().clip = music;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
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
