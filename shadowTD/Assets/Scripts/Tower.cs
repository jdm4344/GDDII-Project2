using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    // Attributes

    // where it is located
    public Vector3 position;
    // direction to the target
    public Vector3 direction;
    // the current target
    public Enemy target;
    // the amount of damage it does
    protected int damage;
    // cooldown between shots, in seconds
    protected float cooldown;

    // debug to see if it shooting
    bool isShooting;
    // length of burst
    public float burstLength;
    // child object
    Renderer muzzleFlash;

    // how much time has passed since the last shot
    public float timeSinceLastShot;
    // how far it can shoot enemies at
    protected float range;
    // queue of in range enemies
    public List<Enemy> inRangeEnemies = new List<Enemy>();

    // access to the master list of enemies
    public EnemyManager enemyManager;

    protected virtual void Awake()
    {
        enemyManager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
    }
    // Use this for initialization
    protected virtual void Start () {
        position = transform.position;
        direction = Vector3.forward;
        isShooting = false;

        
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        timeSinceLastShot += Time.deltaTime;

        UpdateInRangeTargets();
        UpdateTarget();
        TrackTarget();
	}

    // updates the list of in range targets
    void UpdateInRangeTargets()
    {
        Enemy current;
        // loop through all enemies on the field
        for (int i = 0; i < enemyManager.enemyList.Count; i++)
        {
            current = enemyManager.enemyList[i];
            // in range and not on the list
            if ((current.position - this.position).sqrMagnitude <= range && !inRangeEnemies.Contains(current))
            {
                inRangeEnemies.Add(current);
            }
            // not in range and on the list
            else if ((current.position - this.position).sqrMagnitude > range && inRangeEnemies.Contains(current))
            {
                inRangeEnemies.Remove(current);
            }
        }
        inRangeEnemies.RemoveAll(delegate (Enemy e) { return e == null; });
    }

    // searches for the first vulnerable target in the list of in range targets, sets it to the current target
    void UpdateTarget() {
        for (int i = 0; i < inRangeEnemies.Count; i++)
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
            direction = target.position - position;
            direction.Normalize();
            //float angle = Vector3.Angle(-Vector3.right, direction);
            //transform.Rotate(0f, 0f, direction);
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.right = -direction;
            Attack();
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
            isShooting = true;
        }
    }

    // where to look if it isn't tracking a target
    void Idle() {
        
    }
}
