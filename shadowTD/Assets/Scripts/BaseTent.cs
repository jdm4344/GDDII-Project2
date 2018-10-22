using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTent : MonoBehaviour {

    public int health = 5;

    EnemyManager enemyManager;

    void Awake()
    {
        enemyManager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
    }

	// Use this for initialization
	void Start () {
        health = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckEnemies();	
	}

    void CheckEnemies()
    {
        for (int i = 0; i < enemyManager.enemyList.Count; i++)
        {
            if ((enemyManager.enemyList[i].position - transform.position).sqrMagnitude < .25f * .25f)
            {
                // base takes damage
                TakeDamage();
                // kill the enemy upon touching the base
                enemyManager.enemyList[i].TakeDamage(999);
            }          
        }
    }

    void TakeDamage()
    {
        health -= 1;
        if (health >= 0)
        {
            // End the game
            Debug.Log("Game Over you lose");
        }
    }
}
