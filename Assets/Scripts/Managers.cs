using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeatherManager))]
public class Managers : MonoBehaviour
{
    public static WeatherManager Weather { get; private set; }

    private List<IGameManager> _startSequence;

    private void Awake()
    {
        Weather = GetComponent<WeatherManager>();
        _startSequence = new List<IGameManager>();
        _startSequence.Add(Weather);

        StartCoroutine(StartupManager());
    }

    private IEnumerator StartupManager()
    {
        NetworkService network = new NetworkService();

        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup(network);
        }

        yield return null;

        int numModels = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModels)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }


                if (numReady > lastReady)
                {
                    Debug.Log("Progress: " + numReady + "/" + numModels);
                }
                yield return null;
            }
            Debug.Log("All managers started up.");
        }
    }
}