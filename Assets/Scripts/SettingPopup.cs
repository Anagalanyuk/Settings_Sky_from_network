using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider SpeedSlider;

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
    }

    public void OnSoundValue(float value)
    {
        Managers.Audio.soundVolume = value;
    }
}
