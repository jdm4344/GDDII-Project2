using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Enemy {

	public GameObject vampPrefab;

    private void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start ()
    {
        base.Start();
        health = 12;
        direction = Vector3.right;
        maxSpeed = 1;
        isVulnerable = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}
}
