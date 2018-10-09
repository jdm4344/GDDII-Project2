using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    // attributes

    // master list that contains all current Enemy objects in the scene
    List<Enemy> enemyList;
    // list of enemies that must be spawned this level
    List<Enemy> enemySpawnQueue;
    // the max number of enemies that can be in the level at once
    int maxEnemies;
    // the time in between individual enemy spawns
    float spawnCooldown;
    // the time since the last enemy spawn
    float lastSpawn;
    // where to spawn the enemies
    Vector3 spawnPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // update when the last enemy was spawned
        lastSpawn += Time.deltaTime;
        SpawnNextEnemy();

	}

    // spawns the next enemy at the spawnPoint once the cooldown is over
    void SpawnNextEnemy() {
        // check the cooldown and if all enemies have been spawned
        if (lastSpawn >= spawnCooldown && enemySpawnQueue.Count != 0)
        {
            // instantiate the first enemy on the queue
            Instantiate(enemySpawnQueue[0], spawnPoint, Quaternion.identity);
            // remove it from the queue
            enemySpawnQueue.RemoveAt(0);
            // reset cooldown
            lastSpawn = 0.0f;
        }
    }

    // removes an enemy from the master list, to be called by the enemy when it is destroyed
    public void RemoveEnemy(Enemy enemy) {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }
}
