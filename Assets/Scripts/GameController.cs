using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefabe;
    private GameObject enemy;

    private void Update()
    {
        if(enemy == null)
        {
            enemy = Instantiate(enemyPrefabe) as GameObject;
            enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 369);
            enemy.transform.Rotate(0, angle, 0);
        } 
    }
}
