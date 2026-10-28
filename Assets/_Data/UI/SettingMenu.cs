using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : LoadMonoBehaviour
{
    [SerializeField] protected Slider musicSlider;
    [SerializeField] protected Slider soundSlider;

    protected override void OnEnable()
    {
        base.OnEnable();
        musicSlider.value = AudioManger.Instance.GetMusicVolume();
        soundSlider.value = AudioManger.Instance.GetSoundVolume();
    }

    public virtual void SetMusic()
    {
        AudioManger.Instance.SetMusicVolume(musicSlider.value);
    }

    public virtual void SetSound()
    {
        AudioManger.Instance.SetSoundVolume(soundSlider.value);
    }
}
