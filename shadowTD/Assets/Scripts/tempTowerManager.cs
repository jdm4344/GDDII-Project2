﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempTowerManager : MonoBehaviour {

    // Variables
    public GameObject towerPrefab;
    public List<GameObject> towerList;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < 1; i++)
        {
            towerList.Add(Instantiate(towerPrefab, new Vector3(3 + i, 7, -1), Quaternion.Euler(180, -90, 90)));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
