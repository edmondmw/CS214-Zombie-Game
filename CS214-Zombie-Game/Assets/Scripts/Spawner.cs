using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] spawnLocations;
    public GameObject zombie;
    public int wave = 1;
    int i = 0;
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float spawnTimer;
        if (1 - wave * 0.1f > 0.1f)
        {
            spawnTimer = 2 - wave * 0.1f;
        }
        else
        {
            spawnTimer = 0.1f;
        }
        for (int i = 0; i < wave * 10; i++)
        {
            Instantiate(zombie, spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
