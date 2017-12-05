using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] enemySpawns;
    public GameObject enemy;
    int waveSize = 0;
    int numEnemies = 0;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(!PhotonNetwork.isMasterClient)
        {
            return;
        }

        if(numEnemies == 0)
        {
            SpawnWaves();
            waveSize *= 2;
        }
	}

    void SpawnWaves()
    {
        for(int i = 0, j = 0; i < waveSize; ++i, ++j)
        {
            if (j == enemySpawns.Length)
                j = 0;
            PhotonNetwork.Instantiate(enemy.name, enemySpawns[j].position, enemySpawns[j].rotation, 0);
        }

        numEnemies = waveSize;
    }
}
