using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource _audiosource;

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
}
