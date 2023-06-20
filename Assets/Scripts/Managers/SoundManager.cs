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
    [SerializeField] AudioClip purchase;
    [SerializeField] AudioClip reload;
    [SerializeField] AudioClip fire;
    [SerializeField] AudioClip gearPickup;

    [Header("MenuSFX")]
    [SerializeField] AudioClip menuNext;
    [SerializeField] AudioClip menuBack;
    [SerializeField] AudioClip menuPause;

    [Header("Music")]
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip hubMusic;

    private Dictionary<string, AudioClip> SoundList;

    private void Awake()
    {
        SoundList = new Dictionary<string, AudioClip>()
        {
            {"nextLine", nextLine },
            {"purchase", purchase },
            {"reload", reload },
            {"fire", fire },
            {"gearPickup", gearPickup },
            {"menuNext", menuNext },
            {"menuBack", menuBack },
            {"menuPause", menuPause },
            {"menuMusic", menuMusic },
            {"hubMusic", hubMusic },
        };

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
            PlayMusic("menuMusic");
        else
            PlayMusic("hubMusic");
    }

    public void PlaySound(string name, float volumeScale)
    {
        if (_effectSource == null)
        {
            Debug.LogError("Effect source is not assigned.");
            return;
        }
        if (SoundList.ContainsKey(name))
        {
            AudioClip sound = SoundList[name];
            _effectSource.PlayOneShot(sound, volumeScale);
        }
        else
        {
            Debug.LogError("Sound not found: " + name);
        }
    }

    public void PlayMusic(string name)
    {
        if (_musicSource == null)
        {
            Debug.LogError("Music source is not assigned.");
            return;
        }
        if (SoundList.ContainsKey(name))
        {
            AudioClip music = SoundList[name];
            _musicSource.gameObject.GetComponent<AudioSource>().clip = music;
            _musicSource.Play();
        }
        else
        {
            Debug.LogError("Music not found: " + name);
        }
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
