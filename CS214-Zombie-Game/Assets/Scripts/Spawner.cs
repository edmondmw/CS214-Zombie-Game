using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public GameObject[] spawnLocations;
    public static List<GameObject> Zombies = new List<GameObject>();
    public GameObject zombie;
    public int wave = 1;

    private void Start()
    {
        UpdateText();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        UpdateText();
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
        wave++;
        UpdateText();
        Spawn();
    }

    public void UpdateText()
    {
        if (PlayerController.players.Count > 0)
        {
            for (int i = 0; i < PlayerController.players.Count; i++)
            {
                PlayerController.players[i].UpdateWave(wave);
            }
        }
        else
        {
            Debug.Log("No Player");
        }
    }
}
