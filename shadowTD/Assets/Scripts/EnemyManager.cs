using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    // attributes

    // master list that contains all current Enemy objects in the scene
    public List<Enemy> enemyList;
    // list of enemies that must be spawned this level
    public List<Enemy> enemySpawnQueue;
    // the max number of enemies that can be in the level at once
    public int maxEnemies;
    // the time in between individual enemy spawns
    public float spawnCooldown;
    // the time since the last enemy spawn
    float lastSpawn;
    // lets us know when all enemies have been defeated
    public bool enemiesDefeated;
    // where to spawn the enemies
    public Vector3 spawnPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // This is here temporarily just so we can have a way to see that the player has won
        // *  
        //if (Input.GetButton("Space")) {
        //    enemiesDefeated = true;
        //}
        // *

        if (!enemiesDefeated) { 
            // update when the last enemy was spawned
            lastSpawn += Time.deltaTime;
            SpawnNextEnemy();
        }
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
            // add to the list
            enemyList.Add(enemySpawnQueue[0]);
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
