using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI1 : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private GameObject fireball;

    public float speed = 3.0f;
    public float obstracleRange = 5.0f;
    public bool _alive;


    private void Start()
    {
        _alive = true;
    }
    private void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab) as GameObject;
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            }
            else if (hit.distance < obstracleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
