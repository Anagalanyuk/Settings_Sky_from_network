using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&APPID=9094b2f987770e9d07f2fa80dedf09dd";
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&APPID=9094b2f987770e9d07f2fa80dedf09d";
    private IEnumerator CallApi(string url, Action<string> callBack)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.Send();

            if (request.isNetworkError)
            {
                Debug.LogError("network problem" + request.error);
            }
            else if (request.responseCode != (long)System.Net.HttpStatusCode.OK)
            {
                Debug.LogError("response erro" + request.responseCode);
            }
            else
            {
                callBack(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXml(Action<string> callback)
    {
        return CallApi(xmlApi, callback);
    }

    public IEnumerator GetWeatherJson(Action<string> callback)
    {
        return CallApi(jsonApi, callback);
    }
}
