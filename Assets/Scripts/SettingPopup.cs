using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider SpeedSlider;
    [SerializeField] private AudioClip _sound;

    private void Start()
    {
        SpeedSlider.value = PlayerPrefs.GetFloat("Speed_Slider", 1);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnSliderValue(float value)
    {
        volumeSlider.value = value;
    }

    public void OnSpeedValue(float speed)
    {
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }

    public void OnSoundToggle()
    {
        Managers.Audio.soundMute = !Managers.Audio.soundMute;
        Managers.Audio.PlaySound(_sound);
    }

    public void OnSoundValue(float value)
    {
        Managers.Audio.soundVolume = value;
    }

    public void OnPlayMusic(int selector)
    {
        switch (selector)
        {
            case 1:
                Managers.Audio.PlayIntroMusic();
                break;
            case 2:
                Managers.Audio.PlayLevelMucic();
                break;
            default:
                Managers.Audio.StopMusic();
                break;
        }
    }
}
