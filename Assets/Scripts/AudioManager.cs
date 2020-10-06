using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioSource _music1Source;

    [SerializeField] private string _introBGMusic;
    [SerializeField] private string _levelBDMusic;

    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public ManagerStatus status { get; private set; }

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Audio manager setting...");

        _network = service;

        soundVolume = 1;

        status = ManagerStatus.Started;
    }

    public void PlaySound(AudioClip clip)
    {
        _audiosource.PlayOneShot(clip);
    }

    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load("Music/" + _introBGMusic) as AudioClip);
    }

    public void PlayLevelMucic()
    {
        PlayMusic(Resources.Load("Music/" + _levelBDMusic) as AudioClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        _music1Source.clip = clip;
        _music1Source.Play();
    }

    public void StopMusic()
    {
        _music1Source.Stop();
    }
}
