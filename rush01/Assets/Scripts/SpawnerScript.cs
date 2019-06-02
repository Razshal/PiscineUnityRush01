using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] prefabToSpawn;
    private GameObject actualMob;
    private float timer = 0;
    public float respawnTime = 3f;

    void Update()
    {
        if (!actualMob || actualMob.GetComponent<EnemyScript>().life <= 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            actualMob = Instantiate(prefabToSpawn[Random.Range(0, prefabToSpawn.Length)], transform.position, transform.rotation);
            timer = respawnTime;
        }
    }
}
