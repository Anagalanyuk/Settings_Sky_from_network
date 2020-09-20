using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private NetworkService _network;

    public float cloud_value { get; private set; }

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");

        _network = service;

         StartCoroutine(_network.GetWeatherXml(OnXmlDataLoaded));
        //StartCoroutine(_network.GetWeatherJson(OnJsonDataLoaded));

        status = ManagerStatus.Initializing;
    }

    private void OnJsonDataLoaded(string data)
    {
        Dictionary<string, object> dict;
        dict = Json.Deserialize(data) as Dictionary<string, object>;

        Dictionary<string, object> clouds = (Dictionary<string, object>)dict["clouds"];
        cloud_value = (long)clouds["all"] / 100f;
        Debug.Log("value: " + cloud_value);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATE);

        status = ManagerStatus.Started;
    }

    public void OnXmlDataLoaded(string data)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data);
        XmlNode root = doc.DocumentElement;

        XmlNode node = root.SelectSingleNode("clouds");
        string value = node.Attributes["value"].Value;
        cloud_value = Convert.ToInt32(value) / 100f;
        Debug.Log("value: " + cloud_value);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATE);
        status = ManagerStatus.Started;
    }
}