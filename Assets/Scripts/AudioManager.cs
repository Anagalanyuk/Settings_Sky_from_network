using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioSource _music1Source;
    [SerializeField] private AudioSource _music2Source;

    [SerializeField] private string _introBGMusic;
    [SerializeField] private string _levelBDMusic;

    private AudioSource _activeMusic;
    private AudioSource _inactiveMusic;

    public float crossFadeRate = 1.5f;
    private bool _crossFading;

    private float _musicVolume;
    public float mucicVolume
    {
        get { return _musicVolume; }

        set 
        { 
            _musicVolume = value;
            if(_music1Source != null && !_crossFading)
            {
                _music1Source.volume = _musicVolume;
                _music2Source.volume = _musicVolume;
            }
        }
    }

    public bool _musicMute
    {
        get
        {
            if(_music1Source != null)
            {
                return _music1Source.mute;
            }
            return false;
        }
        set
        {
            if(_music1Source != null)
            {
                _music1Source.mute = value;
                _music2Source.mute = value;
            }
        }
    }


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

        _music1Source.ignoreListenerVolume = true;
        _music2Source.ignoreListenerVolume = true;
        _music1Source.ignoreListenerPause = true;
        _music2Source.ignoreListenerPause = true;

        soundVolume = 1;
        mucicVolume = 1;
        _activeMusic = _music1Source;
        _inactiveMusic = _music2Source;

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
        if (_crossFading) { return; }
        StartCoroutine(CrossFadeMusic(clip));
        //_music1Source.clip = clip;
        //_music1Source.Play();
    }

    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        _crossFading = true;
        _inactiveMusic.clip = clip;
        _inactiveMusic.volume = 0;
        _inactiveMusic.Play();

        float scaleRade = crossFadeRate * _musicVolume;
        while(_activeMusic.volume > 0)
        {
            _activeMusic.volume -= scaleRade * Time.deltaTime;
            _inactiveMusic.volume += scaleRade * Time.deltaTime;

            yield return null;
        }

        AudioSource temp = _activeMusic;
        _activeMusic = _inactiveMusic;
        _activeMusic.volume = _musicVolume;

        _inactiveMusic = temp;
        _inactiveMusic.Stop();
        _crossFading = false;
    }

    public void StopMusic()
    {
        _music1Source.Stop();
        _inactiveMusic.Stop();
    }
}