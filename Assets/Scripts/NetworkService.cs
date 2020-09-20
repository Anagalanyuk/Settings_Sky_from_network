using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&APPID=9094b2f987770e9d07f2fa80dedf09dd";
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&APPID=9094b2f987770e9d07f2fa80dedf09d";
    private const string webImage = "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

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

    public IEnumerator DownLoadImage(Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.Send();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}