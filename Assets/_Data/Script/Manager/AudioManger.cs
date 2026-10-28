using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManger : LoadMonoBehaviour
{
    private static AudioManger instance;
    public static AudioManger Instance => instance;

    [Header("Audio Sources")]
    [SerializeField] protected AudioSource audioMusic;
    [SerializeField] protected AudioSource audioSound;

    [Header("Audio Mixer")]
    [SerializeField] protected AudioMixer myMixer;

    [Header("Audio Clips")]
    [SerializeField] protected AudioClip background1;
    [SerializeField] protected AudioClip coinCollect;
    [SerializeField] protected AudioClip coinDrop;
    [SerializeField] protected AudioClip buttonClick;
    [SerializeField] protected AudioClip cut;
    [SerializeField] protected AudioClip bell;
    [SerializeField] protected AudioClip meow;
    [SerializeField] protected AudioClip open;

    protected override void Awake()
    {
        base.Awake();
        if (AudioManger.instance != null && AudioManger.instance != this)
        {
            Destroy(gameObject);
            return;
        }

        AudioManger.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.InitVolume();

        this.audioMusic.clip = background1;
        this.audioMusic.Play();
    }

    protected virtual void InitVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume(1f);
            SetSoundVolume(1f);
        }
    }

    public virtual void SetMusicVolume(float volume)
    {
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public virtual void SetSoundVolume(float volume)
    {
        myMixer.SetFloat("Sound", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    protected virtual void LoadVolume()
    {
        Debug.Log("LoadVolume");
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);

        this.SetMusicVolume(musicVolume);
        this.SetSoundVolume(soundVolume);
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("SoundVolume", 1f);
    }

    //protected virtual void LoadPointSound()
    //{
    //    if (this.pointSound != null) return;
    //    this.pointSound = Resources.Load<AudioClip>("Audio/point");
    //    Debug.LogWarning(transform.name + ": LoadPointSound", gameObject);
    //}

    public void PlayCoinCollect()
    {
        this.audioSound.PlayOneShot(this.coinCollect);
    }

    public void PlayCoinDrop()
    {
        this.audioSound.PlayOneShot(this.coinDrop);
    }

    public void PlayButtonClick()
    {
        this.audioSound.PlayOneShot(this.buttonClick);
    }

    public void PlayBell()
    {
        this.audioSound.PlayOneShot(this.bell);
    }

    public void PlayMeow()
    {
        this.audioSound.PlayOneShot(this.meow);
    }

    public void PlayOpen()
    {
        this.audioSound.PlayOneShot(this.open);
    }


}
