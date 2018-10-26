using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunNest : Tower {

    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();
        damage = 4;
        cooldown = .333f;
        burstLength = .167f;
        range = 9.0f;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}
}
