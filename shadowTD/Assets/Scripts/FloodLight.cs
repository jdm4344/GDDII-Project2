using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodLight : Tower {

    public GameObject lightbeam;
    public GameObject lightFixtureRotator;

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        // have access to the child parts of the prefab
        lightFixtureRotator = transform.GetChild(0).GetChild(0).gameObject;
        lightbeam = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        range = 4.0f;
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        timeSinceLastShot += Time.deltaTime;

        UpdateInRangeTargets();
        UpdateTarget();
        FloodLightTracking();
        ExposeEnemy();
    }

    new protected void UpdateInRangeTargets()
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
                current.isVulnerable = false;
                inRangeEnemies.Remove(current);
            }
        }
        inRangeEnemies.RemoveAll(delegate (Enemy e) { return e == null; });
    }

    new protected void UpdateTarget()
    {
        if (inRangeEnemies.Count > 0)
        {
            target = inRangeEnemies[0];
        }
        else
        {
            target = null;
        }
        // if there is no valid target
    }

    void FloodLightTracking()
    {
        if (target != null)
        {
            direction = target.position - lightFixtureRotator.transform.position;
            direction.Normalize();
            //float angle = Vector3.Angle(-Vector3.right, direction);
            //transform.Rotate(0f, 0f, direction);
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            lightFixtureRotator.transform.right = direction;
        }
        else
        {
            Idle();
        }
    }

    void Idle()
    {
        // spins the light fixture around
        direction.z = 0.0f;
        direction.Normalize();
        direction = Vector3.RotateTowards(direction, lightFixtureRotator.transform.up, Mathf.PI / 2 * Time.deltaTime, Mathf.PI / 2);
        lightFixtureRotator.transform.right = direction;
        //Debug.Log("idling");
    }

    void ExposeEnemy()
    {
        // check only for enemies that are in range
        for (int i = 0; i < inRangeEnemies.Count; i++)
        {
            if (CalcAngleDiff(lightFixtureRotator.transform.position - inRangeEnemies[i].position))
            {
                inRangeEnemies[i].isVulnerable = true;
            }
        }
    }

    bool CalcAngleDiff(Vector3 lightToTarget)
    {
        // checking if the vampire is within the angle of the light
        if (Vector3.Angle(lightToTarget, -lightFixtureRotator.transform.right) <= 6.4f)
        {
            return true;
        }
        return false;
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "FloodLightPrefab")
    //    {
    //        target.isVulnerable = true;
    //    }
    //}
}
