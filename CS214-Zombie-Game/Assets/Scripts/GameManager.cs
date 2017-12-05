using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform[] enemySpawns;
    public GameObject enemy;
    public int waveSize = 1;
    int numEnemies = 0;
    int numPlayers = 1;

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
            StartCoroutine(SpawnWaves());
            waveSize *= 2;
            numEnemies = waveSize;
        }

        /* Not working
        UpdateNumPlayers();

        if(numPlayers == 0)
        {
            StartCoroutine(RestartLevel());
        }
        
         */
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(10f);

        for(int i = 0, j = 0; i < waveSize; ++i, ++j)
        {
            // reset to 0th spawn index
            if (j == enemySpawns.Length)
                j = 0;
            PhotonNetwork.Instantiate(enemy.name, enemySpawns[j].position, enemySpawns[j].rotation, 0);
        }
    }


    IEnumerator RestartLevel()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.connected)
            yield return null;
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void decrementNumEnemies()
    {
        numEnemies--;
    }

    public void decrementNumPlayers()
    {
        numPlayers--;
    }

    void UpdateNumPlayers()
    {
        numPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
    }
}
