using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Enemy {

	public GameObject vampPrefab;

    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        health = 12;
        direction = Vector3.right;
        maxSpeed = 3;
        isVulnerable = true;
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
	}
}
