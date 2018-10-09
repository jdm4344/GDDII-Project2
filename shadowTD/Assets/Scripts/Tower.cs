using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    // Attributes

    // where it is located
    Vector3 position;
    // which way it is facing, normalized
    Vector3 direction;
    // the current target
    Enemy target;
    // the amount of damage it does
    protected int damage;
    // cooldown between shots, in seconds
    protected float cooldown;
    // how much time has passed since the last shot
    float timeSinceLastShot;
    // how far it can shoot enemies at
    protected float range;
    // queue of in range enemies
    List<Enemy> inRangeEnemies;

    // access to the master list of enemies
    EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastShot += Time.deltaTime;

        UpdateInRangeTargets();
        UpdateTarget();
        TrackTarget();
	}

    // updates the list of in range targets
    void UpdateInRangeTargets() {
        Enemy current;
        // loop through all enemies on the field
        for (int i = 0; i < enemyManager.enemyList.Capacity; i++)
        {
            current = enemyManager.enemyList[i];
            // in range and not on the list
            if ((current.position - this.position).magnitude <= range && !inRangeEnemies.Contains(current))
            {
                inRangeEnemies.Add(current);
            }
            // not in range and on the list
            else if ((current.position - this.position).magnitude > range && inRangeEnemies.Contains(current))
            {
                inRangeEnemies.Remove(current);
            }
        }
    }

    // searches for the first vulnerable target in the list of in range targets, sets it to the current target
    void UpdateTarget() {
        for (int i = 0; i < inRangeEnemies.Capacity; i++)
        {
            if (inRangeEnemies[i].isVulnerable)
            {
                target = inRangeEnemies[i];
                return;
            }
        }
        // if there is no valid target
        target = null;
    }

    // get the vector from this tower to the target, set to direction
    void TrackTarget() {
        if (target != null)
        {
            direction = (target.position - this.position).normalized;
        }
        else
        {
            Idle();
        }
    }

    // deal damage to the target
    void Attack() {
        // check for cooldown
        if (timeSinceLastShot >= cooldown)
        {
            timeSinceLastShot = 0.0f;
            target.TakeDamage(damage);
        }
    }

    // where to look if it isn't tracking a target
    void Idle() {
        direction = Vector3.up;
    }
}
