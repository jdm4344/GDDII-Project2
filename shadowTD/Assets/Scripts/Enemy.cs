using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Attributes

    // hit points
    public int health;
    // where it is located
    public Vector3 position;
    // which way it is facing, normalized
    public Vector3 direction;
    // how fast it can move
    public float maxSpeed;
    // can it take damage
    public bool isVulnerable;

    // access to the EnemyManager
    public EnemyManager enemyManager;

    protected virtual void Awake()
    {
        enemyManager = GameObject.Find("GameManager_Empty").GetComponent<EnemyManager>();
    }

    // Use this for initialization
    protected virtual void Start () {
        position = transform.position;
        direction = transform.forward;
        //Debug.Log("position set " + transform.position);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        
        Move();
	}

    // handles subtracting from health and 'killing off' the emeny, called by the Towers
    public void TakeDamage(int amount) {
        // subtract from health
        health -= amount;
        // kill it
        if (health <= 0)
        {
            DestroySelf();
        }
    }

    // removes itself from the EnemyManager's master list and deletes itself
    private void DestroySelf() {
        enemyManager.RemoveEnemy(this);
        Destroy(this.gameObject);
    }

    // called every frame, advances the Enemy toward the Base
    private void Move() {
        // position
        position += direction * maxSpeed * Time.deltaTime;
        transform.position = position;

        // direction
    }
}
