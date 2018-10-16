using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Attributes

    // where it is located
    public Vector3 position;
    // which way it is facing, normalized
    public Vector3 direction;

    // hit points
    public int health;
    // how fast it can move
    public float maxSpeed;
    // can it take damage
    public bool isVulnerable;
    // money rewarded on kill
    public int reward;

    // grid to use for pathfinding
    char[,] gridTerrain;

    // access to Managers
    public GameGrid gameGrid;
    public EnemyManager enemyManager;

    protected virtual void Awake()
    {
        gameGrid = GameObject.Find("GameGrid(Clone)").GetComponent<GameGrid>();
        enemyManager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
    }

    // Use this for initialization
    protected virtual void Start()
    {
        gridTerrain = gameGrid.dataGrid;

        position = transform.position;
        direction = transform.forward;
	}
	
	// Update is called once per frame
	protected virtual void Update()
    {
        Move();
	}

    public void MakeVulnerable()
    {
        isVulnerable = true;
    }

    public void MakeInvulnerable()
    {
        isVulnerable = false;
    }

    // handles subtracting from health and 'killing off' the emeny, called by the Towers
    public void TakeDamage(int amount)
    {
        // subtract from health
        health -= amount;
        // kill it
        if (health <= 0)
        {
            DestroySelf();
        }
    }

    // removes itself from the EnemyManager's master list and deletes itself
    private void DestroySelf()
    {
        enemyManager.RemoveEnemy(this);
        Destroy(this.gameObject);
    }

    // called every frame, advances the Enemy toward the Base
    private void Move()
    {
        // position
        position += direction * maxSpeed * Time.deltaTime;
        transform.position = position;

        // direction
        
        transform.right = direction;
    }
}
