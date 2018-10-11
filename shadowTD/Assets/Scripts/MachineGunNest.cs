using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunNest : Tower {

    private void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start () {
        base.Start();
        damage = 3;
        cooldown = .667f;
        range = 9.0f;
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}
}
