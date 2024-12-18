using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    public void Awake()
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

    public void Update()
    {
        UpdateObjectReferences();
    }

    public void PlaySfx(AudioClip audio, bool condition = false)
    {
        if (condition)
            return;
        
        sfxSource.PlayOneShot(audio);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    private void UpdateObjectReferences()
    {
        if (!musicSource)
            musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        if (!sfxSource)
            sfxSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }
}
