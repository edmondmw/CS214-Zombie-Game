using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public GameObject[] spawnLocations;
    public static int numZombies;
    public GameObject zombie;
    public int wave = 1;
    public bool gameOver;

    private void Start()
    {
		if (GameMode.isSinglePlayer || PhotonNetwork.isMasterClient) 
		{
			UpdateText ();
			StartCoroutine (Spawn ());
		}
    }

    IEnumerator Spawn()
    {
		while (true) 
		{
			UpdateText ();
			float spawnTimer;
			if (1 - wave * 0.1f > 0.1f) {
				spawnTimer = 2 - wave * 0.1f;
			} else {
				spawnTimer = 0.1f;
			}
			yield return new WaitForSeconds (4);
			for (int i = 0; i < wave * 5 + 1; i++) {
				if (PhotonNetwork.connected && PhotonNetwork.isMasterClient) {
					PhotonNetwork.Instantiate (zombie.name, spawnLocations [Random.Range (0, spawnLocations.Length)].transform.position, Quaternion.identity, 0);
				} else {
					Instantiate (zombie, spawnLocations [Random.Range (0, spawnLocations.Length)].transform.position, Quaternion.identity);
				}
				numZombies++;
				yield return new WaitForSeconds (spawnTimer);
			}
			while (numZombies > 0) {
				yield return new WaitForSeconds (1);
			}
			wave++;
		}
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
