using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    // attributes

    int currentWave;
    // how many enemies are in the largest wave
    public int waveSize;
    // number of waves
    public int waves;
    // char array of the enemies to spawn
    public char[,] enemySpawnArray;

    // master list that contains all current Enemy objects in the scene
    public List<Enemy> enemyList;
    // list of enemies that must be spawned this wave
    public List<Enemy> enemySpawnQueue;

    // the max number of enemies that can be in the wave at once
    public int maxEnemies;
    // the time in between individual enemy spawns
    public float spawnCooldown;
    // the time since the last enemy spawn
    float lastSpawn;
    // where to spawn the enemies
    public Vector3 spawnPoint;

    // total enemies in the wave
    int totalEnemies;
    // lets us know when all enemies have been defeated
    int enemiesDefeated;
    
    GameManager gameManager;

	// Use this for initialization
	void Start ()
    {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentWave = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        lastSpawn += Time.deltaTime;
        SpawnNextEnemy();
        CheckWaveComplete();
	}

    // checks if all enemies are gone
    void CheckWaveComplete()
    {
        if (enemySpawnQueue.Count == 0 && enemyList.Count == 0)
        {
            SetupWave();
        }
    }

    void SetupWave()
    {
        currentWave++;
        if (currentWave > waves)
        {
            // This is when all waves have been defeated, end the game
            Debug.Log("Game Over You Win");
        }
        else
        {
            for (int i = 0; i < waveSize; i++)
            {
                switch (enemySpawnArray[currentWave - 1, i])
                {
                    case 'v':
                        enemySpawnQueue.Add(gameManager.vampirePrefab);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // spawns the next enemy at the spawnPoint once the cooldown is over
    void SpawnNextEnemy()
    {
        // check the cooldown and if all enemies have been spawned
        if (lastSpawn >= spawnCooldown && enemySpawnQueue.Count != 0 && enemyList.Count < maxEnemies)
        {
            // instantiate the first enemy on the queue
            
            // add to the list
            enemyList.Add(Instantiate(enemySpawnQueue[0], spawnPoint, Quaternion.identity));
            // remove it from the queue
            enemySpawnQueue.RemoveAt(0);
            
            // reset cooldown
            lastSpawn = 0.0f;
        }
    }

    // removes an enemy from the master list, to be called by the enemy when it is destroyed
    public void RemoveEnemy(Enemy enemy)
    {
        if (enemyList.Contains(enemy))
        {
            // adds to total funds
            gameManager.AddFunds(enemy.reward);
            // remove from master list
            enemyList.Remove(enemy);
        }
    }
}
