using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
 public Color gizmoColor = Color.red;
    
    
    public enum SpawnTypes
    {
        Normal,
        Once,
        Wave,
        TimedWave
    }
    public enum EnemyLevels
    {
        Easy,
        Medium,
        Hard,
        Boss
        
    }
    public EnemyLevels enemyLevel = EnemyLevels.Easy;
    
    public GameObject Player;
    public GameObject EasyEnemy;
    public GameObject MediumEnemy;
    public GameObject HardEnemy;
    public GameObject BossEnemy;
    private Dictionary<EnemyLevels, GameObject> Enemies = new Dictionary<EnemyLevels, GameObject>(4);
    
    public int totalEnemy = 10;
    private int numEnemy = 0;
    private int spawnedEnemy = 0;
    
    private int SpawnID;
    
    private bool waveSpawn = false;
    public bool Spawn = true;
    public SpawnTypes spawnType = SpawnTypes.Normal;
    public float waveTimer = 30.0f;
    private float timeTillWave = 0.0f;
    public int totalWaves = 5;
    private int numWaves = 0;
	// Use this for initialization
	void Start () {
        SpawnID = Random.Range(1, 500);
        Enemies.Add(EnemyLevels.Easy, EasyEnemy);
        Enemies.Add(EnemyLevels.Boss, BossEnemy);
		Enemies.Add(EnemyLevels.Medium, MediumEnemy);
        Enemies.Add(EnemyLevels.Hard, HardEnemy);
	}
	void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, new Vector3 (0.5f,0.5f,0.5f));
        
    }
	// Update is called once per frame
	void Update () {
        if(Spawn)
        {
            if (spawnType == SpawnTypes.Normal)
            {
                if(numEnemy < totalEnemy)
                {
                    spawnEnemy();
                }
            }
            else if (spawnType == SpawnTypes.Once)
            {
                if(spawnedEnemy >= totalEnemy)
                {
                    Spawn = false;
                
                }
                else
                {
                    spawnEnemy();
                }
            }
            else if (spawnType == SpawnTypes.Wave)
            {
                if(numWaves < totalWaves + 1)
                {
                    if (waveSpawn)
                    {
                        spawnEnemy();
                    }
                    if (numEnemy == 0)
                    {
                        waveSpawn = true;
                        numWaves++;
                    }
                    if(numEnemy == totalEnemy)
                    {
                        waveSpawn = false;
                    }
                }
            }
            else if(spawnType == SpawnTypes.TimedWave)
            {
                if(numWaves <= totalWaves)
                {
                    timeTillWave += Time.deltaTime;
                    if (waveSpawn)
                    {
                        spawnEnemy();
                    }
                    if (timeTillWave >= waveTimer)
                    {
                        waveSpawn = true;
                        timeTillWave = 0.0f;
                        numWaves++;
                        numEnemy = 0;
                    }
                    if(numEnemy >=totalEnemy)
                    {
                        waveSpawn = false;
                    }
                }
                else
                {
                    Spawn = false;
                }
            }
        }
		
	}
    private void spawnEnemy()
    {
        GameObject Enemy = (GameObject) Instantiate(Enemies[enemyLevel], gameObject.transform.position, Quaternion.identity);
        Enemy.GetComponent<ZombieBehavior> ().player = Player;
        Enemy.SendMessage("Enemy", SpawnID);
        numEnemy++;
        spawnedEnemy++;
        }
    public void killEnemy(int sID)
    {
        if (SpawnID ==sID)
        {
            numEnemy--;
        }
    }
    public void disableSpawner(int sID)
    {
        if(SpawnID == sID)
        {
            Spawn = false;
        }
    }
    public float TimeTillWave
    {
        get
        {
            return timeTillWave;
        }
    }
    public void enableTrigger()
    { 
    Spawn = true;
    }
}
