using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunNest : Tower {

	// Use this for initialization
	void Start () {
        damage = 3;
        cooldown = .667f;
        range = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
