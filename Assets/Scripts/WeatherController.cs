using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private Material sky;
    [SerializeField] private Light sun;

    private float _fullIntensity;

    // private float _cloudValue = 0f;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATE, OnWeatherUpdate);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATE, OnWeatherUpdate);
    }

    private void OnWeatherUpdate()
    {
        SetOverCast(Managers.Weather.cloud_value);
    }

    void Start()
    {
        _fullIntensity = sun.intensity;
    }

    private void SetOverCast(float value)
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}