using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This is just for testing.
public class GameManagerSingle : MonoBehaviour
{
    public Transform[] enemySpawns;
    public GameObject enemy;
    public int waveSize = 2;
    int numEnemies = 0;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        if(numEnemies == 0)
        {
            StartCoroutine(SpawnWaves());
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
        for(int i=0;i<15;i++){
        enemy = ObjectPooler.SharedInstance.GetPooledObject("Enemy"); 
        if (enemy != null) {
            enemy.transform.position = enemySpawns[0].transform.position;
            enemy.transform.rotation = enemySpawns[0].transform.rotation;
            enemy.GetComponent<ZombieHealth> ().currentHealth = enemy.GetComponent<ZombieHealth> ().startingHealth;
            enemy.SetActive (true);
            }
        }
        //numEnemies = waveSize;
        yield return null;
        //yield return new WaitForSeconds(10f);

        /*for(int i = 0, j = 0; i < waveSize; ++i, ++j)
        {
            // reset to 0th spawn index
            if (j == enemySpawns.Length)
                j = 0;
            PhotonNetwork.Instantiate(enemy.name, enemySpawns[j].position, enemySpawns[j].rotation, 0);

        }
        waveSize *= 2;*/
    }


    /*IEnumerator RestartLevel()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.connected)
            yield return null;
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }*/

    public void decrementNumEnemies()
    {
        numEnemies--;
    }

    /*public void decrementNumPlayers()
    {
        numPlayers--;
    }

    void UpdateNumPlayers()
    {
        numPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
    }*/
}
